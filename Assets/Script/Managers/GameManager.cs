using System;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action OnStateChange;
    public event Action OnGamePaused;
    public event Action OnGameUnpaused;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePLaying,
        GameOver,
    }

    [SerializeField] private float timerGamePlayingMax = 180f;

    private State state;
    private float timerCountdownToStart = 3f;
    private float timerGamePlaying;
    private bool isGamePause = false;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void OnEnable()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractActionE += GameInput_OnInteractActionE;
    }

    private void GameInput_OnInteractActionE()
    {
        if (state == State.WaitingToStart)
        {
            state = State.CountdownToStart;
            OnStateChange?.Invoke();
        }
    }

    private void OnDisable()
    {
        GameInput.Instance.OnPauseAction -= GameInput_OnPauseAction;
    }
    private void GameInput_OnPauseAction()
    {
        PauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                break;
            case State.CountdownToStart:
                timerCountdownToStart -= Time.deltaTime;
                if (timerCountdownToStart < 0f)
                {
                    timerGamePlaying = timerGamePlayingMax;
                    state = State.GamePLaying;
                    OnStateChange?.Invoke();
                }
                break;
            case State.GamePLaying:

                timerGamePlaying -= Time.deltaTime;
                if (timerGamePlaying < 0f)
                {
                    state = State.GameOver;
                    OnStateChange?.Invoke();
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePLaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return timerCountdownToStart;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetTimerGamePlayingNormalized()
    {
        return 1 - (timerGamePlaying / timerGamePlayingMax);
    }

    public void PauseGame()
    {
        isGamePause = !isGamePause;

        if (isGamePause)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke();
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke();
        }
    }
}
