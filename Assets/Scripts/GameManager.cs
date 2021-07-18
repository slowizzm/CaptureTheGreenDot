using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int lives = 3;
    public static int level;
    [HideInInspector] public UIManager uiManager;
    [HideInInspector] public FlagManager flagManager;
    public static event Action<GameManager> DispatchStartGameEvent = delegate { };
    public static event Action<GameManager> DispatchEndGameEvent = delegate { };
    public static event Action<GameManager> DispatchRestartGameEvent = delegate { };
    public static event Action<GameManager> DispatchReloadGameEvent = delegate { };

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(gameObject);
        uiManager = UIManager.Instance;
        flagManager = FlagManager.Instance;
    }

    public void StartGame()
    {
        lives = 3;
        DispatchStartGameEvent(this);
    }

    public void RestartGame()
    {
        lives = 3;
        DispatchRestartGameEvent(this);
    }

    public void EndGame()
    {
        DispatchEndGameEvent(this);
    }

    public void ReloadGame()
    {
        DispatchReloadGameEvent(this);
    }
}