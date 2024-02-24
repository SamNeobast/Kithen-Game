using System;

public class TrashCounter : BaseCounter
{
    public static event Action<object> OnAnyObjectTrashed;

    public override void Interact(Player player)
    {
        if (player.HasKithenObject())
        {
            player.GetKithenObject().DestroySelf();

            OnAnyObjectTrashed?.Invoke(this);
        }

    }
}
