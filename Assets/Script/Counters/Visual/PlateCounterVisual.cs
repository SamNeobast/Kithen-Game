using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private Transform topKithenPoint;

    private List<GameObject> plateVisualGameObjectList;

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }

    private void OnEnable()
    {
        plateCounter.OnSpawnPlate += PlateCounter_OnSpawnPlate;
        plateCounter.OnRemovePlate += PlateCounter_OnRemovePlate;
    }


    private void OnDisable()
    {
        plateCounter.OnSpawnPlate -= PlateCounter_OnSpawnPlate;
        plateCounter.OnRemovePlate -= PlateCounter_OnRemovePlate;
    }
    private void PlateCounter_OnRemovePlate()
    {
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlateCounter_OnSpawnPlate()
    {
        Transform platesVisualTransform = Instantiate(plateVisualPrefab, topKithenPoint);

        float plateOfSetY = 0.1f;
        platesVisualTransform.position += new Vector3(0, plateOfSetY * plateVisualGameObjectList.Count, 0);

        plateVisualGameObjectList.Add(platesVisualTransform.gameObject);
    }
}
