using System;
using System.Collections.Generic;

[System.Serializable]
public class OrderData
{
    public List<IngredientType> requiredIngredients = new List<IngredientType>();
    public List<IngredientType> deliveredIngredients = new List<IngredientType>();
    public int timeElapsed = 0;
    public bool isComplete = false;
    public event Action OnIngredientDelivered;

    public int BaseScore
    {
        get
        {
            int total = 0;
            foreach (var ing in requiredIngredients)
            {
                switch (ing)
                {
                    case IngredientType.Vegetable: total += 20; break;
                    case IngredientType.Cheese: total += 10; break;
                    case IngredientType.Meat: total += 30; break;
                }
            }
            return total;
        }
    }

    public int FinalScore => BaseScore - timeElapsed;

    public bool NeedsIngredient(IngredientType type)
    {
        int needed = 0;
        int delivered = 0;
        foreach (var ing in requiredIngredients)
            if (ing == type) needed++;
        foreach (var ing in deliveredIngredients)
            if (ing == type) delivered++;
        return delivered < needed;
    }

    public void DeliverIngredient(IngredientType type)
    {
        deliveredIngredients.Add(type);
        if (deliveredIngredients.Count >= requiredIngredients.Count)
        {
            isComplete = true;
        }
        OnIngredientDelivered?.Invoke();
    }


    public Dictionary<IngredientType, int> GetRemainingNeeds()
    {
        var needs = new Dictionary<IngredientType, int>();
        foreach (var ing in requiredIngredients)
        {
            if (!needs.ContainsKey(ing)) needs[ing] = 0;
            needs[ing]++;
        }
        foreach (var ing in deliveredIngredients)
        {
            if (needs.ContainsKey(ing) && needs[ing] > 0)
                needs[ing]--;
        }
        return needs;
    }
}
