using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public GameObject cnv_ui;
    public int lives;
    List<Transform> frames = new List<Transform>();

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(gameObject);
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        PlayerController.DispatchPlayerDeadEvent += DisplayGameMenu;
        PlayerController.DispatchPlayerAtBaseEvent += DisplaySuccessMenu;
        GameManager.DispatchStartGameEvent += HideAllMenus;
        GameManager.DispatchEndGameEvent += DisplayStartMenu;
        GameManager.DispatchRestartGameEvent += HideAllMenus;
        GameManager.DispatchReloadGameEvent += DisplayStartMenu;
    }

    private void Start()
    {
        foreach (Transform child in transform) frames.Add(child);
    }

    public void HideAllMenus<T>(T e)
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
        bool[] arr = { false, false, true };
        MenuStates(arr);
    }
    public void MenuStates(bool[] states)
    {
        ToggleHomeMenu(states[0]);
        ToggleGameMenu(states[1]);
        ToggleSuccessMenu(states[2]);
    }
    public void ToggleHomeMenu(bool state)
    {
        transform.GetChild(0).gameObject.SetActive(state);
    }

    public void ToggleGameMenu(bool state)
    {
        transform.GetChild(1).gameObject.SetActive(state);
    }
    public void ToggleSuccessMenu(bool state)
    {
        transform.GetChild(2).gameObject.SetActive(state);
    }
}