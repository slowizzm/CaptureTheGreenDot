using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public GameObject cnv_ui;
    public int lives;
    private int currentScene;

    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        PlayerController.DispatchPlayerDeadEvent += DisplayGameMenu;
        PlayerController.DispatchPlayerAtBaseEvent += DisplaySuccessMenu;
        GameManager.DispatchStartGameEvent += HideAllMenus;
        GameManager.DispatchEndGameEvent += DisplayStartMenu;
        GameManager.DispatchRestartLevelEvent += HideAllMenus;
        GameManager.DispatchReloadGameEvent += HideAllMenus;
    }
    private void OnDisable()
    {
        PlayerController.DispatchPlayerDeadEvent -= DisplayGameMenu;
        PlayerController.DispatchPlayerAtBaseEvent -= DisplaySuccessMenu;
        GameManager.DispatchStartGameEvent -= HideAllMenus;
        GameManager.DispatchEndGameEvent -= DisplayStartMenu;
        GameManager.DispatchRestartLevelEvent -= HideAllMenus;
        GameManager.DispatchReloadGameEvent -= HideAllMenus;
    }
    public void HideAllMenus<T>(T e)
    {
        bool[] arr = { false, false, false };
        MenuStates(arr);
    }
    public void HideAllMenus() // overload
    {
        bool[] arr = { false, false, false };
        MenuStates(arr);
    }
    private void DisplayStartMenu<T>(T e)
    {
        bool[] arr = { true, false, false };
        MenuStates(arr);
    }
    public void DisplayGameMenu<T>(T e)
    {
        bool[] arr = { false, true, false };
        MenuStates(arr);
    }
    private void DisplaySuccessMenu<T>(T e)
    {
        dsm();
    }

    private void dsm()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings)
        {
            bool[] arr = { false, false, true };
            MenuStates(arr);
        }
        else
        {
            bool[] arr = { false, false, true };
            MenuStates(arr);
        }
    }
    public void MenuStates(bool[] states)
    {
        ToggleStartMenu(states[0]);
        ToggleFailMenu(states[1]);
        ToggleSuccessMenu(states[2]);
    }
    public void ToggleStartMenu(bool state)
    {
        transform.GetChild(0).gameObject.SetActive(state);
    }
    public void ToggleFailMenu(bool state)
    {
        transform.GetChild(1).gameObject.SetActive(state);
    }
    public void ToggleSuccessMenu(bool state)
    {
        transform.GetChild(2).gameObject.SetActive(state);
    }

    // user events
    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
    public void EndGame()
    {
        GameManager.Instance.EndGame();
    }
    public void RestartLevel()
    {
        GameManager.Instance.ReloadGame();
    }
    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
    public void StartNextLevel()
    {
        LevelManager.Instance.StartNextLevel();
        HideAllMenus();
    }
}