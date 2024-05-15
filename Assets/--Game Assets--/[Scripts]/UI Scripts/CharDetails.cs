using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharDetails : MonoBehaviour
{
    public int index;
    public CharactersSO charDetails;
    public Character_Selection characterSelectionScreen;
    public Image character_icon;
    public void SelectPlayer(bool isSelected)
    {
        if (isSelected)
            characterSelectionScreen.SelectedPlayer(charDetails,this);
    }
}
