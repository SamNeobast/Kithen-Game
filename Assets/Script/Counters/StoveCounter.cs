using System;
using UnityEngine;

public class StoveCounter : BaseCounter
{

    public event Action OnStateChanged;
    public enum StoveState
    {
        Idle,
        Frying,
        Fried,
        Burned
    }


    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private StoveState state;
    public StoveState State
    {
        get { return state; }
    }

    FryingRecipeSO fryingRecipeSo;
    BurningRecipeSO burningRecipeSO;
    private float fryingTimer;
    private float burningTimer;


    private void Update()
    {
        if (HasKithenObject())
        {
            switch (state)
            {
                case StoveState.Idle:
                    break;
                case StoveState.Frying:
                    fryingTimer += Time.deltaTime;

                    if (fryingTimer > fryingRecipeSo.fryingTimerMax)
                    {
                        GetKithenObject().DestroySelf();

                        KithenObject.SpawnKithenObject(fryingRecipeSo.output, this);

                        burningTimer = 0f;

                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKithenObject().GetKithenObjectSO());
                        state = StoveState.Fried;

                        OnStateChanged?.Invoke();
                    }
                    break;
                case StoveState.Fried:
                    burningTimer += Time.deltaTime;

                    if (burningTimer > burningRecipeSO.burningTimerMax)
                    {
                        GetKithenObject().DestroySelf();

                        KithenObject.SpawnKithenObject(burningRecipeSO.output, this);

                        state = StoveState.Burned;
                        OnStateChanged?.Invoke();
                    }
                    break;
                case StoveState.Burned:
                    break;
            }
        }

        
    }


    public override void Interact(Player player)
    {
        if (!HasKithenObject())
        {
            if (player.HasKithenObject())
            {
                if (HasRecipeWithInput(player.GetKithenObject().GetKithenObjectSO()))
                {
                    player.GetKithenObject().SetKithenObjectParent(this);
                    fryingRecipeSo = GetFryingRecipeSOWithInput(GetKithenObject().GetKithenObjectSO());

                    state = StoveState.Frying;
                    OnStateChanged?.Invoke();
                    fryingTimer = 0f;
                }
            }
        }
        else
        {
            if (!player.HasKithenObject())
            {
                GetKithenObject().SetKithenObjectParent(player);
                state = StoveState.Idle;
                OnStateChanged?.Invoke();
            }
        }
    }


    private bool HasRecipeWithInput(KithenObjectSO inputKithenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKithenObjectSO);
        return fryingRecipeSO != null;
    }

    private KithenObjectSO GetOutputForInput(KithenObjectSO inputKithenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKithenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KithenObjectSO inputKithenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKithenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KithenObjectSO inputKithenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKithenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }

}
