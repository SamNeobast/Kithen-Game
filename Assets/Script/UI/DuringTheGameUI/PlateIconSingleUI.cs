using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetKithenObjectSO(KithenObjectSO kithenObjectSO)
    {
        image.sprite = kithenObjectSO.sprite;
    }
}
