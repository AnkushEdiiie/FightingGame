using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Custom/CharacterSO", order = 2)]
public class CharactersSO : ScriptableObject
{
    public string characterName;
    public Sprite characterImage;
    public GameObject SP_Prefab;
    public GameObject MP_Prefab;
    public GameObject AI_Prefab;
    public GameObject Select_Prefab;
}
