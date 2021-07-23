using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI tmp_lives;
  [SerializeField] private TextMeshProUGUI tmp_level;
  [SerializeField] private TextMeshProUGUI tmp_flagIsHeld;
  public TextMeshProUGUI tmp_timer;

  // register events
  private void OnEnable()
  {
    PlayerController.DispatchPlayerHasFlagEvent += ShowFlagIndicator;
    PlayerController.DispatchPlayerDroppedFlagEvent += HideFlagIndicator;
    PlayerController.DispatchPlayerDeadEvent += UpdateLivesDisplay;
  }
  // deregister events
  private void OnDestroy()
  {
    PlayerController.DispatchPlayerHasFlagEvent -= ShowFlagIndicator;
    PlayerController.DispatchPlayerDroppedFlagEvent -= HideFlagIndicator;
    PlayerController.DispatchPlayerDeadEvent -= UpdateLivesDisplay;
  }

  void Start()
  {
    // lives = GameManager.lives;
    tmp_lives.text = "Lives: " + GameManager.lives.ToString();
    tmp_level.text = "Score: " + GameManager.level.ToString();
    UpdateLivesDisplay();
    UpdateScoreDisplay();
  }
  private void Update()
  {
    tmp_timer.text = GameManager.timer;
    // Debug.Log(GameManager.timer);
  }
  //set lives text
  // not a fan of the overloading, refactor later
  public void UpdateLivesDisplay()
  {
    if (GameManager.lives > 0)
    {
      tmp_lives.text = $"Lives: {GameManager.lives.ToString()}";
    }
  }
  public void UpdateLivesDisplay(PlayerController e)
  {
    tmp_lives.text = $"Lives: 0";
  }
  public void UpdateScoreDisplay()
  {
    tmp_level.text = $"Level: {GameManager.level}";
  }

  public void ShowFlagIndicator(PlayerController e)
  {
    tmp_flagIsHeld.text = "Dot Captured";
  }
  public void HideFlagIndicator(PlayerController e)
  {
    tmp_flagIsHeld.text = " ";
  }
}
