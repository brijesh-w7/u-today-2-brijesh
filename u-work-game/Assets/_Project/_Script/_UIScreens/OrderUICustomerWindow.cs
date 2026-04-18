using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[Serializable]
public class OrderUICustomerWindow : MonoParent
{
    public OrderData currentOrder;
    public IngredientSO ingredientSO;
    public GameObject container;
    public List<Image> ingredientImages;
    public List<Image> ingredientDoneImages;
    public List<GameObject> ingContainer;
    public TMP_Text timerText;

    CanvasGroup cg;

    void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        HideUI();
    }

    internal void SetData(OrderData currentOrder, IngredientSO ingredientSO)
    {
        this.ingredientSO = ingredientSO;
        this.currentOrder = currentOrder;
        currentOrder.OnIngredientDelivered += OnIngredientDeliveredCallback;
        int index = 0;
        DisplayUI();
        foreach (var item in ingredientImages)
        {
            item.gameObject.SetActive(false);
            ingContainer[index].gameObject.SetActive(false);
            index++;
        }

        index = 0;

        foreach (var item in currentOrder.requiredIngredients)
        {
            ingredientImages[index].gameObject.SetActive(true);
            ingContainer[index].gameObject.SetActive(true);
            ingredientImages[index].sprite = ingredientSO.GetIcon(item);
            index++;
        }
    }

    private void OnIngredientDeliveredCallback()
    {
        int index = 0;
        List<IngredientType> doneIng = new List<IngredientType>(currentOrder.deliveredIngredients);
        foreach (var item in currentOrder.requiredIngredients)
        {
            if (doneIng.Contains(item))
            {
                if (index < ingredientDoneImages.Count && ingredientDoneImages[index] != null)
                {
                    ingredientDoneImages[index].gameObject.SetActive(true);
                }
                doneIng.Remove(item);

            }
            index++;

        }
    }

    public void DisplayUI()
    {
        StartCoroutine(FadeCanvas(cg, 1, 1));

        cg.blocksRaycasts = false;
        cg.interactable = false;
        foreach (var item in ingredientDoneImages)
        {
            item.gameObject.SetActive(false);
        }
    }
    public void HideUI()
    {
        StartCoroutine(FadeCanvas(cg, 0, 1));
        cg.blocksRaycasts = false;
        cg.interactable = false;
        if (currentOrder != null)
        {
            currentOrder.OnIngredientDelivered -= OnIngredientDeliveredCallback;
        }
    }

    public void ResetDoneImages()
    {
        foreach (var item in ingredientDoneImages)
        {
            item.gameObject.SetActive(false);
        }
    }

    IEnumerator FadeCanvas(CanvasGroup canvasGroup, float targetAlpha, float time)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / time);
            yield return null;
        }
        canvasGroup.alpha = targetAlpha;
    }
}
