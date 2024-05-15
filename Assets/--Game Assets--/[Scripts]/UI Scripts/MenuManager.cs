using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [SerializeField] public EventSystem _eventSystemUI;
    [SerializeField] private GameObject _player;

    [Header("Menu Objects")]
    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _settingsMenuCanvas;
    [SerializeField] private GameObject _rebindGamepadMenuCanvas;
    [SerializeField] private GameObject _rebindKeyboardMenuCanvas;

    [Header("First Selected Options")]
    [SerializeField] private GameObject _mainMenuFirst;
    [SerializeField] private GameObject _settingsMenuFirst;
    [SerializeField] private GameObject _rebindGamepadFirst;
    [SerializeField] private GameObject _rebindKeyboardFirst;

    private bool isPaused;

    private void Start()
    {
        _eventSystemUI = GetComponentInChildren<EventSystem>();
        _player = GameObject.FindGameObjectWithTag("P1");
        _mainMenuCanvas.SetActive(false);
        _settingsMenuCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isPaused)
            {
                Paused();
            }
            else
            {
                UnPaused();
            }
        }
    }

    #region Pause/UnPause Functions
    public void Paused()
    {
        isPaused = true;
        Time.timeScale = 0f;
        OpenMainMenu();
        _player.SetActive(false);
    }
    public void UnPaused()
    {
        isPaused = false;
        Time.timeScale = 1f;
        CloseAllMenus();
        _player.SetActive(true);
    }
    #endregion

    #region Canvas Activation/Deactivation Functions
    private void OpenMainMenu()
    {
        _mainMenuCanvas.SetActive(true);
        _settingsMenuCanvas.SetActive(false);
        _eventSystemUI.SetSelectedGameObject(_mainMenuFirst);
    }
    private void OpenSettingsMenuHandler()
    {
        _settingsMenuCanvas.SetActive(true);
        _mainMenuCanvas.SetActive(false);
        _eventSystemUI.SetSelectedGameObject(_settingsMenuFirst);
    }
    private void OpenRebindGampadMenuHandler()
    {
        _rebindGamepadMenuCanvas.SetActive(true);
        _settingsMenuCanvas.SetActive(false);
        _eventSystemUI.SetSelectedGameObject(_rebindGamepadFirst);
    }
    private void OpenRebindKeyboardMenuHandler()
    {
        _rebindKeyboardMenuCanvas.SetActive(true);
        _settingsMenuCanvas.SetActive(false);
        _eventSystemUI.SetSelectedGameObject(_rebindKeyboardFirst);
    }
    private void CloseAllMenus()
    {
        _mainMenuCanvas.SetActive(false);
        _settingsMenuCanvas.SetActive(false);
        _eventSystemUI.SetSelectedGameObject(null);
    }
    #endregion

    #region Main Menu Buttons Actions
    public void OnSettingsPressed()
    {
        OpenSettingsMenuHandler();
    }
    public void OnResumePressed()
    {
        UnPaused();
    }
    public void OnRebindGamepadPressed()
    {
        OpenRebindGampadMenuHandler();
    }
    public void OnRebindKeyboardPressed()
    {
        OpenRebindKeyboardMenuHandler();
    }
    #endregion

    #region Settings Menu Buttons Actions
    public void OnSettingBackPressed()
    {
        OpenMainMenu();
    }
    public void OnBindingBackPressed()
    {
        _rebindGamepadMenuCanvas.SetActive(false);
        _rebindKeyboardMenuCanvas.SetActive(false);
        OpenSettingsMenuHandler();
    }
    #endregion
}