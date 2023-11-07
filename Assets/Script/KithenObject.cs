using UnityEngine;

public class KithenObject : MonoBehaviour
{
    [SerializeField] private KithenObjectSO kithenObjectSO;

    public KithenObjectSO GetKithenObjectSO() { return kithenObjectSO; }
}
