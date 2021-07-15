using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int lives;
    public Text LifeCount;

    int level;
    GameObject player; 
    [SerializeField] float speed = 3f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lives = GI_CGD.lives;
        LifeCount.text = "Lives: " + lives.ToString();
        level = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(hInput, vInput, 0);

        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // on death remove life and restart
        if (collision.gameObject.tag == "death")
        {
            lives -= 1;
            if (lives > 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                SetLifeCounterText();
            }
            else
            {
                Destroy(player);
                SetLifeCounterText();
                SceneManager.LoadScene(0);
            }
        }

        // on captured add point and go to next level
        if (collision.gameObject.tag == "captured")
        {
            level++;
            SetLifeCounterText();
            SceneManager.LoadScene(level);
        }
    }

    void SaveGameData()
    {
        //update game instance 
        GI_CGD.lives = lives;
    }

    //display life count - move to ui manager
    void SetLifeCounterText()
    {
        if (lives > 0)
        {
            LifeCount.text = "Lives: " + lives.ToString();
        }
        else if (lives == 0)
        {
            LifeCount.text = "You ran out of lives. You'll have to start over.";
        }
    }

}