using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{

    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;


    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.DeliverySpawned += DeliveryManager_DeliverySpawned;
        DeliveryManager.Instance.DeliveryCompleted += DeliveryManager_DeliveryCompleted;

        UpdateVisual();
    }

    private void OnDisable()
    {
        DeliveryManager.Instance.DeliverySpawned -= DeliveryManager_DeliverySpawned;
        DeliveryManager.Instance.DeliveryCompleted -= DeliveryManager_DeliveryCompleted;
    }

    private void DeliveryManager_DeliveryCompleted()
    {
        UpdateVisual();
    }

    private void DeliveryManager_DeliverySpawned()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }

    }
}
