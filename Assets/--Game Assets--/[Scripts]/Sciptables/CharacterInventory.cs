using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterInventory", menuName = "Custom/CharacterInventory", order = 1)]
public class CharacterInventory : ScriptableObject
{
    public List<CharactersSO> TotalCharacters;
}
