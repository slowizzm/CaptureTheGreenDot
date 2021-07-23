using UnityEngine;
using System;
using System.Collections;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance { get; private set; }
    public float timerLength = 10f;
    // public HUDManager hudManager;
    public float elapsedTime;

    DateTime endTime;
    TimeSpan remainingTime;
    bool bTimeIsOut = false;
    bool bCanSendEvent = true;

    public static event Action<GameTimer> DispatchTimeIsOutEvent = delegate { };
    private TimeSpan timePlaying;
    private bool bIsTimerGoing = false;
    public static bool bStartTimer = false;

    Coroutine co;


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void OnEnable()
    {
        GameManager.DispatchStartGameEvent += StartTimer;
        PlayerController.DispatchPlayerDeadEvent += StopTimer;
    }

    private void InitTimer()
    {
        elapsedTime = GameManager.time;
        endTime = DateTime.Now.AddSeconds(timerLength);

        bIsTimerGoing = true;
        bCanSendEvent = true;
        bTimeIsOut = false;
    }

    private void StartTimer<T>(T e)
    {
        InitTimer();
    }
    public void StartTimer()
    {
        InitTimer();
    }
    private void StopTimer<T>(T e)
    {
        bIsTimerGoing = false;
    }

    private void LateUpdate()
    {
        if (!bIsTimerGoing) return;

        GetElapsedTime();
        CountdownTime();

    }

    private void CountdownTime()
    {
        remainingTime = endTime - DateTime.Now;

        if (remainingTime.Seconds >= 0)
        {
            string displayTime = String.Format(":{1:00}", remainingTime.Minutes, remainingTime.Seconds);
            GameManager.timer = displayTime;
        }
        else
        {
            bTimeIsOut = true;
        }

        if (bTimeIsOut && bCanSendEvent)
        {
            // Debug.Log("out of time");
            bCanSendEvent = false;
            DispatchTimeIsOutEvent(this);
        }
    }

    private void GetElapsedTime()
    {
        elapsedTime += Time.deltaTime;
        GameManager.time = elapsedTime;
    }
}

