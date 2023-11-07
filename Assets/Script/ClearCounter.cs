using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private Transform topKithenPoint;
    [SerializeField] private KithenObjectSO kithenObjectSO;
    public void Interact()
    {
        Transform kithenObjectSOTransform = Instantiate(kithenObjectSO.prefab, topKithenPoint);
        kithenObjectSOTransform.localPosition = Vector3.zero;

        Debug.Log(kithenObjectSOTransform.GetComponent<KithenObject>().GetKithenObjectSO().objectName);
    }
}
