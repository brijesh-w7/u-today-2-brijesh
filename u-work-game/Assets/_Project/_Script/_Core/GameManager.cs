using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class GameManager : SingletonMono<GameManager>
{

    [Header("Game Settings")]
    public int gameDuration = 180;   // in seconds

    public UIContainer UIContainer;
    public ToastMessagePanel toastMessage;
    public PlayerController playerController;
    private int timeRemaining;
    public int currentScore;
    private GameState gameState = GameState.Menu;
    public IngredientSO ingredientSO;

    public GameState State => gameState;
    public bool IsPlaying => gameState == GameState.Playing;

    protected virtual void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 170;
    }

    void Start()
    {
        UIContainer.mainMenuUI.ShowUI();
    }

    public static Action<int> OnScoreUpdated;
    public void AddScore(int points)
    {
        LogsManager.Log("AddScrroe: " + points, currentScore);
        currentScore += points;
        OnScoreUpdated?.Invoke(currentScore);
    }

    public void ReStartGame()
    {
        UIContainer.gameOverUI.HideUI();
        StartGame();
    }

    public void StartGame()
    {
        currentScore = 0;
        timeRemaining = gameDuration;
        gameState = GameState.Playing;
        Time.timeScale = 1f;

        CustomerWindowManager.Instance?.InitializeWindows();
        playerController.ResetPlayer();
        StartCountDownTimerRoutine();
        UIContainer.gamePlayUI.ShowUI();
        UIContainer.gamePlayUI.UpdateScoreText();
        UIContainer.mainMenuUI.HideUI();
    }

    public void TogglePause()
    {
        if (gameState == GameState.Playing)
        {
            UIContainer.pauseUI.ShowUI();
            gameState = GameState.Paused;
            Time.timeScale = 0f;
        }
        else if (gameState == GameState.Paused)
        {
            UIContainer.pauseUI.HideUI();
            gameState = GameState.Playing;
            Time.timeScale = 1f;
        }
    }

    void EndGame()
    {
        gameState = GameState.GameOver;
        Time.timeScale = 1f;
        bool hasGoNewBestScore = false;
        if (currentScore > Prefs.BestScore)
        {
            Prefs.BestScore = currentScore;
            hasGoNewBestScore = true;
        }

        CustomerWindowManager.Instance?.StopAll();

        UIContainer.gamePlayUI.HideUI();
        UIContainer.gameOverUI.ShowUI();
        UIContainer.gameOverUI.SetData(currentScore, Prefs.BestScore, hasGoNewBestScore);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public IEnumerator PlayCountDownTimerRoutine()
    {
        timeRemaining = gameDuration;
        while (timeRemaining >= 0)
        {
            yield return Constants.WFS_1;
            timeRemaining--;
            UIContainer.gamePlayUI.remaingDurationTxt.text = CommonUtils.GetTimeInMinuteAndSeconds(timeRemaining);
        }
        EndGame();
    }

    Coroutine routineCountDownTimer;
    private void StartCountDownTimerRoutine()
    {
        StopCountDownTimerRoutine();
        routineCountDownTimer = StartCoroutine(PlayCountDownTimerRoutine());
    }

    private void StopCountDownTimerRoutine()
    {
        if (routineCountDownTimer != null)
        {
            StopCoroutine(routineCountDownTimer);
            routineCountDownTimer = null;
        }
    }
}
