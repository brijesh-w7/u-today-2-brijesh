
using System.Collections;
using UnityEngine;

public class Stove : InteractableBase
{
    public float cookTime = 6f;
    public int slotCount = 2;

    public StoveSlotUI[] slotUIs;
    public Transform[] ingredientNodeParent;

    private IngredientObject[] slots;
    private bool[] isCooking;
    private Coroutine[] cookRoutines;
    Collider mCollider;
    void Awake()
    {
        mCollider = GetComponent<Collider>();
        slots = new IngredientObject[slotCount];
        isCooking = new bool[slotCount];
        cookRoutines = new Coroutine[slotCount];

        for (int i = 0; i < slotCount && i < slotUIs.Length; i++)
            slotUIs[i]?.Hide();
    }

    public override void Interact(PlayerController player)
    {
        LogsManager.Log(gameObject.name, " Interact: ");
        if (!GameManager.Instance.IsPlaying) return;

        if (player.HasIngredient)
        {
            var ing = player.HeldIngredient;

            if (ing.ingredientType != IngredientType.Meat)
            {
                GameManager.Instance.toastMessage.ShowToastMessage("Stove only cooks\nmeat!");
                return;
            }
            if (ing.ingredientState == IngredientState.Processed)
            {
                GameManager.Instance.toastMessage.ShowToastMessage("Already cooked!");
                return;
            }

            // Find an empty slot
            int freeSlot = FindFreeSlot();
            if (freeSlot < 0)
            {
                GameManager.Instance.toastMessage.ShowToastMessage("Stove is full!");

                return;
            }

            slots[freeSlot] = player.DropHeld();
            PositionIngredientInSlot(freeSlot);
            cookRoutines[freeSlot] = StartCoroutine(CookRoutine(freeSlot));

            int readySlot = FindReadySlot();
            if (readySlot >= 0 && IsTouchingPlayer(mCollider))
            {
                Interact(GameManager.Instance.playerController);
            }
        }
        else
        {
            // Pick up the first finished (cooked) ingredient
            int readySlot = FindReadySlot();
            if (readySlot >= 0)
            {
                IngredientObject cooked = slots[readySlot];
                slots[readySlot] = null;
                slotUIs[readySlot]?.Hide();
                cooked.transform.SetParent(ingredientNodeParent[readySlot]);
                player.PickUp(cooked);
            }
            else
            {
                LogsManager.Log("Nothing ready yet.", transform.position);
            }
        }
    }

    IEnumerator CookRoutine(int slotIndex)
    {
        isCooking[slotIndex] = true;
        float elapsed = 0f;
        SoundManager.Instance.Play(SoundManager.Instance.Clips.eatableProcessing);
        slotUIs[slotIndex]?.Show();
        float t;
        while (elapsed < cookTime)
        {
            elapsed += Time.deltaTime;
            t = Mathf.Clamp01(elapsed / cookTime);
            slotUIs[slotIndex]?.SetProgress(t);
            yield return null;
        }

        if (slots[slotIndex] != null)
            slots[slotIndex].SetProcessed();

        isCooking[slotIndex] = false;
        slotUIs[slotIndex]?.SetDone();
        SoundManager.Instance.Play(SoundManager.Instance.Clips.eatableProcessed);
        GameManager.Instance.toastMessage.ShowToastMessage("Meat ready!");
        if (IsTouchingPlayer(mCollider))
        {
            Interact(GameManager.Instance.playerController);
        }
    }

    int GetOccupiedSlotCount()
    {
        int count = 0;
        for (int i = 0; i < slotCount; i++)
            if (slots[i] != null) count++;
        return count;
    }

    int FindFreeSlot()
    {
        for (int i = 0; i < slotCount; i++)
            if (slots[i] == null) return i;
        return -1;
    }

    int FindReadySlot()
    {
        for (int i = 0; i < slotCount; i++)
            if (slots[i] != null && !isCooking[i] && slots[i].IsReady)
                return i;
        return -1;
    }

    void PositionIngredientInSlot(int slotIndex)
    {
        if (slots[slotIndex] == null) return;
        slots[slotIndex].transform.SetParent(ingredientNodeParent[slotIndex]);
        slots[slotIndex].transform.localPosition = Vector3.zero;
    }

    public void ForceReset()
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (cookRoutines[i] != null) StopCoroutine(cookRoutines[i]);
            isCooking[i] = false;
            slotUIs[i]?.Hide();
            if (slots[i] != null)
            {
                GameManager.Instance.ingredientSO.ReleaseToPool(slots[i]);
                slots[i] = null;
            }
        }
    }
}


