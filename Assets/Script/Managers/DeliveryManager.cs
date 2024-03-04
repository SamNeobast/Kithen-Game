using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event Action OnDeliverySpawned;
    public event Action OnDeliveryCompleted;
    public event Action OnDeliveryFailed;
    public event Action OnDeliverySucces;

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

            if (GameManager.Instance.IsGamePlaying() && waitinResipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.resipeSOList
                    [UnityEngine.Random.Range(0, recipeListSO.resipeSOList.Count)];

                waitinResipeSOList.Add(waitingRecipeSO);
                OnDeliverySpawned?.Invoke();
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
                    OnDeliveryCompleted?.Invoke();
                    OnDeliverySucces?.Invoke();
                    return;
                }
            }
        }

        //Player did not deliver a corect recipe
        OnDeliveryFailed?.Invoke();
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
