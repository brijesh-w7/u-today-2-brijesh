using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class ToastMessagePanel : MonoBehaviour
{
    public TMP_Text messageText;
    public GameObject rootObject;
    private CanvasGroup cg;

    Coroutine fadeRoutine;


    private void Start()
    {
        cg = GetComponent<CanvasGroup>();
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
    public void ShowToastMessage(string message)
    {
        messageText.text = message;



        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
            fadeRoutine = null;
        }
        fadeRoutine = StartCoroutine(FadeCanvas(cg));

    }
    IEnumerator FadeCanvas(CanvasGroup canvasGroup)
    {
        float targetAlpha = 1f;
        float time = .5f;

        float startAlpha = 0;
        float elapsed = 0f;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / time);
            yield return null;
        }
        canvasGroup.alpha = targetAlpha;

        yield return Constants.WFS_1;

        targetAlpha = 0f;
        time = .25f;

        startAlpha = 1;
        elapsed = 0f;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / time);
            yield return null;
        }
    }

}
