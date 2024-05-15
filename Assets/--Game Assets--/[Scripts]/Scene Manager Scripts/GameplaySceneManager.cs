using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class GameplaySceneManager : MonoBehaviour
{
    [SerializeField] CinemachineTargetGroup _cineTargetGroup;

    private GameObject _playerAPrefab;
    private GameObject _playerBPrefab;
    private InputReader _inputReaderPlayerA;
    private InputReader _inputReaderPlayerB;

    private bool isSet = false;

    private void Awake()
    {
        if(GameManager_Old.instance.GameMode == GameType.P1vsP2)
        {
            PlayerVSPlayer();
        }
        if(GameManager_Old.instance.GameMode == GameType.P1vsComp)
        {
            PlayerVsAI();
        }
    }

    public void PlayerVSPlayer()
    {
        _playerAPrefab = GameManager_Old.instance.characterInventory.TotalCharacters[GameManager_Old.instance._playerA_index].SP_Prefab;
        _playerBPrefab = GameManager_Old.instance.characterInventory.TotalCharacters[GameManager_Old.instance._playerB_index].SP_Prefab;

        _inputReaderPlayerA = Instantiate(_playerAPrefab, new Vector3(0, 0, -1), Quaternion.identity).GetComponent<InputReader>();
        _inputReaderPlayerB = Instantiate(_playerBPrefab, new Vector3(0, 0, 1), Quaternion.Euler(0, 180, 0)).GetComponent<InputReader>();

        _inputReaderPlayerA._inputReaderHolder = GameManager_Old.instance.inputReaderHolderA;
        _inputReaderPlayerB._inputReaderHolder = GameManager_Old.instance.inputReaderHolderB;

        _playerAPrefab.tag = "P1";
        _playerBPrefab.tag = "P2";

        if (_playerAPrefab && _playerBPrefab)
        {
            _cineTargetGroup.AddMember(_inputReaderPlayerA.transform, 1, 1);
            _cineTargetGroup.AddMember(_inputReaderPlayerB.transform, 1, 1);
            _cineTargetGroup.transform.rotation = Quaternion.Euler(0, 90, 0);
            isSet = true;
        }

    }

    public void PlayerVsAI()
    {
        _playerAPrefab = Instantiate(GameManager_Old.instance.characterInventory.TotalCharacters[GameManager_Old.instance._playerA_index].SP_Prefab, new Vector3(0, 0, -1), Quaternion.identity);
        _playerBPrefab = Instantiate(GameManager_Old.instance.characterInventory.TotalCharacters[GameManager_Old.instance._playerB_index].AI_Prefab, new Vector3(0, 0, 1), Quaternion.Euler(0, 180, 0));

        _playerAPrefab.GetComponent<InputReader>()._inputReaderHolder = GameManager_Old.instance.inputReaderHolderAI;

        _playerAPrefab.tag = "P1";
        _playerBPrefab.tag = "P2";

        if (_playerAPrefab && _playerBPrefab)
        {
            _cineTargetGroup.AddMember(_playerAPrefab.transform, 1, 1);
            _cineTargetGroup.AddMember(_playerBPrefab.transform, 1, 1);
            _cineTargetGroup.transform.rotation = Quaternion.Euler(0, 90, 0);
            isSet = true;
        }
    }
}