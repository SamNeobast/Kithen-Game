using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitinResipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;

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
                    [Random.Range(0, recipeListSO.resipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                waitinResipeSOList.Add(waitingRecipeSO);
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
                    Debug.Log("Player Deiver the correct recipe!");
                    waitinResipeSOList.RemoveAt(i);
                    return;
                }
            }
        }

        Debug.Log("Player did not deliver correct recipe");
    }
}
