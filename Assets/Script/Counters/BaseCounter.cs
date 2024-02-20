using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKithenObjectParent
{
    public static event Action<object> OnAnyObjectPlacedHere;

    [SerializeField] private Transform topKithenPoint;

    private KithenObject kithenObject;

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter - Interact");
    }

    public virtual void InteractAlternative(Player player)
    {
        Debug.Log("ќбЇкт не маЇ взаЇмод≥ F");
    }

    public Transform GetTopKithenPointFollowTransform()
    {
        return topKithenPoint;
    }

    public void SetKithenObject(KithenObject kithenObject)
    {
        this.kithenObject = kithenObject;

        if (kithenObject != null)
        {
            OnAnyObjectPlacedHere(this);
        }
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
