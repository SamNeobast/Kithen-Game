using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event Action OnPlayerGrabbedObject;

    [SerializeField] private KithenObjectSO kithenObjectSO;


    public override void Interact(Player player)
    {
        if (!player.HasKithenObjectParent())
        {
            Transform kithenObjectTransform = Instantiate(kithenObjectSO.prefab);
            kithenObjectTransform.GetComponent<KithenObject>().SetKithenObjectParent(player);
            OnPlayerGrabbedObject?.Invoke();
        }
    }


}
