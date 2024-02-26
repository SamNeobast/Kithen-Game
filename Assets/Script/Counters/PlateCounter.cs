using System;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event Action OnSpawnPlate;
    public event Action OnRemovePlate;


    [SerializeField] private KithenObjectSO plateKithenObjectSO;

    private float spawnPlateTimerMax = 4f;
    private float spawnPlateTimer;
    private int plateCount;
    private int plateCountMax = 4;


    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimerMax <= spawnPlateTimer)
        {
            spawnPlateTimer = 0f;

            if (GameManager.Instance.IsGamePlaying() && plateCount < plateCountMax)
            {
                plateCount++;
                OnSpawnPlate?.Invoke();
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKithenObject())
        {
            if (plateCount > 0)
            {
                plateCount--;
                KithenObject.SpawnKithenObject(plateKithenObjectSO, player);
                OnRemovePlate?.Invoke();
            }
        }
    }


}
