using UnityEngine;


[RequireComponent(typeof(CanvasGroup))]
public class UIScreen : MonoParent
{
    protected CanvasGroup cg;

    protected virtual void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        HideUI();
    }

    public void ShowUI()
    {
        if (cg == null) cg = gameObject.AddComponent<CanvasGroup>();
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    public void HideUI()
    {
        if (cg == null) cg = gameObject.AddComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
}
