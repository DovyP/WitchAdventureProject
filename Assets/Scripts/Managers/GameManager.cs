using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChange;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameResume;

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
    private float gameTimer;
    private float gameTimerMax = 15f;

    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;

        state = State.GameNotStarted;
    }

    private void Start()
    {
        Inputs.Instance.OnPauseAction += Inputs_OnPauseAction;
    }

    private void Inputs_OnPauseAction(object sender, EventArgs e)
    {
        ToggleGamePause();
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
                    gameTimer = gameTimerMax;
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

    public bool IsGameFinished()
    {
        return state == State.GameFinished;
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

    public float GetGameTimerNormalized()
    {
        return 1 - (gameTimer / gameTimerMax);
    }

    public void ToggleGamePause()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePause?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameResume?.Invoke(this, EventArgs.Empty);
        }
    }
}
