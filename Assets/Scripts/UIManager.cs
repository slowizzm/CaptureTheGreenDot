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
    public TextMeshProUGUI tmp_score;
    public TextMeshProUGUI tmp_time;

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
        GameManager.DispatchReloadGameEvent += HideAllMenus;
        FlagManager.DispatchSuicideLevelEvent += DisplayShowSucideMenu;
    }
    private void OnDestroy()
    {
        PlayerController.DispatchPlayerDeadEvent -= DisplayGameMenu;
        PlayerController.DispatchPlayerAtBaseEvent -= DisplaySuccessMenu;
        GameManager.DispatchStartGameEvent -= HideAllMenus;
        GameManager.DispatchEndGameEvent -= DisplayStartMenu;
        GameManager.DispatchReloadGameEvent -= HideAllMenus;
        FlagManager.DispatchSuicideLevelEvent -= DisplayShowSucideMenu;
    }
    private void HideAllMenus<T>(T e)
    {
        bool[] arr = { false, false, false, false, false };
        MenuStates(arr);
    }
    private void HideAllMenus() // overload
    {
        bool[] arr = { false, false, false, false, false };
        MenuStates(arr);
    }
    private void DisplayStartMenu<T>(T e)
    {
        bool[] arr = { true, false, false, false, false };
        MenuStates(arr);
    }
    private void DisplayGameMenu<T>(T e)
    {
        tmp_time.text = $"Total Time: {GameManager.time.ToString()}";
        tmp_score.text = $"Dots Captured: {GameManager.level.ToString()}";
        bool[] arr = { false, true, false, false, false };
        MenuStates(arr);
    }
    private void DisplayShowSucideMenu<T>(T e)
    {
        bool[] arr = { false, true, false, false, true };
        MenuStates(arr);
    }
    private void DisplaySuccessMenu<T>(T e)
    {
        tmp_time.text = $"Player Time: {GameManager.time.ToString()}";
        tmp_score.text = $"Player Score: {GameManager.level.ToString()}";
        bool[] arr = { false, false, true, false, false };
        MenuStates(arr);
        // CheckIfShowSuccessOrCompletedFrame();
    }

    private void CheckIfShowSuccessOrCompletedFrame()
    {
        if (LevelManager.currentLevel >= GameManager.maxLevels)
        {
            bool[] arr = { false, false, false, true, false };
            MenuStates(arr);
            // tmp_time.text = $"Player Time: {GameManager.time.ToString()}";
            // tmp_score.text = $"Player Score: {GameManager.level.ToString()}";
        }
        else
        {
            bool[] arr = { false, false, true, false, false };
            MenuStates(arr);
        }
    }
    private void MenuStates(bool[] states)
    {
        ToggleStartFrame(states[0]);
        ToggleFailFrame(states[1]);
        ToggleSuccessFrame(states[2]);
        ToggleCompletedFrame(states[3]);
        ToggleShowSucideMenu(states[4]);
    }
    private void ToggleStartFrame(bool state)
    {
        transform.GetChild(0).gameObject.SetActive(state);
    }
    private void ToggleFailFrame(bool state)
    {
        transform.GetChild(1).gameObject.SetActive(state);
    }
    private void ToggleSuccessFrame(bool state)
    {
        transform.GetChild(2).gameObject.SetActive(state);
    }
    private void ToggleCompletedFrame(bool state)
    {
        transform.GetChild(3).gameObject.SetActive(state);
    }
    private void ToggleShowSucideMenu(bool state)
    {
        transform.GetChild(4).gameObject.SetActive(state);
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

    public void GoToStartMenu()
    {
        bool[] arr = { true, false, false, false };
        MenuStates(arr);
    }
}