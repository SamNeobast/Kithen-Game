using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private Transform topKithenPoint;
    [SerializeField] private KithenObjectSO kithenObjectSO;
    [SerializeField] private ClearCounter secondClearCounter;

    [SerializeField] private bool testing;

    private KithenObject kithenObject;

    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            if (kithenObject != null)
            {
                kithenObject.SetClearCounter(secondClearCounter);
            }
        }

    }

    public void Interact()
    {
        if (kithenObject == null)
        {
            Transform kithenObjectTransform = Instantiate(kithenObjectSO.prefab, topKithenPoint);
            kithenObjectTransform.GetComponent<KithenObject>().SetClearCounter(this);
        }
        else
        {
            Debug.Log(kithenObject.GetClearCounter());
        }
    }

    public Transform GetTopKithenPointFollowTransform()
    {
        return topKithenPoint;
    }

    public void SetKithenObject(KithenObject kithenObject)
    {
        this.kithenObject = kithenObject;
    }

    public KithenObject GetKithenObject()
    {
        return kithenObject;
    }

    public void ClearKithenObject()
    {
        kithenObject = null;
    }

    public bool HasKithenObject()
    {
        return kithenObject != null;
    }
}
