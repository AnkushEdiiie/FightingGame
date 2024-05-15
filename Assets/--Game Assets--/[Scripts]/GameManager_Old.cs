using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class GameManager_Old : MonoBehaviour
{
    public static GameManager_Old instance;

    [Header("Variables")]
    public int _playerA_index;
    public int _playerB_index;

    public int _charA_index;
    public int _charB_index;

    public bool _playerASelected;
    public bool _playerBSelected;
    public GameType GameMode;

    [Header("Scripts References")]
    public CharacterInventory characterInventory;
    public Character_Selection characterSelection;
    public UIController uiController;

    [Header("Input Reader Holder")]
    public InputReaderHolder inputReaderHolderA;
    public InputReaderHolder inputReaderHolderB;

    public InputReaderHolder inputReaderHolderAI;

    [Header("Event System")]
    public MultiplayerEventSystem[] _eventSystemsPlayer = new MultiplayerEventSystem[2];

    public EventSystem _eventSystemAI;

    private void Awake()
    {
        if(instance == null )
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayerSelectDetails(string value)
    {
        if (value == "A_Selected") {
            _playerASelected = true;
            uiController.playerUI[0].SetActive(false);

        }

        if (value == "B_Selected") {
            _playerBSelected = true;
            uiController.playerUI[0].SetActive(false);

        }

        if (_playerASelected && _playerBSelected)
        {
            uiController.playerUI[1].SetActive(false);
            SetPlayerActionMaps();
            GameSceneManager.instance.LoadSceneWithDelay("P1vsP2_Mainscene", 5f);
        }
    }
    public void SetPlayerActionMaps()
    {
        inputReaderHolderA._playerInput.SwitchCurrentActionMap("Player");
        inputReaderHolderB._playerInput.SwitchCurrentActionMap("Player");
    }
    public void SetUIActionMap()
    {
        inputReaderHolderA._playerInput.SwitchCurrentActionMap("UI");
        inputReaderHolderB._playerInput.SwitchCurrentActionMap("UI");
    }

    public bool OnSelectPlayer()
    {
        if (_playerASelected && _playerBSelected)
            return true;
        else
            return false;
    }

}
public enum GameType
{
    None,
    P1vsP2,
    P1vsComp,
    Multiplayer
}
