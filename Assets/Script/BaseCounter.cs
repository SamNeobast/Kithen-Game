using UnityEngine;

public class BaseCounter : MonoBehaviour, IKithenObjectParent
{
    [SerializeField] private Transform topKithenPoint;

    private KithenObject kithenObject;

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter");
    }


    public Transform GetTopKithenPointFollowTransform()
    {
        return topKithenPoint;
    }

    public void SetKithenObjectParent(KithenObject kithenObject)
    {
        this.kithenObject = kithenObject;
    }

    public KithenObject GetKithenObject()
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
