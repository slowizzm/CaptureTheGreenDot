using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
  public int lives;
  public float health;
  public HealthBar healthBar;
  public static event Action<PlayerController> DispatchPlayerAtBaseEvent = delegate { };
  public static event Action<PlayerController> DispatchPlayerDeadEvent = delegate { };
  public static event Action<PlayerController> DispatchPlayerLifeLostEvent = delegate { };
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

  // register events
  private void OnEnable()
  {
    GameTimer.DispatchTimeIsOutEvent += PlayerDead;
  }
  // deregister events
  private void OnDestroy()
  {
    GameTimer.DispatchTimeIsOutEvent -= PlayerDead;
  }

  void Start()
  {
    // update this players lives, health, max health based on GamaManager
    // only valid for single player - GameManager needs updated to store a list/dic of players
    lives = GameManager.lives;
    health = GameManager.maxHealth;
    healthBar.SetMaxHealth(health);
    // ref rigidbody
    rb = GetComponent<Rigidbody>();
    // jump settings
    jump = new Vector3(0, force, 0);
  }

  void Update()
  {
    PlayerMovement();
    PlayerJump();
  }
  // locomotion - x,z and jump
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
    // check if dead
    // on loss of life remove life and restart level
    // on death restart game
    if (collider.gameObject.tag == "death")
    {
      PlayerDead();
    }
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
        // Debug.Log("Taking Damage");

        healthBar.UpdateHealth(health);
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
        // Debug.Log("Healing");
        healthBar.UpdateHealth(health);
        UpdatePlayerHealthData();
      }
    }
  }
  // utility methods
  private void PlayerDead<T>(T e)
  {
    lives -= 1;
    UpdatePlayerLivesData();
    GameTimer.DispatchTimeIsOutEvent -= PlayerDead;
    // move player to main scene
    // add mark for destroy script
    // remove this script
    SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName("Main"));
    // Destroy(GetComponent<Rigidbody>());
    gameObject.AddComponent<MarkForDestroy>();
    Destroy(gameObject.transform.GetChild(0).gameObject);
    Destroy(GetComponent<PlayerController>());

    // if has lives left:
    // dispatch life lost event
    if (lives > 0)
    {
      DispatchPlayerLifeLostEvent(this);
    }
    else //// player dead - dispatch event
    {
      DispatchPlayerDeadEvent(this);
    }
  }
  private void PlayerDead()
  {
    lives -= 1;
    UpdatePlayerLivesData();
    GameTimer.DispatchTimeIsOutEvent -= PlayerDead;
    // move player to main scene
    // add mark for destroy script
    // remove this script
    SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName("Main"));
    // Destroy(GetComponent<Rigidbody>());
    gameObject.AddComponent<MarkForDestroy>();
    Destroy(gameObject.transform.GetChild(0).gameObject);
    Destroy(GetComponent<PlayerController>());
    gameObject.tag = "death";

    // if has lives left:
    // dispatch event
    if (lives > 0)
    {
      DispatchPlayerLifeLostEvent(this);
    }
    else //// player dead - dispatch event
    {
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