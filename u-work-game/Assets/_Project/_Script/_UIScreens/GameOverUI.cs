using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameOverUI : UIScreen
{
    public TMP_Text currentScoreTxt;
    public TMP_Text bestScoreTxt;
    public TMP_Text gotNewBestScoreValueText;
    public GameObject newBestScoreObject;
    public Button replayButton;
    public Button quitButton;

    public void SetData(int currentScore, int bestScore, bool gotNewBestScore)
    {
        currentScoreTxt.text = currentScore.ToString();
        bestScoreTxt.text = bestScore.ToString();
        if (gotNewBestScore)
        {
            newBestScoreObject.SetActive(true);
            gotNewBestScoreValueText.text = bestScoreTxt.text;
        }
        else
        {

            newBestScoreObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        replayButton.onClick.AddListener(OnClickedReplayButton);
        quitButton.onClick.AddListener(OnClickedQuitButton);
    }


    private void OnDisable()
    {
        replayButton.onClick.RemoveListener(OnClickedReplayButton);
        quitButton.onClick.RemoveListener(OnClickedQuitButton);
    }

    private void OnClickedQuitButton()
    {
        PlayButtonClickedSound();
        GameManager.Instance.QuitGame();
    }

    private void OnClickedReplayButton()
    {
        PlayButtonClickedSound();
        GameManager.Instance.ReStartGame();
    }


}
