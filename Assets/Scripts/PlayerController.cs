using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public int lives;
    public static event Action<PlayerController> DispatchPlayerAtBaseEvent = delegate { };
    public static event Action<PlayerController> DispatchPlayerDeadEvent = delegate { };
    public static event Action<PlayerController> DispatchRestartLevelEvent = delegate { };

    private int level;
    [SerializeField] private float speed = 15f;
    private Vector3 jump;
    private float force = 3f;
    private bool bIsOnGround = true;
    Rigidbody rb;

    void Start()
    {
        lives = GameManager.lives;
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0, force, 0);
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
            if (lives > 0)
            {
                DispatchRestartLevelEvent(this);
            }
            else
            {
                DispatchPlayerDeadEvent(this);
            }

            SaveGameData();
        }

        if (collision.gameObject.tag == "flag")
        {
            FlagManager.Instance.bIsHeld = true;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        // player and flag are at base - send dispatch
        if (collider.gameObject.tag == "base" && FlagManager.Instance.bIsHeld)
        {
            DispatchPlayerAtBaseEvent(this);
        }
    }

    void SaveGameData()
    {
        //update game instance 
        GameManager.lives = lives;
    }

}