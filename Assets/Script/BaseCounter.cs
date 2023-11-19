using UnityEngine;

public class BaseCounter : MonoBehaviour, IKithenObjectParent
{
    [SerializeField] private Transform topKithenPoint;

    private KithenObject kithenObject;

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter - Interact");
    }

    public virtual void InteractAlternative(Player player)
    {
        Debug.LogError("BaseCounter - InteractAlternative");
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

    public bool HasKithenObject()
    {
        return kithenObject != null;
    }
}
