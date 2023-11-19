using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cutKithenObjectSOArray;

    public override void Interact(Player player)
    {
        if (!HasKithenObject())
        {

            if (player.HasKithenObject())
            {
                if (HasRecipeWithInput(player.GetKithenObject().GetKithenObjectSO()))
                {

                    player.GetKithenObject().SetKithenObjectParent(this);
                }
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
        if (HasKithenObject() && HasRecipeWithInput(GetKithenObject().GetKithenObjectSO()))
        {
            KithenObjectSO outputKithenObjectSO = GetOutputForInput(GetKithenObject().GetKithenObjectSO());

            GetKithenObject().DestroySelf();

            KithenObject.SpawnKithenObject(outputKithenObjectSO, this);
        }
    }

    private bool HasRecipeWithInput(KithenObjectSO inputKithenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cutKithenObjectSOArray)
        {
            if (cuttingRecipeSO.input == inputKithenObjectSO)
            {
                return true;
            }
        }
        return false;
    }

    private KithenObjectSO GetOutputForInput(KithenObjectSO inputKithenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cutKithenObjectSOArray)
        {
            if (cuttingRecipeSO.input == inputKithenObjectSO)
            {
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }
}
