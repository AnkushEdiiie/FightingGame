using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    [SerializeField] public GameObject[] playerUI;
    [SerializeField] public TMP_Text NameA;
    [SerializeField] public TMP_Text NameB;
    [SerializeField] public int PlayerA_index;
    [SerializeField] public int PlayerB_index;
    [SerializeField] public InputActionReference onSelect;
    [SerializeField] private Button leftSide;
    [SerializeField] private Button rightSide;


    private void Awake()
    {
        GameManager_Old.instance.uiController = this;
    }

    public void PlayerASelection()
    {
        if (GameManager_Old.instance.GameMode == GameType.P1vsP2)
        {
            playerUI[0].SetActive(true);
            playerUI[1].SetActive(false);
        }
        if (GameManager_Old.instance.GameMode == GameType.P1vsComp)
        {
            playerUI[0].SetActive(true);
            playerUI[1].SetActive(false);
            GameManager_Old.instance.characterSelection.SpawningPlayerDetails();
            GameManager_Old.instance._eventSystemAI.SetSelectedGameObject(playerUI[0].transform.GetChild(0).gameObject);
        }
    }

    public void PlayerBSelection()
    {
        if (GameManager_Old.instance.GameMode == GameType.P1vsP2)
        {
            playerUI[0].SetActive(false);
            playerUI[1].SetActive(true);
        }
        if (GameManager_Old.instance.GameMode == GameType.P1vsComp)
        {
            playerUI[0].SetActive(false);
            playerUI[1].SetActive(true);
            GameManager_Old.instance.characterSelection.SpawningPlayerDetails();
            GameManager_Old.instance._eventSystemAI.SetSelectedGameObject(playerUI[1].transform.GetChild(0).gameObject);
        }
    }

    public void PlayerASelected()
    {
        GameManager_Old.instance.SetPlayerSelectDetails("A_Selected");

        foreach (Transform items in playerUI[0].transform)
        {
            items.GetComponent<Button>().interactable = false;
        }
    }

    public void PlayerBSelected()
    {
        GameManager_Old.instance.SetPlayerSelectDetails("B_Selected");

        foreach (Transform items in playerUI[1].transform)
        {
            items.GetComponent<Button>().interactable = false;
        }
    }

    public void OnSelected_Player()
    {
        if (GameManager_Old.instance.GameMode == GameType.P1vsComp)
        {
            if (!GameManager_Old.instance._playerASelected)
            {
                GameManager_Old.instance._playerASelected = true;
                PlayerBSelection();
            }
            else if (GameManager_Old.instance._playerASelected)
                GameManager_Old.instance._playerBSelected = true;

            if (GameManager_Old.instance.OnSelectPlayer())
            {
                GameManager_Old.instance.inputReaderHolderAI._playerInput.SwitchCurrentActionMap("Player");
                GameSceneManager.instance.LoadSceneWithDelay("P1vsComp_Mainscene", 5f);
            }
        }
    }


}
