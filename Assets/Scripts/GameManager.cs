using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> _characters = new List<GameObject>();
    private GameObject _player_1;
    private GameObject _player_2;

    public Transform Player_1_parent;
    public Transform Player_2_parent;

    private void Start()
    {
        StartCoroutine("Instantiate_Players");
    }

    IEnumerator Instantiate_Players()
    {
        _player_1 = Instantiate(_characters[PlayerPrefs.GetInt("Selected_Character_Player_1")], new Vector3(0f, 0f, -2f), Quaternion.identity);
        _player_1.name = "PLAYER 1";
        _player_1.transform.parent = Player_1_parent;
        Player_1_parent.GetComponent<InputManager_Player>().animator = _player_1.GetComponent<Animator>();
        Player_1_parent.GetComponent<InputManager_Player>()._rigidBody_Player = _player_1.GetComponent<Rigidbody>();
        Player_1_parent.GetComponent<InputManager_Player>()._playerManager_Player = _player_1.GetComponent<PlayerManager>();

        Player_1_parent.GetComponent<InputManager_Player>().GetRotationMultiplier();


        _player_2 = Instantiate(_characters[PlayerPrefs.GetInt("Selected_Character_Player_2")], new Vector3(0f, 0f, 2f), Quaternion.Euler(0f, 180f, 0f));
        _player_2.name = "PLAYER 2";
        _player_2.transform.parent = Player_2_parent;
        Player_2_parent.GetComponent<InputManager_Player>().animator = _player_2.GetComponent<Animator>();
        Player_2_parent.GetComponent<InputManager_Player>()._rigidBody_Player = _player_2.GetComponent<Rigidbody>();
        Player_2_parent.GetComponent<InputManager_Player>()._playerManager_Player = _player_2.GetComponent<PlayerManager>();

        Player_2_parent.GetComponent<InputManager_Player>().GetRotationMultiplier();


        yield return new WaitForSeconds(0.1f);
    }
}
