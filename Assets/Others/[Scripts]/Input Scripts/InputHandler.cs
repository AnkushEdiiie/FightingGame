using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    public Controls controls;
    //public Manager manager;
    private PlayerInput playerInput;
    public MultiplayerEventSystem InputHandEveSys;
    public Vector2 movement;
    InputActionMap AMap1;
    InputActionMap AMap2;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        //manager = FindObjectOfType<Manager>();
        //GameManager.instance.inputHandler = this;
    }
    public void OnEnable()
    {
        if (SceneManager.GetActiveScene().name != "P1vsP2_Mainscene" || SceneManager.GetActiveScene().name != "P1vsCOMP_Mainscene")
        {
            if (playerInput.playerIndex == 0)
            {
                InputHandEveSys.playerRoot = GameManager_Old.instance.uiController.playerUI[0];
                InputHandEveSys.playerRoot.GetComponent<Character_Selection>().eventSystem = InputHandEveSys;
                InputHandEveSys.playerRoot.GetComponent<Character_Selection>().SpawningPlayerDetails();
                InputHandEveSys.gameObject.SetActive(true);

                GameManager_Old.instance._eventSystemsPlayer[0] = InputHandEveSys;

                AMap1 = playerInput.actions.FindActionMap("P1");
                AMap1.Enable();
                AMap1.FindAction("OnSelected").started += ctx => GameManager_Old.instance.uiController.PlayerASelected();
                AMap1.FindAction("Join").started += ctx => GameManager_Old.instance.uiController.PlayerASelection();
                //GameManager.instance.inputReaderHolders[0] = this.gameObject;
            }
            else
            {
                //GameManager.instance.EventSystem_P1vsP2[0].gameObject.SetActive(false);

                InputHandEveSys.playerRoot = GameManager_Old.instance.uiController.playerUI[1];
                InputHandEveSys.playerRoot.GetComponent<Character_Selection>().eventSystem = InputHandEveSys;
                InputHandEveSys.playerRoot.GetComponent<Character_Selection>().SpawningPlayerDetails();
                InputHandEveSys.gameObject.SetActive(true);

                //GameManager.instance.EventSystem_P1vsP2[0].gameObject.SetActive(true);
                GameManager_Old.instance._eventSystemsPlayer[1] = InputHandEveSys;

                AMap2 = playerInput.actions.FindActionMap("P1");
                AMap2.Enable();
                AMap2.FindAction("OnSelected").started += ctx => GameManager_Old.instance.uiController.PlayerBSelected();
                AMap2.FindAction("Join").started += ctx => GameManager_Old.instance.uiController.PlayerBSelection();
                //GameManager.instance.inputReaderHolders[1] = this.gameObject;
            }
        }
    }
}
    
//    public void PlayerMovementInput(InputAction.CallbackContext context)
//    {
//        if (manager)
//        {
//            manager.MoveInput( context.ReadValue<Vector2>());
//        }
//    }

//    public void PlayerJumpInput(InputAction.CallbackContext context)
//    {
//        if (manager)
//        {
//            bool jump = context.action.triggered;
//            manager.JumpInput(jump);
//        }
//    }

//    public void PlayerCrouchInput(InputAction.CallbackContext context)
//    {
//        if (manager)
//        {
//            bool crouch = context.action.triggered;
//            manager.CrouchInput(crouch);
//        }
//    }
//    public void PlayerCrouchWalkInput(InputAction.CallbackContext context)
//    {
//        if (manager)
//        {
//            bool crouchWalk = context.action.triggered;
//            manager.CrouchWalkInput(crouchWalk);
//        }
//    }

//    public void PlayerBlockInput(InputAction.CallbackContext context)
//    {
//        if (manager)
//        {
//            bool block = context.action.triggered;
//            manager.BlockInput(block);
//        }
//    }

//    public void PlayerHardKickInput(InputAction.CallbackContext context)
//    {
//        if (manager)
//        {
//            bool hardKick = context.action.triggered;
//            manager.HardKickInput(hardKick);
//        }
//    }

//    public void PlayerLightKickInput(InputAction.CallbackContext context)
//    {
//        if (manager)
//        {
//            bool lightKick = context.action.triggered;
//            manager.LightKickInput(lightKick);
//        }
//    }

//    public void PlayerHardPunchInput(InputAction.CallbackContext context)
//    {
//        if (manager)
//        {
//            bool hardPunch = context.action.triggered;
//            manager.HardPunchInput(hardPunch);
//        }
//    }

//    public void PlayerLightPunchInput(InputAction.CallbackContext context)
//    {
//        if (manager)
//        {
//            bool lightPunch = context.action.triggered;
//            manager.LightPunchInput(lightPunch);
//        }
//    }
//}
