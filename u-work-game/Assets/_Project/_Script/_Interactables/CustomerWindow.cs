
using System.Collections;
using UnityEngine;

public class CustomerWindow : InteractableBase
{
    public OrderUICustomerWindow orderUIRoot;
    public float respawnDelay = 5f;

    public OrderData currentOrder;
    private bool hasOrder;
    private Coroutine respawnCoroutine;


    Coroutine courTimer = null;
    public void SpawnOrder()
    {
        currentOrder = GenerateOrder();
        hasOrder = true;

        if (orderUIRoot != null) orderUIRoot.SetData(currentOrder, GameManager.Instance.ingredientSO);

        StartTimerRoutine();
    }

    public override void Interact(PlayerController player)
    {
        LogsManager.Log(gameObject.name, " Interact ");
        if (!GameManager.Instance.IsPlaying) return;
        if (!hasOrder) return;
        if (!player.HasIngredient) return;

        IngredientObject ing = player.HeldIngredient;

        // Must be ready (chopped/cooked if needed)
        if (!ing.IsReady)
        {
            GameManager.Instance.toastMessage.ShowToastMessage("Not prepared yet!");
            return;
        }

        // Check if this order needs it
        if (!currentOrder.NeedsIngredient(ing.ingredientType))
        {
            LogsManager.Log("Not needed here.", transform.position);
            GameManager.Instance.toastMessage.ShowToastMessage(ing.ingredientType.ToString() + " not needed here.");
            SoundManager.Instance.Play(SoundManager.Instance.Clips.eatableNotDeliverable);
            return;
        }

        // Deliver
        currentOrder.DeliverIngredient(ing.ingredientType);
        player.DiscardHeld();   // consume the ingredient
        SoundManager.Instance.Play(SoundManager.Instance.Clips.eatableDelivered);
        if (currentOrder.isComplete)
        {
            CompleteOrder();
        }
    }

    public void StopOrder()
    {
        if (respawnCoroutine != null) StopCoroutine(respawnCoroutine);
        hasOrder = false;
    }


    void CompleteOrder()
    {
        int score = currentOrder.FinalScore;
        GameManager.Instance.AddScore(score);
        GameManager.Instance.toastMessage.ShowToastMessage(score + " Added.");
        hasOrder = false;
        orderUIRoot.HideUI();
        StopTimerRoutine();
        // Respawn after delay
        respawnCoroutine = StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {

        yield return new WaitForSeconds(respawnDelay);
        if (GameManager.Instance.IsPlaying)
            SpawnOrder();
    }


    OrderData GenerateOrder()
    {
        var order = new OrderData();
        int count = UnityEngine.Random.Range(0, 199) % 2 == 0 ? 2 : 3;
        IngredientType[] allTypes = GameManager.Instance.ingredientSO.availableIngredients.ToArray();
        for (int i = 0; i < count; i++)
        {
            order.requiredIngredients.Add(allTypes[UnityEngine.Random.Range(0, 1984) % allTypes.Length]);
        }
        return order;
    }

    public IEnumerator StartTimer()
    {
        currentOrder.timeElapsed = 0;
        float gameDuration = GameManager.Instance.gameDuration;
        while (currentOrder != null && currentOrder.timeElapsed < gameDuration)
        {
            yield return Constants.WFS_1;
            currentOrder.timeElapsed++;
            if (orderUIRoot != null && orderUIRoot.timerText != null)
            {
                orderUIRoot.timerText.text = CommonUtils.GetTimeInMinuteAndSeconds(currentOrder.timeElapsed);
            }
        }
    }

    private void StartTimerRoutine()
    {
        StopTimerRoutine();
        courTimer = StartCoroutine(StartTimer());
    }
    private void StopTimerRoutine()
    {
        if (courTimer != null)
        {
            StopCoroutine(courTimer);
            courTimer = null;
        }
    }
}
