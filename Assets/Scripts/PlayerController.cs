using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public int lives;
    public static event Action<PlayerController> DispatchPlayerAtBaseEvent = delegate { };
    public static event Action<PlayerController> DispatchPlayerDeadEvent = delegate { };
    public static event Action<PlayerController> DispatchRestartLevelEvent = delegate { };
    public static event Action<PlayerController> DispatchPlayerHasFlagEvent = delegate { };
    public static event Action<PlayerController> DispatchPlayerDroppedFlagEvent = delegate { };

    private int level;
    [SerializeField] private float speed = 15f;
    private Vector3 jump;
    private float force = 1.9f;
    private bool bIsOnGround = true;
    Rigidbody rb;

    void Start()
    {
        lives = GameManager.lives;
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0, force, 0);
    }

    private void OnDisable()
    {
        // SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName("Main"));
        // Destroy(GetComponent<PlayerController>());
    }

    private void OnCollisionStay(Collision other)
    {
        bIsOnGround = true;
    }
    void Update()
    {
        PlayerMovement();
        PlayerJump();
    }
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
    private void OnCollisionEnter(Collision collision)
    {
        // on death remove life and restart
        if (collision.gameObject.tag == "death")
        {
            lives -= 1;
            UpdateGameLivesData();
            if (lives > 0)
            {
                DispatchRestartLevelEvent(this);
                // StartCoroutine(DelayLevelRestart());
                SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName("Main"));
                gameObject.AddComponent<MarkForDestroy>();
                Destroy(GetComponent<PlayerController>());
            }
            else
            {
                DispatchPlayerDeadEvent(this);
            }
        }

        if (collision.gameObject.tag == "flag")
        {
            FlagManager.Instance.bIsHeld = true;
            DispatchPlayerHasFlagEvent(this);
        }
    }
    IEnumerator DelayLevelRestart()
    {
        yield return new WaitForSeconds(1);
        DispatchRestartLevelEvent(this);
    }
    private void OnTriggerEnter(Collider collider)
    {
        // player and flag are at base - send dispatch
        if (collider.gameObject.tag == "base" && FlagManager.Instance.bIsHeld)
        {
            UpdateGameScoreData();
            DispatchPlayerAtBaseEvent(this);
        }
    }
    //update game instance 
    void UpdateGameLivesData()
    {
        GameManager.lives = lives;
    }
    void UpdateGameScoreData()
    {
        GameManager.score += 1;
    }
}