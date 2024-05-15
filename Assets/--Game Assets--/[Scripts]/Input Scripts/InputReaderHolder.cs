using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class InputReaderHolder : MonoBehaviour
{
    [SerializeField] public PlayerInput _playerInput;
    [SerializeField] PlayerInputManager _playerInputManager;
    [SerializeField] public MultiplayerEventSystem _eventSystem;
    [SerializeField] public Vector2 moveInput { get; set; }
    public bool walkBackward { get; set; }
    public bool walkForward { get; set; }
    public bool jump { get; set; }
    public bool crouch { get; set; }
    public bool lightPunch { get; set; }
    public bool hardPunch { get; set; }
    public bool lightKick { get; set; }
    public bool hardKick { get; set; }
    public bool jumpForward { get; set; }
    public bool jumpBackward { get; set; }
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.SwitchCurrentActionMap("UI");

        _eventSystem = GetComponentInChildren<MultiplayerEventSystem>();

        _playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name != "P1vsP2_Mainscene" || SceneManager.GetActiveScene().name != "P1vsCOMP_Mainscene")
        {
            if(GameManager_Old.instance.GameMode == GameType.P1vsP2)
            {
                GetControlScheme();
                SetInputEventSystemAndUI();
            }
            if(GameManager_Old.instance.GameMode == GameType.P1vsComp)
            {
                GetControlScheme();
                GameManager_Old.instance._eventSystemAI = _eventSystem;
                GameManager_Old.instance.inputReaderHolderAI = this;

            }
        }

        if(GameManager_Old.instance.GameMode == GameType.Multiplayer)
        {
            GameManager_Old.instance.inputReaderHolderA = this;
        }
    }
    private void Start()
    {
    }
    public void GetControlScheme()
    {
        if (_playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            this.gameObject.tag = "Keyboard";
            this.gameObject.name = "Keyboard_InputReaderHolder";
            _playerInput.defaultControlScheme = "Keyboard&Mouse";
        }
        else if (_playerInput.currentControlScheme == "Gamepad")
        {
            this.gameObject.tag = "Gamepad";
            this.gameObject.name = "Gamepad_InputReaderHolder";
            _playerInput.defaultControlScheme = "Gamepad";
        }
    }
    public void SetInputEventSystemAndUI()
    {
        if (_playerInput.playerIndex == 0)
        {
            _eventSystem.playerRoot = GameManager_Old.instance.uiController.playerUI[0];
            Character_Selection charSelection = _eventSystem.playerRoot.GetComponent<Character_Selection>();
            charSelection.eventSystem = _eventSystem;
            charSelection.SpawningPlayerDetails();

            GameManager_Old.instance._eventSystemsPlayer[0] = _eventSystem;
            GameManager_Old.instance.inputReaderHolderA = this;
        }
        else if (_playerInput.playerIndex == 1)
        {
            _eventSystem.playerRoot = GameManager_Old.instance.uiController.playerUI[1];
            Character_Selection charSelection = _eventSystem.playerRoot.GetComponent<Character_Selection>();
            charSelection.eventSystem = _eventSystem;
            charSelection.SpawningPlayerDetails();

            GameManager_Old.instance._eventSystemsPlayer[1] = _eventSystem;
            GameManager_Old.instance.inputReaderHolderB = this;
        }
    }

    private void Update()
    {
        if (GameManager_Old.instance.GameMode == GameType.P1vsP2)
        {
            if (_playerInputManager.playerCount == 2)
            {
                _playerInputManager.DisableJoining();

            }
        }
    }

    //Player Inputs
    public void PlayerMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (moveInput == Vector2.zero)
        {
            walkBackward = false;
            walkForward = false;
        }
        else if (moveInput.x < 0)
        {
            walkBackward = true;
            walkForward = false;
        }
        else if (moveInput.x > 0)
        {
            walkForward = true;
            walkBackward = false;
        }
    }
    public void PlayerJumpInput(InputAction.CallbackContext context)
    {
        jump = context.action.triggered;

    }
    public void PlayerCrouchInput(InputAction.CallbackContext context)
    {
        crouch = context.action.triggered;
    }
    public void PlayerLightPunchInput(InputAction.CallbackContext context)
    {
        lightPunch = context.action.triggered;
    }
    public void PlayerHardPunchInput(InputAction.CallbackContext context)
    {
        hardPunch = context.action.triggered;
    }
    public void PlayerLightKickInput(InputAction.CallbackContext context)
    {
        lightKick = context.action.triggered;
    }
    public void PlayerHardKickInput(InputAction.CallbackContext context)
    {
        hardKick = context.action.triggered;
    }
    public void PlayerJumpForewardInput(InputAction.CallbackContext context)
    {
        jumpForward = context.action.triggered;
    }
    public void PlayerJumpBackwardInput(InputAction.CallbackContext context)
    {
        jumpBackward = context.action.triggered;
    }

    //UI Inputs
    public void PlayerSelectInput(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            if(GameManager_Old.instance.GameMode == GameType.P1vsP2)
            {
                if (_playerInput.currentControlScheme == "Keyboard&Mouse")
                {
                    GameManager_Old.instance.uiController.PlayerASelected();
                    Debug.Log("Player A Selected");
                }
                else if (_playerInput.currentControlScheme == "Gamepad")
                {
                    GameManager_Old.instance.uiController.PlayerBSelected();
                    Debug.Log("Player B Selected");
                }
            }

            if(GameManager_Old.instance.GameMode == GameType.P1vsComp)
            {
                if (_playerInput.currentControlScheme == "Keyboard&Mouse")
                {
                    GameManager_Old.instance.uiController.OnSelected_Player();
                    Debug.Log("Player A Selected");
                }
                else if (_playerInput.currentControlScheme == "Gamepad")
                {
                    GameManager_Old.instance.uiController.OnSelected_Player();
                    Debug.Log("Player A Selected");
                }
            }
        }
    }
}