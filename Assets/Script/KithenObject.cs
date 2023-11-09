using UnityEngine;

public class KithenObject : MonoBehaviour
{
    [SerializeField] private KithenObjectSO kithenObjectSO;

    private IKithenObjectParent kithenObjectParent;

    public void SetKithenObjectParent(IKithenObjectParent kithenObjectParent)
    {
        if (this.kithenObjectParent != null)
        {
            this.kithenObjectParent.ClearKithenObjectParent();
        }

        this.kithenObjectParent = kithenObjectParent;

        if (this.kithenObjectParent.HasKithenObjectParent())
        {
            Debug.LogError("KithenObjectParent already has object");
        }
      
        this.kithenObjectParent.SetKithenObjectParent(this);

        transform.parent = this.kithenObjectParent.GetTopKithenPointFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKithenObjectParent GetKithenObjectParent()
    {
        return kithenObjectParent;
    }

    public KithenObjectSO GetKithenObjectSO() { return kithenObjectSO; }
}
