using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager_Player _inputManager_Player;
    private GameManager _gamemanager;

    public Characters_Data characterData;

    //public List<Collider> _colliders = new List<Collider>();

    public Collider _leftHand_Collider;
    public Collider _rightHand_Collider;
    public Collider _leftLeg_Collider;
    public Collider _rightLeg_Collider;

    public List<Collider> _tempColliders = new List<Collider>();

    private void Start()
    {
        if(GameObject.Find("Game_Manager"))
            _gamemanager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        _inputManager_Player = gameObject.GetComponentInParent<InputManager_Player>();
    }

    public void EnableColliderForPunch(bool _enable, int _playerIndex)
    {
        _rightHand_Collider.enabled = _enable;
        _tempColliders.Add(_rightHand_Collider);
    }

    public void EnableColliderForKick(bool _enable, int _playerIndex)
    {
        _leftLeg_Collider.enabled = _enable;
        _tempColliders.Add(_leftLeg_Collider);
    }

    public void EnableColliderForJab(bool _enable, int _playerIndex)
    {
        _leftLeg_Collider.enabled= _enable;
        _tempColliders.Add(_leftLeg_Collider);
    }

    public void EnableColliderForUpperCut(bool _enable, int _playerIndex)
    {
        _rightHand_Collider.enabled= _enable;
        _tempColliders.Add(_rightHand_Collider);
    }

    public void EnableColliderForCrouchSweep(bool _enable, int _playerIndex)
    {
        _leftLeg_Collider.enabled = _enable;
        _tempColliders.Add(_leftLeg_Collider);
    }

    public void DisableColliders(bool _enable, int _playerIndex)
    {
        foreach (Collider _col in _tempColliders)
        {
            _col.enabled = _enable;
        }
        _tempColliders.Clear();
    }

    public void DisableAllColliders(bool _enable, int _playerIndex)
    {
        _leftHand_Collider.enabled = _enable;
        _rightHand_Collider.enabled = _enable;
        _leftLeg_Collider.enabled = _enable;
        _rightLeg_Collider.enabled = _enable;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        { 
            return; 
        }
        if(_inputManager_Player.playerIndex == 1)
        {
            Debug.Log("Player 1 Trigger Method Called" + other.gameObject);
            gameObject.GetComponent<Animator>().applyRootMotion = true;
            if (other.gameObject.GetComponentInParent<InputManager_Player>()._playerState == InputManager_Player.PlayerAnimationState.punch)
            {
                Debug.Log("Player 2 Punch");
                gameObject.GetComponent<Animator>().SetTrigger("LightHitTrigger");
                _gamemanager.HealthBar_Players_Method(1, 10f);
            }
            else if (other.gameObject.GetComponentInParent<InputManager_Player>()._playerState == InputManager_Player.PlayerAnimationState.kick)
            {
                Debug.Log("Player 2 Kick");
                gameObject.GetComponent<Animator>().SetTrigger("KnockdownTrigger");
                _gamemanager.HealthBar_Players_Method(1, 15f);
            }
            else if (other.gameObject.GetComponentInParent<InputManager_Player>()._playerState == InputManager_Player.PlayerAnimationState.upperCut)
            {
                Debug.Log("Player 2 Upper Cut");
                gameObject.GetComponent<Animator>().SetTrigger("KnockdownTrigger");
                _gamemanager.HealthBar_Players_Method(1, 15f);
            }
            else if (other.gameObject.GetComponentInParent<InputManager_Player>()._playerState == InputManager_Player.PlayerAnimationState.crouch)
            {
                Debug.Log("Player 2 Crouch");
                gameObject.GetComponent<Animator>().SetTrigger("KnockdownTrigger");
                _gamemanager.HealthBar_Players_Method(1, 15f);
            }
            else if (other.gameObject.GetComponentInParent<InputManager_Player>()._playerState == InputManager_Player.PlayerAnimationState.jab)
            {
                Debug.Log("Player 2 Jab");
                gameObject.GetComponent<Animator>().SetTrigger("LightHitTrigger");
                _gamemanager.HealthBar_Players_Method(1, 10f);
            }
            DisableColliders(false, 0);
            StartCoroutine("ResetRootMotion");
        }
        else if(_inputManager_Player.playerIndex == 2)
        {
            Debug.Log("Player 2 Trigger Method Called" + other.gameObject);
            gameObject.GetComponent<Animator>().applyRootMotion = true;
            if (other.gameObject.GetComponentInParent<InputManager_Player>()._playerState == InputManager_Player.PlayerAnimationState.punch)
            {
                Debug.Log("Player 2 Punch");
                gameObject.GetComponent<Animator>().SetTrigger("LightHitTrigger");
                _gamemanager.HealthBar_Players_Method(2, 10f);
            }
            else if (other.gameObject.GetComponentInParent<InputManager_Player>()._playerState == InputManager_Player.PlayerAnimationState.kick)
            {
                Debug.Log("Player 2 Kick");
                gameObject.GetComponent<Animator>().SetTrigger("KnockdownTrigger");
                _gamemanager.HealthBar_Players_Method(2, 15f);
            }
            else if (other.gameObject.GetComponentInParent<InputManager_Player>()._playerState == InputManager_Player.PlayerAnimationState.upperCut)
            {
                Debug.Log("Player 2 Upper Cut");
                gameObject.GetComponent<Animator>().SetTrigger("KnockdownTrigger");
                _gamemanager.HealthBar_Players_Method(2, 15f);
            }
            else if (other.gameObject.GetComponentInParent<InputManager_Player>()._playerState == InputManager_Player.PlayerAnimationState.crouch)
            {
                Debug.Log("Player 2 Crouch");
                gameObject.GetComponent<Animator>().SetTrigger("KnockdownTrigger");
                _gamemanager.HealthBar_Players_Method(2, 15f);
            }
            else if (other.gameObject.GetComponentInParent<InputManager_Player>()._playerState == InputManager_Player.PlayerAnimationState.jab)
            {
                Debug.Log("Player 2 Jab");
                gameObject.GetComponent<Animator>().SetTrigger("LightHitTrigger");
                _gamemanager.HealthBar_Players_Method(2, 10f);
            }
            DisableColliders(false, 0);
            StartCoroutine("ResetRootMotion");
        }

        //Debug.Log("Collision happens -->>" + other.gameObject.name);
        //if(other.GetComponentInParent<PlayerManager>() != this)
        //{
        //    other.GetComponent<Animator>().applyRootMotion = true;
        //    if(_inputManager_Player._playerState == InputManager_Player.PlayerAnimationState.upperCut)
        //    {
        //        other.GetComponent<Animator>().SetTrigger("KnockdownTrigger");
        //    }
        //    else if(_inputManager_Player._playerState == InputManager_Player.PlayerAnimationState.crouch)
        //    {
        //        other.GetComponent<Animator>().SetTrigger("KnockdownTrigger");
        //    }
        //    else if(_inputManager_Player._playerState != InputManager_Player.PlayerAnimationState.idle)
        //    {
        //        other.GetComponent<Animator>().SetTrigger("LightHitTrigger");
        //    }
        //    Debug.Log("Name of the GameObject is -->>" + other.gameObject.name);
        //    DisableColliders(false, 0);

        //    StartCoroutine("ResetRootMotion", other);
        //}
    }

    IEnumerator ResetRootMotion()
    {
        yield return new WaitForSeconds(0.75f);
        gameObject.GetComponent<Animator>().applyRootMotion = false;
    }
}
