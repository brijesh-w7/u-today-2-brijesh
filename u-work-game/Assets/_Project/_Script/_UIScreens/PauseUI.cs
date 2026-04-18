using UnityEngine.UI;

public class PauseUI : UIScreen
{
    public Button resumeButton;
    public Button quitButton;

    private void OnEnable()
    {
        resumeButton.onClick.AddListener(OnClickedResumeButton);
        quitButton.onClick.AddListener(OnClickedQuitButton);
    }


    private void OnDisable()
    {
        resumeButton.onClick.RemoveListener(OnClickedResumeButton);
        quitButton.onClick.RemoveListener(OnClickedQuitButton);
    }

    private void OnClickedQuitButton()
    {
        PlayButtonClickedSound();
        GameManager.Instance.QuitGame();
    }

    private void OnClickedResumeButton()
    {
        PlayButtonClickedSound();
        GameManager.Instance.TogglePause();
    }

}
