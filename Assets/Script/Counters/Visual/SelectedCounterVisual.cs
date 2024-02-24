using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;

    private void OnEnable()
    {
        Player.OnSelectedCounterChanged += SelectedCounter;
    }
    private void OnDisable()
    {

        Player.OnSelectedCounterChanged -= SelectedCounter;
    }

    private void SelectedCounter(BaseCounter selectedCounter)
    {
        if (selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }

}
