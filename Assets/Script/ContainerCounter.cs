using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event Action OnPlayerGrabbedObject;

    [SerializeField] private KithenObjectSO kithenObjectSO;


    public override void Interact(Player player)
    {
        if (!player.HasKithenObject())
        {
            KithenObject.SpawnKithenObject(kithenObjectSO, player);

            OnPlayerGrabbedObject?.Invoke();
        }
    }


}
