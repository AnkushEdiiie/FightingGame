using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class Character_Selection : MonoBehaviour
{
    public CharacterInventory character_inventory;
    public Button character_btn;
    public Transform leftPlayerPos;
    public Transform rightPlayerPos;

    public MultiplayerEventSystem eventSystem;

    public Image characterIcon;

    public ControlType controlType;
    public UIController UiController;

    public bool isEventSystem;

    private void Awake()
    {
        GameManager_Old.instance.characterSelection = this;

        if (controlType == ControlType.System)
        {
            foreach (CharactersSO item in character_inventory.TotalCharacters)
            {
                Image charInfo = Instantiate(characterIcon, transform).GetComponent<Image>();
                charInfo.sprite = item.characterImage;
            }
        }
    }
    public void SpawningPlayerDetails()
    {
        if (controlType == ControlType.P1 || controlType == ControlType.P2)
        {

            for (int i = 0; i < character_inventory.TotalCharacters.Count; i++)
            {
                var item = character_inventory.TotalCharacters[i];
                CharDetails charInfo = Instantiate(character_btn.gameObject, transform).GetComponent<CharDetails>();
                charInfo.character_icon.sprite = null;
                charInfo.characterSelectionScreen = this;
                charInfo.charDetails = item;
                charInfo.index = i;
                charInfo.name = i.ToString();
                if (i == 0)
                {
                    charInfo.SelectPlayer(true);
                }
                if (GameManager_Old.instance.GameMode == GameType.P1vsP2 && !isEventSystem)
                {
                    eventSystem.SetSelectedGameObject(charInfo.gameObject);

                    isEventSystem = true;
                }
            }
        }
    }
    public void SelectedPlayer(CharactersSO charSO, CharDetails details)
    {
        if (controlType == ControlType.P1)
        {
            spawnCharacter_On_SelectScreen(charSO, leftPlayerPos);
            UiController.NameA.text = charSO.characterName;
            GameManager_Old.instance._playerA_index = details.index;
            GameManager_Old.instance._charA_index = 1;
        }
        if (controlType == ControlType.P2)
        {
            spawnCharacter_On_SelectScreen(charSO, rightPlayerPos);
            UiController.NameB.text = charSO.characterName;
            GameManager_Old.instance._playerB_index = details.index;
            GameManager_Old.instance._charB_index = 2;
        }
        if (controlType == ControlType.AI)
        {
            spawnCharacter_On_SelectScreen(charSO, rightPlayerPos);
            UiController.NameB.text = charSO.characterName;
            GameManager_Old.instance._playerB_index = 2;
        }
    }

    private void spawnCharacter_On_SelectScreen(CharactersSO charSO, Transform pos)
    {
        foreach (Transform i in pos)
            Destroy(i.gameObject);

        Instantiate(charSO.Select_Prefab, pos);
    }
    public enum ControlType
    {
        System,
        P1,
        P2,
        AI,
    }
}
