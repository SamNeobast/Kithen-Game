using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{

    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;

    private void OnEnable()
    {
        stoveCounter.OnStateChanged += ChangeVisualObject;
    }
    private void OnDisable()
    {
        stoveCounter.OnStateChanged -= ChangeVisualObject;
    }


    private void ChangeVisualObject()
{
    bool stateIsFrying = stoveCounter.State == StoveCounter.StoveState.Frying;
    bool stateIsFried = stoveCounter.State == StoveCounter.StoveState.Fried;

    bool showVisual = stateIsFrying || stateIsFried;

    stoveOnGameObject.SetActive(showVisual);
    particlesGameObject.SetActive(showVisual);
}
}
