using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public int lives;
    public float health;
    public HealthBar healthBar;
    public static event Action<PlayerController> DispatchPlayerAtBaseEvent = delegate { };
    public static event Action<PlayerController> DispatchPlayerDeadEvent = delegate { };
    public static event Action<PlayerController> DispatchRestartLevelEvent = delegate { };
    public static event Action<PlayerController> DispatchPlayerHasFlagEvent = delegate { };
    public static event Action<PlayerController> DispatchPlayerDroppedFlagEvent = delegate { };

    [SerializeField] private float speed = 15f;
    private int level;
    private Vector3 jump;
    private float force = 1.9f;
    private const float damageAmt = 0.25f;
    private const float healAmt = 0.1f;
    private bool bIsOnGround = true;
    Rigidbody rb;

    void Start()
    {
        lives = GameManager.lives;
        health = GameManager.maxHealth;
        healthBar.SetMaxHealth(health);
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0, force, 0);
    }

    void Update()
    {
        PlayerMovement();
        PlayerJump();
    }
    // locomotion
    private void PlayerMovement()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(hInput, 0, vInput);

        transform.Translate(direction * speed * Time.deltaTime);
    }
    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && bIsOnGround)
        {
            rb.AddForce(jump * force, ForceMode.Impulse);
            bIsOnGround = false;
        }
    }
    // collision detection

    // is player on ground
    private void OnCollisionStay(Collision other)
    {
        bIsOnGround = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        // check if dead
        // on loss of life remove life and restart level
        // on death restart game
        if (collision.gameObject.tag == "death")
        {
            PlayerDead();
        }

        // player has flag - dispatch event
        if (collision.gameObject.tag == "flag")
        {
            FlagManager.Instance.bIsHeld = true;
            DispatchPlayerHasFlagEvent(this);
        }
    }
    // trigger events
    private void OnTriggerEnter(Collider collider)
    {
        // player and flag are at base - dispatch event
        if (collider.gameObject.tag == "base" && FlagManager.Instance.bIsHeld)
        {
            DispatchPlayerAtBaseEvent(this);
        }
    }
    // check if player is inside trigger collider
    private void OnTriggerStay(Collider collider)
    {
        // player taking damage - decrease game manager health and update healthbar
        if (collider.gameObject.tag == "damage")
        {
            if (health >= 0)
            {
                health -= damageAmt;
                Debug.Log("Taking Damage");

                healthBar.SetHealth(health);
            }
            else // player is dead
            {
                PlayerDead();
            }
        }
        // player healing - increase game manager health and update healthbar
        if (collider.gameObject.tag == "heal")
        {
            if (health <= GameManager.maxHealth)
            {
                health += healAmt;
                Debug.Log("Healing");
                healthBar.SetHealth(health);
                UpdatePlayerHealthData();
            }
        }
    }
    // utility methods
    private void PlayerDead()
    {
        lives -= 1;
        UpdatePlayerLivesData();
        // if has lives left:
        // dispatch event
        // move player to main scene
        // add mark for destroy script
        // remove this script
        if (lives > 0)
        {
            DispatchRestartLevelEvent(this);
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName("Main"));
            gameObject.AddComponent<MarkForDestroy>();
            Destroy(GetComponent<PlayerController>());
        }
        else //// player dead - dispatch event
        {
            Destroy(GetComponent<Rigidbody>());
            // health = GameManager.maxHealth;
            DispatchPlayerDeadEvent(this);
        }
    }
    // update game instance methods
    void UpdatePlayerLivesData()
    {
        GameManager.lives = lives;
    }
    void UpdatePlayerHealthData()
    {
        GameManager.health = health;
    }
}