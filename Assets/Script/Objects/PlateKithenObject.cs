using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKithenObject : KithenObject
{
    public event Action<KithenObjectSO> OnIngredientAdded;

    [SerializeField] private List<KithenObjectSO> validKithenObjectSOList;

    private List<KithenObjectSO> kithenObjectSOList;

    private void Awake()
    {
        kithenObjectSOList = new List<KithenObjectSO>();
    }


    public bool TryAddIngredient(KithenObjectSO kithenObjectSO)
    {
        if (!validKithenObjectSOList.Contains(kithenObjectSO))
        {
            return false;
        }
        if (kithenObjectSOList.Contains(kithenObjectSO))
        {
            return false;
        }
        else
        {
            kithenObjectSOList.Add(kithenObjectSO);
            OnIngredientAdded?.Invoke(kithenObjectSO);
            return true;
        }
    }

}
