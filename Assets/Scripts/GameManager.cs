using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int lives;
    public static int maxLives = 3;
    public static float health;
    public static float maxHealth = 100;
    public static int score = 0;
    public static int level = 1;
    public static int maxLevels = 2;
    public static float time;
    public static string timer = "0";

    // events
    public static event Action<GameManager> DispatchStartGameEvent = delegate { };
    public static event Action<GameManager> DispatchEndGameEvent = delegate { };
    public static event Action<GameManager> DispatchReloadGameEvent = delegate { };

    // Game Instance
    public static GameManager Instance { get; private set; }

    // cache instance - set to not destroy on load
    private void Awake()
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // init settings
    private void InitGame()
    {
        lives = maxLives;
        score = 0;
        level = 1;
        health = maxHealth;
        time = 0;
    }

    // user events - from UIManager
    public void StartGame()
    {
        InitGame();
        DispatchStartGameEvent(this);
    }
    public void EndGame()
    {
        DispatchEndGameEvent(this);
    }
    public void ReloadGame()
    {
        InitGame();
        DispatchReloadGameEvent(this);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}