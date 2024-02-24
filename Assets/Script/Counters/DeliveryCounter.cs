public class DeliveryCounter : BaseCounter
{

    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

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
