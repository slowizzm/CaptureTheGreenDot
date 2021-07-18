using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public int lives;
    public TextMeshProUGUI tmp_lives;
    public int score = 0;
    public TextMeshProUGUI tmp_score;
    public bool bFlagIsHeld = false;
    public TextMeshProUGUI tmp_flagIsHeld;

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
        tmp_score.text = "Score: " + score.ToString();
        UpdateLivesDisplay();
        UpdateScoreDisplay();
    }
    //set life count
    public void UpdateLivesDisplay()
    {
        // lives = GameManager.lives;
        if (GameManager.lives > 0)
        {
            tmp_lives.text = $"Lives: {GameManager.lives.ToString()}";
        }
        else if (GameManager.lives == 0)
        {
            tmp_lives.text = "You ran out of lives. You'll have to start over.";
        }
    }
    public void UpdateLivesDisplay(PlayerController e)
    {
        // lives = GameManager.lives;
        tmp_lives.text = $"Lives: {GameManager.lives.ToString()}";
    }
    public void UpdateScoreDisplay()
    {
        tmp_score.text = $"Score: {GameManager.score}";
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
