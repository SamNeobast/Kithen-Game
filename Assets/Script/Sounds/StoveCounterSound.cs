using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private AudioSource audioSource;
    private float warningSoundTimer;
    private bool playWarningSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }


    private void OnDisable()
    {
        stoveCounter.OnStateChanged -= StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged -= StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(float progressNormalized)
    {
        float burnShowProgressAmount = 0.5f;
        playWarningSound = stoveCounter.isFried() && progressNormalized >= burnShowProgressAmount;
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

    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0)
            {
                float warningSoundTimerMax = 0.2f;
                warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }
}
