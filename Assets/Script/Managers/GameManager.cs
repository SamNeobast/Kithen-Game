using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   
    public static GameManager Instance { get; private set; }

    public event Action OnStateChange;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePLaying,
        GameOver,
    }

    private State state;
    private float timerWaitingToStart = 1f;
    private float timerCountdownToStart = 3f;
    private float timerGamePlaying;
    private float timerGamePlayingMax = 15f;


    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                timerWaitingToStart -= Time.deltaTime;
                if (timerWaitingToStart < 0f)
                {
                    state = State.CountdownToStart;
                    OnStateChange?.Invoke();
                }
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
        Debug.Log(state);
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
}
