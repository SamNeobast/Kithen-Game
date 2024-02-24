using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{

    [SerializeField] private ContainerCounter containerCounter;

    private const string OPEN_CLOSE = "OpenClose";
    private Animator containerAnimation;

    private void Awake()
    {
        containerAnimation = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        containerCounter.OnPlayerGrabbedObject += StartAnimationContainer;

    }
    private void OnDisable()
    {
        containerCounter.OnPlayerGrabbedObject -= StartAnimationContainer;
    }

    private void StartAnimationContainer()
    {
        containerAnimation.SetTrigger(OPEN_CLOSE);
    }
}
