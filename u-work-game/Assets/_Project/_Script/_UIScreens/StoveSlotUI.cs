using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StoveSlotUI
{
    public GameObject root;
    public Image progressFill;
    public GameObject doneIcon;

    public void Show()
    {
        if (root != null) root.SetActive(true);
        if (doneIcon != null) doneIcon.SetActive(false);
        if (progressFill != null) progressFill.gameObject.SetActive(true);
        if (progressFill != null) progressFill.fillAmount = 0f;
    }

    public void Hide()
    {
        if (root != null) root.SetActive(false);
    }

    public void SetProgress(float t)
    {
        if (progressFill != null) progressFill.fillAmount = t;
    }

    public void SetDone()
    {
        if (progressFill != null) progressFill.fillAmount = 1f;
        if (doneIcon != null) doneIcon.SetActive(true);
    }
}
