using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public int lives;
    public TextMeshProUGUI tmp_LifeCount;

    private void Awake()
    {
        // HUDManager.DispatchLossOfLifeEvent += RemoveLife;
    }

    void Start()
    {
        lives = GameManager.lives;
        tmp_LifeCount.text = "Lives: " + lives.ToString();
        SetLifeCounterText();
    }
    //set life count
    public void SetLifeCounterText()
    {
        if (lives > 0)
        {
            tmp_LifeCount.text = "Lives: " + lives.ToString();
        }
        else if (lives == 0)
        {
            tmp_LifeCount.text = "You ran out of lives. You'll have to start over.";
        }
    }
}
