using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private bool bIsGameMenu = false;
    public float camRotY;

    private void Awake()
    {
        camRotY = Camera.main.transform.localEulerAngles.y;

    }
    private void OnEnable()
    {
        PlayerController.DispatchPlayerDeadEvent += EnableRotateAround;
        GameManager.DispatchStartGameEvent += ResetCameraRotation;
    }
    private void OnEnableDisable()
    {
        PlayerController.DispatchPlayerDeadEvent -= EnableRotateAround;
        GameManager.DispatchStartGameEvent -= ResetCameraRotation;
    }
    void Update()
    {
        if (bIsGameMenu)
        {
            transform.RotateAround(Vector3.zero, Vector3.up, 10 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.K))
        {
            transform.RotateAround(Vector3.zero, Vector3.up, 10 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.L))
        {
            transform.RotateAround(Vector3.zero, Vector3.up, -10 * Time.deltaTime);
        }
    }
    private void EnableRotateAround<T>(T e)
    {
        bIsGameMenu = true;
    }
    private void ResetCameraRotation<T>(T e)
    {
        bIsGameMenu = false;
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
