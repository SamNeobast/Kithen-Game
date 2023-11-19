using UnityEngine;

public interface IKithenObjectParent
{
    public Transform GetTopKithenPointFollowTransform();

    public void SetKithenObjectParent(KithenObject kithenObject);

    public KithenObject GetKithenObject();

    public void ClearKithenObjectParent();

    public bool HasKithenObject();
}
