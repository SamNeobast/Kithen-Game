using UnityEngine;

public class ClearCounter : MonoBehaviour, IKithenObjectParent
{
    [SerializeField] private Transform topKithenPoint;
    [SerializeField] private KithenObjectSO kithenObjectSO;

    private KithenObject kithenObject;

    public void Interact(Player player)
    {
        if (kithenObject == null)
        {
            Transform kithenObjectTransform = Instantiate(kithenObjectSO.prefab, topKithenPoint);
            kithenObjectTransform.GetComponent<KithenObject>().SetKithenObjectParent(this);
        }
        else
        {
            kithenObject.SetKithenObjectParent(player);
        }
    }

    public Transform GetTopKithenPointFollowTransform()
    {
        return topKithenPoint;
    }

    public void SetKithenObjectParent(KithenObject kithenObject)
    {
        this.kithenObject = kithenObject;
    }

    public KithenObject GetKithenObjectParent()
    {
        return kithenObject;
    }

    public void ClearKithenObjectParent()
    {
        kithenObject = null;
    }

    public bool HasKithenObjectParent()
    {
        return kithenObject != null;
    }

}
