using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KithenObjectSO kithenObjectSO;


    public override void Interact(Player player)
    {
        if (!HasKithenObjectParent())
        {
            
            if (player.HasKithenObjectParent())
            {
                player.GetKithenObject().SetKithenObjectParent(this);
            }
        }
        else
        {
            if (!player.HasKithenObjectParent())
            {
                GetKithenObject().SetKithenObjectParent(player);
            }
        }
    }


}
