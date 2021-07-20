using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    private int currentLevel;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void OnEnable()
    {
        GameManager.DispatchStartGameEvent += LoadLevel;
        GameManager.DispatchEndGameEvent += UnloadLevel;
        GameManager.DispatchReloadGameEvent += RestartLevel;
        PlayerController.DispatchRestartLevelEvent += RestartLevel;
        PlayerController.DispatchPlayerAtBaseEvent += E_AtBaseEvent;
    }
    private void OnDisable()
    {
        GameManager.DispatchStartGameEvent -= LoadLevel;
        GameManager.DispatchEndGameEvent -= UnloadLevel;
        GameManager.DispatchReloadGameEvent -= RestartLevel;
        PlayerController.DispatchRestartLevelEvent -= RestartLevel;
        PlayerController.DispatchPlayerAtBaseEvent -= E_AtBaseEvent;
    }
    void Start()
    {
        currentLevel = GameManager.level;
    }
    private void E_AtBaseEvent(PlayerController e)
    {
        Debug.Log("congratz: reached end of level");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Main"));
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadLevel(GameManager e)
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
    }
    public void UnloadLevel<T>(T e)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void RestartLevel<T>(T e)
    {
        GameManager.health = GameManager.maxHealth;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Main"));
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
    }
    public void StartNextLevel()
    {
        currentLevel++;
        GameManager.level = currentLevel;
        GameManager.health = GameManager.maxHealth;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
    }
}
