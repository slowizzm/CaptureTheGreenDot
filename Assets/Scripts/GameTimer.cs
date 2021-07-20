using UnityEngine;
using System;
using System.Collections;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance { get; private set; }
    public float timerLength = 10f;
    public HUDManager hudManager;

    DateTime endTime;
    TimeSpan remainingTime;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        Time.timeScale = 1.0f;
    }

    private void Start()
    {
        endTime = DateTime.Now.AddSeconds(timerLength);
    }

    private void LateUpdate()
    {
        remainingTime = endTime - DateTime.Now;

        if (remainingTime.Seconds >= 0)
        {
            string displayTime = String.Format(":{1:00}", remainingTime.Minutes, remainingTime.Seconds);
            hudManager.tmp_timer.text = displayTime;
        }
    }
}

