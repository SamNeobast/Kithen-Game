using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event Action DeliverySpawned;
    public event Action DeliveryCompleted;
    public event Action DeliveryFailed;
    public event Action DeliverySucces;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitinResipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;
    private int countSuccessResipes;

    private void Awake()
    {
        Instance = this;
        waitinResipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;

        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitinResipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.resipeSOList
                    [UnityEngine.Random.Range(0, recipeListSO.resipeSOList.Count)];

                waitinResipeSOList.Add(waitingRecipeSO);
                DeliverySpawned?.Invoke();
            }
        }

    }

    public void DeliverRecipe(PlateKithenObject plateKithenObject)
    {
        for (int i = 0; i < waitinResipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitinResipeSOList[i];

            if (waitingRecipeSO.kithenObjectSOList.Count == plateKithenObject.GetKithenObjectSOList().Count)
            {

                bool plateContentsMathesRecipe = true;

                foreach (KithenObjectSO recipeKithenObjectSO in waitingRecipeSO.kithenObjectSOList)
                {
                    bool ingredientFound = false;

                    foreach (KithenObjectSO plateKithenObjectSO in plateKithenObject.GetKithenObjectSOList())
                    {
                        if (recipeKithenObjectSO == plateKithenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        plateContentsMathesRecipe = false;
                    }
                }

                if (plateContentsMathesRecipe)
                {
                    countSuccessResipes++;

                    waitinResipeSOList.RemoveAt(i);
                    DeliveryCompleted?.Invoke();
                    DeliverySucces?.Invoke();
                    return;
                }
            }
        }

        //Player did not deliver a corect recipe
        DeliveryFailed?.Invoke();
    }


    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitinResipeSOList;
    }

    public int GetCountSuccessRecipes()
    {
        return countSuccessResipes;
    }
}
