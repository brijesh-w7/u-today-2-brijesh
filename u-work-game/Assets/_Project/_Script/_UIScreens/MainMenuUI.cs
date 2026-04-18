
using UnityEngine.UI;


public class MainMenuUI : UIScreen
{
    public Button startGameButton;

    private void OnEnable()
    {
        startGameButton?.onClick.AddListener(OnClickedStartButton);
    }

    private void OnClickedStartButton()
    {
        PlayButtonClickedSound();
        GameManager.Instance.StartGame();
    }

    private void OnDisable()
    {
        startGameButton?.onClick.RemoveListener(OnClickedStartButton);
    }

}
