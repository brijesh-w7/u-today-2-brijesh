using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChoppingTable : InteractableBase
{
    public float chopTime = 2f;

    public GameObject progressBarRoot;
    public Image progressBarFill;
    public GameObject ingredientSlotIcon;

    public Transform ingredientContainer;
    public IngredientObject placedIngredient;
    public bool isChopping;
    public float chopProgress;
    public Coroutine chopRoutine;
    Collider mCollider;
    void Awake()
    {
        mCollider = GetComponent<Collider>();
        if (progressBarRoot != null) progressBarRoot.SetActive(false);
    }

    public override void Interact(PlayerController player)
    {
        LogsManager.Log(gameObject.name, " Interact: ", GameManager.Instance.IsPlaying, player.HasIngredient);
        if (!GameManager.Instance.IsPlaying) return;

        // Case 1: player wants to place a vegetable
        if (player.HasIngredient)
        {
            var ing = player.HeldIngredient;

            if (ing.ingredientType != IngredientType.Vegetable)
            {
                GameManager.Instance.toastMessage.ShowToastMessage("Table only chops\nvegetables!");
                return;
            }
            if (isChopping || placedIngredient != null)
            {
                GameManager.Instance.toastMessage.ShowToastMessage("Table is busy!");
                return;
            }
            if (ing.ingredientState == IngredientState.Processed)
            {
                GameManager.Instance.toastMessage.ShowToastMessage("Already chopped!");
                return;
            }

            // Place ingredient on table
            placedIngredient = player.DropHeld();
            // placedIngredient.transform.position = transform.position + Vector3.up * 0.1f;

            placedIngredient.transform.SetParent(ingredientContainer.transform);
            placedIngredient.transform.localPosition = Vector3.zero;
            // Begin chopping
            chopRoutine = StartCoroutine(ChopRoutine());
        }
        // Case 2: player wants to pick up finished ingredient
        else if (placedIngredient != null && !isChopping && placedIngredient.IsReady)
        {
            IngredientObject finished = placedIngredient;
            placedIngredient = null;
            finished.transform.SetParent(null);
            player.PickUp(finished);
        }
        else if (isChopping)
        {
            GameManager.Instance.toastMessage.ShowToastMessage("Still chopping...");
        }
    }

    IEnumerator ChopRoutine()
    {
        isChopping = true;
        chopProgress = 0f;
        SoundManager.Instance.Play(SoundManager.Instance.Clips.eatableProcessing);
        if (progressBarRoot != null) progressBarRoot.SetActive(true);

        while (chopProgress < chopTime)
        {
            chopProgress += Time.deltaTime;
            float t = Mathf.Clamp01(chopProgress / chopTime);

            if (progressBarFill != null) progressBarFill.fillAmount = t;

            yield return null;
        }

        if (placedIngredient != null)
        {
            placedIngredient.SetProcessed();
            SoundManager.Instance.Play(SoundManager.Instance.Clips.eatableProcessed);
        }

        isChopping = false;
        if (progressBarRoot != null) progressBarRoot.SetActive(false);


        LogsManager.Log("Chopped!", transform.position);
        if (IsTouchingPlayer(mCollider))
        {
            Interact(GameManager.Instance.playerController);
        }
    }


    public void ForceReset()
    {
        if (chopRoutine != null) StopCoroutine(chopRoutine);
        isChopping = false;
        chopProgress = 0f;
        if (progressBarRoot != null) progressBarRoot.SetActive(false);

        if (placedIngredient != null)
        {
            GameManager.Instance.ingredientSO.ReleaseToPool(placedIngredient);
            placedIngredient = null;
        }
    }
}
