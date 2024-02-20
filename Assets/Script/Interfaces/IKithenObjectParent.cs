using UnityEngine;

public interface IKithenObjectParent
{
    public Transform GetTopKithenPointFollowTransform();

    public void SetKithenObject(KithenObject kithenObject);

    public KithenObject GetKithenObject();

    public void ClearKithenObjectParent();

    public bool HasKithenObject();
}
