using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cutKithenObjectSOArray;

    private int cuttingProgress;
    public override void Interact(Player player)
    {
        if (!HasKithenObject())
        {

            if (player.HasKithenObject())
            {
                if (HasRecipeWithInput(player.GetKithenObject().GetKithenObjectSO()))
                {
                    player.GetKithenObject().SetKithenObjectParent(this);
                    cuttingProgress = 0;
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
            cuttingProgress++;
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKithenObject().GetKithenObjectSO());

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressCountMax)
            {
                KithenObjectSO outputKithenObjectSO = GetOutputForInput(GetKithenObject().GetKithenObjectSO());

                GetKithenObject().DestroySelf();

                KithenObject.SpawnKithenObject(outputKithenObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(KithenObjectSO inputKithenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKithenObjectSO);
        return cuttingRecipeSO != null;
    }

    private KithenObjectSO GetOutputForInput(KithenObjectSO inputKithenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKithenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }               
        else
        {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KithenObjectSO inputKithenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cutKithenObjectSOArray)
        {
            if (cuttingRecipeSO.input == inputKithenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
