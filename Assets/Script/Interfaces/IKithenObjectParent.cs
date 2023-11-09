using UnityEngine;

public interface IKithenObjectParent
{
    public Transform GetTopKithenPointFollowTransform();

    public void SetKithenObjectParent(KithenObject kithenObject);

    public KithenObject GetKithenObjectParent();

    public void ClearKithenObjectParent();

    public bool HasKithenObjectParent();
}
