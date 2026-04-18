using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : InteractableBase
{
    public GameObject vegetablePrefab;

    public GameObject cheesePrefab;

    public GameObject meatPrefab;

    public IngredientType[] availableTypes = { IngredientType.Vegetable, IngredientType.Cheese, IngredientType.Meat };


    public override void Interact(PlayerController player)
    {
        LogsManager.Log(gameObject.name, " Interact: ");
        // Player can only pick up if hands are empty
        if (player.HasIngredient)
        {
            GameManager.Instance.toastMessage.ShowToastMessage("Hands full!");
            return;
        }

        if (!GameManager.Instance.IsPlaying) return;

        IngredientType type = RequiredIngredientFromHere();

        GameObject prefab = GetPrefab(type);
        if (prefab == null)
        {
            Debug.LogWarning($"Refrigerator: no prefab assigned for {type}");
            return;
        }

        GameObject ingredientGO = prefab;
        ingredientGO.SetActive(true);
        ingredientGO.transform.position = transform.position;
        ingredientGO.transform.rotation = Quaternion.identity;
        IngredientObject ingredient = ingredientGO.GetComponent<IngredientObject>();
        if (ingredient != null)
            player.PickUp(ingredient);
    }

    GameObject GetPrefab(IngredientType type) => GameManager.Instance.ingredientSO.GetFromPool(type).gameObject;


    public IngredientType RequiredIngredientFromHere()
    {
        List<IngredientType> type = new List<IngredientType>();
        foreach (var item in CustomerWindowManager.Instance.windows)
        {
            if (item.currentOrder == null) continue;
            Dictionary<IngredientType, int> needs = item.currentOrder.GetRemainingNeeds();

            foreach (var entry in needs)
            {
                if (entry.Value > 0 && !type.Contains(entry.Key)) type.Add(entry.Key);
            }
        }
        // LogsManager.Log("Reqq: " + JsonConvert.SerializeObject(type, Formatting.Indented));
        return type[UnityEngine.Random.Range(0, type.Count)];
    }
}
