using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{

    [SerializeField] private CuttingCounter cuttingCounter;

    private const string CUT = "Cut";
    private Animator containerAnimation;

    private void Awake()
    {
        containerAnimation = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        cuttingCounter.OnCut += StartAnimationContainer;

    }
    private void OnDisable()
    {
        cuttingCounter.OnCut -= StartAnimationContainer;
    }

    private void StartAnimationContainer()
    {
        containerAnimation.SetTrigger(CUT);
    }
}
