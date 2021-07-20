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
    [HideInInspector] public UIManager uiManager;
    [HideInInspector] public FlagManager flagManager;
    public static event Action<GameManager> DispatchStartGameEvent = delegate { };
    public static event Action<GameManager> DispatchEndGameEvent = delegate { };
    public static event Action<GameManager> DispatchRestartLevelEvent = delegate { };
    public static event Action<GameManager> DispatchReloadGameEvent = delegate { };

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(gameObject);
        uiManager = UIManager.Instance;
        flagManager = FlagManager.Instance;
    }
    private void InitGame()
    {
        lives = maxLives;
        score = 0;
        level = 1;
        health = maxHealth;
    }
    public void StartGame()
    {
        InitGame();
        DispatchStartGameEvent(this);
    }
    // public void RestartLevel()
    // {
    //     lives = maxLives;
    //     health = maxHealth;
    //     DispatchRestartLevelEvent(this);
    // }
    // public void StartNextLevel()
    // {
    //     lives = maxLives;
    //     health = maxHealth;
    //     DispatchRestartLevelEvent(this);
    // }
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