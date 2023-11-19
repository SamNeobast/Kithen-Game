using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KithenObjectSO cutKithenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKithenObject())
        {

            if (player.HasKithenObject())
            {
                player.GetKithenObject().SetKithenObjectParent(this);
            }
        }
        else
        {
            if (!player.HasKithenObject())
            {
                GetKithenObject().SetKithenObjectParent(player);
            }
        }
    }

    public override void InteractAlternative(Player player)
    {
        if (HasKithenObject())
        {
            GetKithenObject().DestroySelf();

            KithenObject.SpawnKithenObject(cutKithenObjectSO, this);
        }
    }
}
