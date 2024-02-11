using UnityEngine;

public class DeliveryCounter : BaseCounter
{

    public override void Interact(Player player)
    {
        if (player.HasKithenObject())
        {
            if (player.GetKithenObject().TryGetPlate(out PlateKithenObject plateKithenObject))
            {

                DeliveryManager.Instance.DeliverRecipe(plateKithenObject);
                player.GetKithenObject().DestroySelf();
            }
        }
    }
}
