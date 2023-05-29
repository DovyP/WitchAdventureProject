using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public event EventHandler OnStateChange;

    private enum State
    {
        GameNotStarted,
        GameStarting,
        GameInProgress,
        GameFinished,
    }

    private State state;

    private float startTimer = 1f;
    private float startCountdown = 3f;
    private float gameTimer = 10f;

    private void Awake()
    {
        instance = this;

        state = State.GameNotStarted;
    }

    private void Update()
    {
        switch (state)
        {
            case State.GameNotStarted:
                startTimer -= Time.deltaTime;
                if(startTimer < 0f)
                {
                    state = State.GameStarting;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameStarting:
                startCountdown -= Time.deltaTime;
                if (startCountdown < 0f)
                {
                    state = State.GameInProgress;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameInProgress:
                gameTimer -= Time.deltaTime;
                if (gameTimer < 0f)
                {
                    state = State.GameFinished;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameFinished:
                break;
        }
        Debug.Log("Game state " + state);
    }

    public bool IsGameInProgress()
    {
        return state == State.GameInProgress;
    }

    public bool IsGameStarting()
    {
        return state == State.GameStarting;
    }

    public float GetStartCountdown()
    {
        return startCountdown;
    }
}
