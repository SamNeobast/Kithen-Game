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
        DeliveryManager.Instance.OnDeliverySpawned += DeliveryManager_DeliverySpawned;
        DeliveryManager.Instance.OnDeliveryCompleted += DeliveryManager_DeliveryCompleted;

        UpdateVisual();
    }

    private void OnDisable()
    {
        DeliveryManager.Instance.OnDeliverySpawned -= DeliveryManager_DeliverySpawned;
        DeliveryManager.Instance.OnDeliveryCompleted -= DeliveryManager_DeliveryCompleted;
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
