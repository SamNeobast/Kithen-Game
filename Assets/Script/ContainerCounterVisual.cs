using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        containerCounter.OnPlayerGrabbedObject += StartAnimationContainer;
    }

    private void StartAnimationContainer()
    {
        containerAnimation.SetTrigger(OPEN_CLOSE);
    }
}
