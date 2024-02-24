using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void OnDisable()
    {
        stoveCounter.OnStateChanged -= StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged()
    {
        bool playSound = stoveCounter.State == StoveCounter.StoveState.Frying
            || stoveCounter.State == StoveCounter.StoveState.Fried;
        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
