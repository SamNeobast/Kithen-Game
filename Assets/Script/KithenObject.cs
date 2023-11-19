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

        if (this.kithenObjectParent.HasKithenObject())
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

    public void DestroySelf()
    {
        kithenObjectParent.ClearKithenObjectParent();
        Destroy(gameObject);
    }



    public static KithenObject SpawnKithenObject(KithenObjectSO kithenObjectSO,
        IKithenObjectParent kithenObjectParent)
    {
        Transform kithenObjectTransform = Instantiate(kithenObjectSO.prefab);

        KithenObject kithenObject = kithenObjectTransform.GetComponent<KithenObject>();

        kithenObject.SetKithenObjectParent(kithenObjectParent);

        return kithenObject;
    }
}
