public class ClearCounter : BaseCounter
{


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
            if (player.HasKithenObject())
            {
                if (player.GetKithenObject().TryGetPlate(out PlateKithenObject plateKithenObject))
                {
                    if (plateKithenObject.TryAddIngredient(GetKithenObject().GetKithenObjectSO()))
                    {
                        GetKithenObject().DestroySelf();
                    }
                }
                else
                {
                    if (GetKithenObject().TryGetPlate(out plateKithenObject))
                    {
                        if (plateKithenObject.TryAddIngredient(player.GetKithenObject().GetKithenObjectSO()))
                        {
                            player.GetKithenObject().DestroySelf();
                        }
                    }
                }
            }
            if (!player.HasKithenObject())
            {
                GetKithenObject().SetKithenObjectParent(player);
            }
        }
    }


}
