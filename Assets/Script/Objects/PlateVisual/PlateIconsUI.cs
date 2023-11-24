using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{

    [SerializeField] private PlateKithenObject plateKithenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        plateKithenObject.OnIngredientAdded += PlateKithenObject_OnIngredientAdded;
    }

    private void PlateKithenObject_OnIngredientAdded(KithenObjectSO obj)
    {
        UpdateVisual();
    }

    private void OnDisable()
    {
        plateKithenObject.OnIngredientAdded -= PlateKithenObject_OnIngredientAdded;
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child != iconTemplate)
            {
                Destroy(child.gameObject);
            }
        }
        foreach (KithenObjectSO kithenObjectSO in plateKithenObject.GetKithenObjectSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetKithenObjectSO(kithenObjectSO);
        }
    }
}
