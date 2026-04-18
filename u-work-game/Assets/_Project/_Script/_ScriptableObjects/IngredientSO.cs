using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Ingredient_SO", menuName = "ScriptableObjects/Ingredient SO File")]
public class IngredientSO : ScriptableObject
{

    [Serializable]
    public class Item
    {
        public IngredientType type;
        public Sprite icon;
        public Sprite iconProcessed;
        public Transform prefab;
        public ObjectPool<GameObject> pool;
    }

    public List<IngredientType> availableIngredients;
    public List<Item> items;


    public Sprite GetIcon(IngredientType type)
    {
        foreach (var item in items)
        {
            if (item.type == type) return item.icon;
        }
        return null;
    }

    public Sprite GetProcessedIcon(IngredientType type)
    {
        foreach (var item in items)
        {
            if (item.type == type) return item.iconProcessed;
        }
        return null;
    }
    public Item GetItem(IngredientType type)
    {
        foreach (var item in items)
        {
            if (item.type == type) return item;
        }
        return null;
    }

    public void ReleaseToPool(IngredientObject obj)
    {
        GetItem(obj.ingredientType).pool.Release(obj.gameObject);
    }

    public Transform GetFromPool(IngredientType type)
    {

        foreach (var item in items)
        {
            if (item.type == type)
            {
                if (item.pool == null)
                {

                    item.pool = new ObjectPool<GameObject>(
             createFunc: () =>
    {
        GameObject gameObject = Instantiate(item.prefab.gameObject);
        gameObject.name = "PooledCube";
        gameObject.SetActive(true);
        return gameObject;
    },
             actionOnGet: (go) => { go.SetActive(false); },
             actionOnRelease: (go) =>
             {

                 go.GetComponent<IngredientObject>().ResetData();
                 go.SetActive(false);
             },
             actionOnDestroy: null,
             collectionCheck: true,   // helps catch double-release mistakes
             defaultCapacity: 1,
             maxSize: 100
         );
                }

                return item.pool.Get().transform;

            }
        }
        return null;
    }

}