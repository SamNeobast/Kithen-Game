using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KithenObjectSO_GameObject
    {
        public KithenObjectSO kithenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKithenObject plateKithenObject;
    [SerializeField] private List<KithenObjectSO_GameObject> kithenObjectsSOGameObjectList;

    private void Start()
    {
        foreach (KithenObjectSO_GameObject kithenObjectSO_GameObject in kithenObjectsSOGameObjectList)
        {
            kithenObjectSO_GameObject.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        plateKithenObject.OnIngredientAdded += PlateKithenObject_OnIngredientAdded;
    }
    private void OnDisable()
    {
        plateKithenObject.OnIngredientAdded -= PlateKithenObject_OnIngredientAdded;
    }


    private void PlateKithenObject_OnIngredientAdded(KithenObjectSO kithenObjectSO)
    {
        foreach (KithenObjectSO_GameObject kithenObjectSO_GameObject in kithenObjectsSOGameObjectList)
        {
            if (kithenObjectSO_GameObject.kithenObjectSO == kithenObjectSO)
            {
                kithenObjectSO_GameObject.gameObject.SetActive(true);
            }
        }
    }
}
