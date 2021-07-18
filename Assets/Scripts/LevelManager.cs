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
        PlayerController.DispatchPlayerAtBaseEvent += E_AtBaseEvent;
        GameManager.DispatchStartGameEvent += LoadLevel;
        GameManager.DispatchRestartGameEvent += UnloadLevel;
        GameManager.DispatchRestartGameEvent += ReloadLevel;
        PlayerController.DispatchRestartLevelEvent += ReloadLevel;
        GameManager.DispatchEndGameEvent += UnloadLevel;
    }
    private void OnDisable()
    {
        PlayerController.DispatchPlayerAtBaseEvent -= E_AtBaseEvent;
        GameManager.DispatchStartGameEvent -= LoadLevel;
        GameManager.DispatchRestartGameEvent -= UnloadLevel;
        GameManager.DispatchRestartGameEvent -= ReloadLevel;
        PlayerController.DispatchRestartLevelEvent -= ReloadLevel;
        GameManager.DispatchEndGameEvent -= UnloadLevel;
    }
    private void E_AtBaseEvent(PlayerController e)
    {
        Debug.Log("you win");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Main"));
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }
    public void ReloadLevel<T>(T e)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
    }
    public void UnloadLevel<T>(T e)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadLevel(GameManager e)
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }
}
