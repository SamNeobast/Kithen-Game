using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KithenObjectSO kithenObjectSO;


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


}
