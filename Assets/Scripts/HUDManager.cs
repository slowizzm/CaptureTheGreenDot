using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp_lives;
    [SerializeField] private TextMeshProUGUI tmp_level;
    [SerializeField] private TextMeshProUGUI tmp_flagIsHeld;
    public TextMeshProUGUI tmp_timer;

    private void OnEnable()
    {
        PlayerController.DispatchPlayerHasFlagEvent += ShowFlagIndicator;
        PlayerController.DispatchPlayerDroppedFlagEvent += HideFlagIndicator;
        PlayerController.DispatchPlayerDeadEvent += UpdateLivesDisplay;
    }
    private void OnDisable()
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
    //set lives text
    // not a fan of the overloading, refactor later
    public void UpdateLivesDisplay()
    {
        // lives = GameManager.lives;
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
        tmp_flagIsHeld.text = "hasFlag";
    }
    public void HideFlagIndicator(PlayerController e)
    {
        tmp_flagIsHeld.text = " ";
    }
}
