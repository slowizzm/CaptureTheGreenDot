using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
  public static LevelManager Instance { get; private set; }
  public static int currentLevel;

  private void Awake()
  {
    if (Instance == null) Instance = this;
  }
  private void OnEnable()
  {
    GameManager.DispatchStartGameEvent += LoadLevel;
    GameManager.DispatchReloadGameEvent += ReloadLevel;
    PlayerController.DispatchPlayerLifeLostEvent += RestartLevel;
    PlayerController.DispatchPlayerAtBaseEvent += PlayerCapturedFlag;
  }
  private void OnDestroy()
  {
    GameManager.DispatchStartGameEvent -= LoadLevel;
    GameManager.DispatchReloadGameEvent -= ReloadLevel;
    PlayerController.DispatchPlayerLifeLostEvent -= RestartLevel;
    PlayerController.DispatchPlayerAtBaseEvent -= PlayerCapturedFlag;
  }
  void Start()
  {
    currentLevel = GameManager.level;
  }
  private void PlayerCapturedFlag(PlayerController e)
  {
    Debug.Log("captured the dot");
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
    GameTimer.Instance.StartTimer();
  }
  public void RestartLevel()
  {
    GameManager.health = GameManager.maxHealth;
    SceneManager.SetActiveScene(SceneManager.GetSceneByName("Main"));
    SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
    GameTimer.Instance.StartTimer();
  }
  public void StartNextLevel()
  {
    currentLevel++;
    GameManager.level = currentLevel;
    GameManager.health = GameManager.maxHealth;
    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
    GameTimer.Instance.StartTimer();
  }
  public void ReloadLevel<T>(T e)
  {
    currentLevel = 1;
    GameManager.level = currentLevel;
    GameManager.health = GameManager.maxHealth;
    SceneManager.SetActiveScene(SceneManager.GetSceneByName("Main"));
    SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
    GameTimer.Instance.StartTimer();
  }
}
