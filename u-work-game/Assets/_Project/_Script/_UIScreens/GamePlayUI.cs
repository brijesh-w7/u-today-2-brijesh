using UnityEngine.UI;
using TMPro;

public class GamePlayUI : UIScreen
{
    public TMP_Text currentScoreTxt;
    public TMP_Text bestScoreTxt;
    public TMP_Text remaingDurationTxt;
    public TMP_Text fpsText;
    public Button pauseButton;

    private void OnEnable()
    {

        GameManager.OnScoreUpdated += OnScoreUpdatedCallback;
        pauseButton.onClick.AddListener(OnClickedPauseButton);
    }

    public void UpdateScoreText()
    {
        if (currentScoreTxt != null) currentScoreTxt.text = GameManager.Instance.currentScore.ToString();
        if (bestScoreTxt != null) bestScoreTxt.text = Prefs.BestScore.ToString();
    }

    private void OnDisable()
    {
        GameManager.OnScoreUpdated -= OnScoreUpdatedCallback;
        pauseButton.onClick.RemoveListener(OnClickedPauseButton);
    }

    // private void Update()
    // {
    //     float fps = 1.0f / Time.unscaledDeltaTime;
    //     fpsText.text = Mathf.FloorToInt(fps).ToString();
    //     // Debug.Log("Current FPS: " + fps);
    // }
    private void OnClickedPauseButton()
    {
        PlayButtonClickedSound();
        GameManager.Instance.TogglePause();
    }

    private void OnScoreUpdatedCallback(int score)
    {
        if (currentScoreTxt != null) currentScoreTxt.text = score.ToString();
    }


}
