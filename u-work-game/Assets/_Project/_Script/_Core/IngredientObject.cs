using UnityEngine;

public class IngredientObject : MonoBehaviour
{
    public IngredientType ingredientType;
    public IngredientState ingredientState = IngredientState.Raw;

    public Material rawMaterial;

    public Material processedMaterial;

    public Renderer mRenderer;

    public int BaseScore
    {
        get
        {
            switch (ingredientType)
            {
                case IngredientType.Vegetable: return 20;
                case IngredientType.Cheese: return 10;
                case IngredientType.Meat: return 30;
                default: return 0;
            }
        }
    }

    public bool IsReady
    {
        get
        {
            if (ingredientType == IngredientType.Cheese) return true;
            return ingredientState == IngredientState.Processed;
        }
    }

    void Awake()
    {
        mRenderer = GetComponent<Renderer>();
        ResetData();
    }

    public void SetProcessed()
    {
        ingredientState = IngredientState.Processed;
        RefreshVisuals();
    }

    public void RefreshVisuals()
    {
        if (mRenderer == null) return;

        if (ingredientState == IngredientState.Processed && processedMaterial != null)
            mRenderer.sharedMaterial = processedMaterial;
        else if (mRenderer != null)
            mRenderer.sharedMaterial = rawMaterial;
    }


    public void ResetData()
    {
        ingredientState = IngredientState.Raw;
        RefreshVisuals();
    }

}
