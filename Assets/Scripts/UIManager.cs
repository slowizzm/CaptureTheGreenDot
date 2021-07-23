using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
  public GameObject cnv_ui;
  public TextMeshProUGUI tmp_score;
  public TextMeshProUGUI tmp_time;

  public static UIManager Instance { get; private set; }
  private void Awake()
  {
    if (Instance == null) Instance = this;
  }

  // register events
  private void OnEnable()
  {
    PlayerController.DispatchPlayerDeadEvent += DisplayGameFrame;
    PlayerController.DispatchPlayerAtBaseEvent += DisplaySuccessFrame;
    GameManager.DispatchStartGameEvent += HideAllFrames;
    GameManager.DispatchEndGameEvent += DisplayStartFrame;
    GameManager.DispatchBackToStartEvent += DisplayStartFrame;
    GameManager.DispatchReloadGameEvent += HideAllFrames;
    FlagManager.DispatchSuicideLevelEvent += DisplayShowSucideFrame;
  }

  // deregister events
  private void OnDestroy()
  {
    PlayerController.DispatchPlayerDeadEvent -= DisplayGameFrame;
    PlayerController.DispatchPlayerAtBaseEvent -= DisplaySuccessFrame;
    GameManager.DispatchStartGameEvent -= HideAllFrames;
    GameManager.DispatchEndGameEvent -= DisplayStartFrame;
    GameManager.DispatchBackToStartEvent -= DisplayStartFrame;
    GameManager.DispatchReloadGameEvent -= HideAllFrames;
    FlagManager.DispatchSuicideLevelEvent -= DisplayShowSucideFrame;
  }
  private void HideAllFrames<T>(T e)
  {
    bool[] arr = { false, false, false, false };
    MenuFrameStates(arr);
  }
  private void HideAllFrames() // overload
  {
    bool[] arr = { false, false, false, false };
    MenuFrameStates(arr);
  }
  private void DisplayStartFrame<T>(T e)
  {
    bool[] arr = { true, false, false, false };
    MenuFrameStates(arr);
  }
  private void DisplayGameFrame<T>(T e)
  {
    tmp_time.text = $"Total Time: {GameManager.time.ToString()}";
    tmp_score.text = $"Dots Captured: {GameManager.level.ToString()}";
    bool[] arr = { false, true, false, false };
    MenuFrameStates(arr);
  }
  private void DisplayShowSucideFrame<T>(T e)
  {
    bool[] arr = { false, true, false, true };
    MenuFrameStates(arr);
  }
  private void DisplaySuccessFrame<T>(T e)
  {
    tmp_time.text = $"Player Time: {GameManager.time.ToString()}";
    tmp_score.text = $"Player Score: {GameManager.level.ToString()}";
    bool[] arr = { false, false, true, false };
    MenuFrameStates(arr);
  }
  // set menu frame - children of UIManager gameObject
  private void MenuFrameStates(bool[] states)
  {
    for (int i = 0; i < states.Length; i++)
    {
      transform.GetChild(i).gameObject.SetActive(states[i]);
    }
  }
  // user events
  public void StartGame()
  {
    GameManager.Instance.StartGame();
  }
  public void BackToStart()
  {
    GameManager.Instance.BackToStart();
  }
  public void RestartLevel()
  {
    GameManager.Instance.ReloadGame();
  }
  public void QuitGame()
  {
    GameManager.Instance.QuitGame();
  }
  public void StartNextLevel()
  {
    LevelManager.Instance.StartNextLevel();
    HideAllFrames();
  }
  public void EndGame()
  {
    GameManager.Instance.EndGame();
  }
}