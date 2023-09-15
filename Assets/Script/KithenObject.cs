using UnityEngine;

public class KithenObject : MonoBehaviour
{
    [SerializeField] private KithenObjectSO kithenObjectSO;

    private ClearCounter clearCounter;

    public void SetClearCounter(ClearCounter newClearCounter)
    {
        if (clearCounter != null)
        {
            clearCounter.ClearKithenObject();
        }

        clearCounter = newClearCounter;

        if (clearCounter.HasKithenObject())
        {
            Debug.LogError("Counter already has object");
        }
      
        clearCounter.SetKithenObject(this);

        transform.parent = clearCounter.GetTopKithenPointFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }

    public KithenObjectSO GetKithenObjectSO() { return kithenObjectSO; }
}
