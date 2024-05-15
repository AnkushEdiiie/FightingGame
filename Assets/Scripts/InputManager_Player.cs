using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class InputManager_Player : MonoBehaviour
{
    public GameManager_Old _gameManager;
    public PlayerManager _playerManager_Player;

    public Animator animator;
    private Player_1_Controls _player_1_Controls;
    private Player_2_Controls _player_2_Controls;

    private bool InAir = false;

    private bool canMove = true;
    private bool canMoveActually = true;

    public Rigidbody _rigidBody_Player;
    public float movementSpeed = 2f;
    public int playerIndex;
    Vector2 movement;

    public enum PlayerAnimationState { idle, jump, forwardJump, backwardJump, punch, forwardPunch, backwardPunch, kick, forwardKick, backwardkick, jab, forwardJab, backwardJab, upperCut, forwardUpperCut, backwardUpperCut, crouch };
    public PlayerAnimationState _playerState;

    private void Start()
    {
        //animator = transform.GetChild(0).GetComponent<Animator>();
        _playerState = PlayerAnimationState.idle;
        if(playerIndex == 1)
        {
            _player_1_Controls = new Player_1_Controls();
            _player_1_Controls.Player.Enable();

            _player_1_Controls.Player.Jump.started += Player_1_Jump;
            _player_1_Controls.Player.ForwardJump.started += Player_1_ForwardJump;
            _player_1_Controls.Player.BackwardJump.started += Player_1_BackwardJump;

            _player_1_Controls.Player.Punch.started += Player_1_Punch;
            _player_1_Controls.Player.ForwardPunch.started += Player_1_ForwardPunch;
            _player_1_Controls.Player.BackwardPunch.started += Player_1_BackwardPunch;

            _player_1_Controls.Player.Kick.started += Player_1_Kick;
            _player_1_Controls.Player.ForwardKick.started += Player_1_ForwardKick;
            _player_1_Controls.Player.BackwardKick.started += Player_1_BackwardKick;

            _player_1_Controls.Player.Jab.started += Player_1_Jab;
            _player_1_Controls.Player.ForwardJab.started += Player_1_ForwardJab;
            _player_1_Controls.Player.BackwardJab.started += Player_1_BackwardJab;

            _player_1_Controls.Player.UpperCut.started += Player_1_UpperCut;
            _player_1_Controls.Player.ForwardUpperCut.started += Player_1_ForwardUpperCut;
            _player_1_Controls.Player.BackwardUpperCut.started += Player_1_BackwardUpperCut;

            _player_1_Controls.Player.CrouchSweep.started += Player_1_CrouchSweep;
        }
        else
        {
            _player_2_Controls = new Player_2_Controls();
            _player_2_Controls.Player.Enable();

            _player_2_Controls.Player.Jump.started += Player_1_Jump;
            _player_2_Controls.Player.ForwardJump.started += Player_1_ForwardJump;
            _player_2_Controls.Player.BackwardJump.started += Player_1_BackwardJump;

            _player_2_Controls.Player.Punch.started += Player_1_Punch;
            _player_2_Controls.Player.ForwardPunch.started += Player_1_ForwardPunch;
            _player_2_Controls.Player.BackwardPunch.started += Player_1_BackwardPunch;

            _player_2_Controls.Player.Kick.started += Player_1_Kick;
            _player_2_Controls.Player.ForwardKick.started += Player_1_ForwardKick;
            _player_2_Controls.Player.BackwardKick.started += Player_1_BackwardKick;

            _player_2_Controls.Player.Jab.started += Player_1_Jab;
            _player_2_Controls.Player.ForwardJab.started += Player_1_ForwardJab;
            _player_2_Controls.Player.BackwardJab.started += Player_1_BackwardJab;

            _player_2_Controls.Player.UpperCut.started += Player_1_UpperCut;
            _player_2_Controls.Player.ForwardUpperCut.started += Player_1_ForwardUpperCut;
            _player_2_Controls.Player.BackwardUpperCut.started += Player_1_BackwardUpperCut;

            _player_2_Controls.Player.CrouchSweep.started += Player_1_CrouchSweep;
        }
        
    }

    private float rotationMultiplier = 1;

    public void GetRotationMultiplier()
    {
        rotationMultiplier = this.transform.GetChild(0).rotation.eulerAngles.y > 0 ? -1 : 1;
    }

    private void Update()
    {
        //Debug.Log("State of the Character -->>" + _playerState.ToString());
        if(canMove)
        {
            Vector2 _movement = playerIndex == 1 ? _player_1_Controls.Player.Move.ReadValue<Vector2>() : _player_2_Controls.Player.Move.ReadValue<Vector2>();
            movement = playerIndex == 1 ? _player_1_Controls.Player.Move.ReadValue<Vector2>() : _player_2_Controls.Player.Move.ReadValue<Vector2>();
            if (movement != Vector2.zero)
            {
                if( movement.y < 0)
                {
                    animator.SetBool("Walk Forward", false);
                    animator.SetBool("Walk Backward", false);
                    animator.SetBool("Crouch", true);
                    if(_playerState != PlayerAnimationState.crouch)
                    {
                        _playerState = PlayerAnimationState.crouch;
                    }
                }
                else if(movement.y == 0.0f)
                {
                    animator.SetBool("Crouch", false);
                    if (_playerState != PlayerAnimationState.idle)
                    {
                        _playerState = PlayerAnimationState.idle;
                    }
                    if (movement.x * rotationMultiplier > 0)
                    {
                        animator.SetBool("Walk Forward", true);
                    }
                    else if (movement.x * rotationMultiplier < 0)
                    {
                        animator.SetBool("Walk Backward", true);
                    }
                }
                
            }
            else
            {
                animator.SetBool("Walk Forward", false);
                animator.SetBool("Walk Backward", false);
                animator.SetBool("Crouch", false);
                if (_playerState != PlayerAnimationState.idle)
                {
                    _playerState = PlayerAnimationState.idle;
                }
            }
        }
        else
        {
            movement = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        if(canMoveActually)
        {
            moveCharacter(movement);
        }
    }
    void moveCharacter(Vector3 direction)
    {
        // Convert direction into Rigidbody space.
        //Debug.Log("Movement Speed is-->>" + movementSpeed);
        if(movement.y == 0) 
        {
            direction = this.transform.forward * direction.x;
            _rigidBody_Player.MovePosition(_rigidBody_Player.position + direction * movementSpeed * Time.fixedDeltaTime);
        }
        
    }


    private void Player_1_Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump -->>" + context.phase);
        if(_playerState == PlayerAnimationState.idle)
        {
            canMove = false;
            canMoveActually = false;
            if (context.phase == InputActionPhase.Started)
            {
                animator.SetBool("Walk Forward", false);
                animator.SetBool("Walk Backward", false);
                animator.SetTrigger("JumpTrigger");
                StartCoroutine(GoInAir(0.25f));
            }
            _playerState = PlayerAnimationState.jump;
        }
    }

    private void Player_1_ForwardJump(InputAction.CallbackContext context)
    {
        if (_playerState == PlayerAnimationState.idle)
        {
            canMove = false;
            canMoveActually = false;
            Debug.Log("Forward Jump -->>" + context.phase);
            if (context.phase == InputActionPhase.Started)
            {
                animator.SetBool("Walk Forward", false);
                animator.SetBool("Walk Backward", false);
                animator.ResetTrigger("JumpTrigger");
                animator.SetTrigger("JumpForwardTrigger");
                animator.applyRootMotion = true;
                StartCoroutine(GoInAir(0.25f));
            }
            _playerState = PlayerAnimationState.forwardJump;
        }
    }

    private void Player_1_BackwardJump(InputAction.CallbackContext context)
    {
        if(_playerState == PlayerAnimationState.idle)
        {
            canMove = false;
            canMoveActually = false;
            Debug.Log("Backward Jump -->>" + context.phase);
            if (context.phase == InputActionPhase.Started)
            {
                animator.SetBool("Walk Forward", false);
                animator.SetBool("Walk Backward", false);
                animator.ResetTrigger("JumpTrigger");
                animator.SetTrigger("JumpBackwardTrigger");
                StartCoroutine(GoInAir(0.25f));
            }
            _playerState = PlayerAnimationState.backwardJump;
        }
    }

    private void Player_1_Punch(InputAction.CallbackContext context)
    {
        Debug.Log("Punch -->>" + context.phase);
        if(_playerState == PlayerAnimationState.idle)
        {
            canMove = false;
            canMoveActually = false;
            if (context.phase == InputActionPhase.Started)
            {
                animator.SetBool("Walk Forward", false);
                animator.SetBool("Walk Backward", false);
                animator.SetTrigger("PunchTrigger");
                _playerManager_Player.EnableColliderForPunch(true, 0);
                StartCoroutine(ResetMovement(1f));
            }
            _playerState = PlayerAnimationState.punch;
        }
    }

    private void Player_1_ForwardPunch(InputAction.CallbackContext context)
    {
        if (_playerState == PlayerAnimationState.idle)
        {
            canMove = false;
            canMoveActually = false;
            _playerState = PlayerAnimationState.punch;
            //Debug.Log("Forward Jump -->>" + context.phase);
            if (context.phase == InputActionPhase.Started)
            {
                animator.SetBool("Walk Forward", false);
                animator.SetBool("Walk Backward", false);
                animator.ResetTrigger("PunchTrigger");
                animator.SetTrigger("PunchTrigger");
                _playerManager_Player.EnableColliderForPunch(true, 0);
                StartCoroutine(ResetMovement(1f));
            }
            _playerState = PlayerAnimationState.forwardPunch;
        }
    }

    private void Player_1_BackwardPunch(InputAction.CallbackContext context)
    {
        if (_playerState == PlayerAnimationState.idle)
        {
            canMove = false;
            canMoveActually = false;
            //Debug.Log("Forward Jump -->>" + context.phase);
            if (context.phase == InputActionPhase.Started)
            {
                animator.SetBool("Walk Forward", false);
                animator.SetBool("Walk Backward", false);
                animator.ResetTrigger("PunchTrigger");
                _playerManager_Player.EnableColliderForPunch(true, 0);
                animator.SetTrigger("PunchTrigger");
                StartCoroutine(ResetMovement(1f));
            }
            _playerState = PlayerAnimationState.backwardPunch;
        }
    }
    

    private void Player_1_Kick(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump -->>" + context.phase);
        if (_playerState == PlayerAnimationState.idle)
        {
            canMove = false;
            canMoveActually = false;
            if (context.phase == InputActionPhase.Started)
            {
                animator.SetBool("Walk Forward", false);
                animator.SetBool("Walk Backward", false);
                _playerManager_Player.EnableColliderForKick(true, 0);
                animator.SetTrigger("AxeKickTrigger");
                StartCoroutine(ResetMovement(1f));
            }
            _playerState = PlayerAnimationState.kick;
        }
    }
    private void Player_1_ForwardKick(InputAction.CallbackContext context)
    {
        if (_playerState == PlayerAnimationState.idle)
        {
            canMove = false;
            canMoveActually = false;
            _playerState = PlayerAnimationState.kick;
            //Debug.Log("Forward Jump -->>" + context.phase);
            if (context.phase == InputActionPhase.Started)
            {
                animator.SetBool("Walk Forward", false);
                animator.SetBool("Walk Backward", false);
                animator.ResetTrigger("AxeKickTrigger");
                _playerManager_Player.EnableColliderForKick(true, 0);
                animator.SetTrigger("AxeKickTrigger");
                StartCoroutine(ResetMovement(1f));
            }
            _playerState = PlayerAnimationState.forwardKick;
        }
    }

    private void Player_1_BackwardKick(InputAction.CallbackContext context)
    {
        if (_playerState == PlayerAnimationState.idle)
        {
            canMove = false;
            canMoveActually = false;
            //Debug.Log("Forward Jump -->>" + context.phase);
            if (context.phase == InputActionPhase.Started)
            {
                animator.SetBool("Walk Forward", false);
                animator.SetBool("Walk Backward", false);
                animator.ResetTrigger("AxeKickTrigger");
                _playerManager_Player.EnableColliderForKick(true, 0);
                animator.SetTrigger("AxeKickTrigger");
                StartCoroutine(ResetMovement(1f));
            }
            _playerState = PlayerAnimationState.backwardkick;
        }
    }

    private void Player_1_Jab(InputAction.CallbackContext context)
    {
        Debug.Log("Jab -->>" + context.phase);
        if(_playerState == PlayerAnimationState.idle)
        {
            canMove = false;
            canMoveActually = false;
            if (context.phase == InputActionPhase.Started)
            {
                animator.SetBool("Walk Forward", false);
                animator.SetBool("Walk Backward", false);
                animator.SetTrigger("KickTrigger");
                _playerManager_Player.EnableColliderForJab(true, 0);
                StartCoroutine(ResetMovement(1f));
            }
            _playerState = PlayerAnimationState.jab;
        }
    }
    private void Player_1_ForwardJab(InputAction.CallbackContext context)
    {
        if(_playerState == PlayerAnimationState.idle)
        {
            canMove = false;
            canMoveActually = false;
            Debug.Log("Forward Jump -->>" + context.phase);
            if (context.phase == InputActionPhase.Started)
            {
                animator.SetBool("Walk Forward", false);
                animator.SetBool("Walk Backward", false);
                animator.ResetTrigger("KickTrigger");
                _playerManager_Player.EnableColliderForJab(true, 0);
                animator.SetTrigger("KickTrigger");
                StartCoroutine(ResetMovement(1f));
            }
            _playerState = PlayerAnimationState.forwardJab;
        }
    }

    private void Player_1_BackwardJab(InputAction.CallbackContext context)
    {
        if(_playerState == PlayerAnimationState.idle)
        {
            canMove = false;
            canMoveActually = false;
            Debug.Log("Backward Jab -->>" + context.phase);
            if (context.phase == InputActionPhase.Started)
            {
                animator.SetBool("Walk Forward", false);
                animator.SetBool("Walk Backward", false);
                animator.ResetTrigger("KickTrigger");
                _playerManager_Player.EnableColliderForJab(true, 0);
                animator.SetTrigger("KickTrigger");
                StartCoroutine(ResetMovement(1f));
            }
            _playerState = PlayerAnimationState.backwardJab;
        }
    }

    private void Player_1_UpperCut(InputAction.CallbackContext context)
    {
        if(_playerState == PlayerAnimationState.idle)
        {
            //Debug.Log("Jump -->>" + context.phase);
            canMove = false;
            canMoveActually = false;
            if (context.phase == InputActionPhase.Started)
            {
                animator.SetBool("Walk Forward", false);
                animator.SetBool("Walk Backward", false);
                _playerManager_Player.EnableColliderForUpperCut(true, 0);
                animator.SetTrigger("UppercutTrigger");
                StartCoroutine(ResetMovement(1f));
            }
            _playerState = PlayerAnimationState.upperCut;
        }
    }

    private void Player_1_ForwardUpperCut(InputAction.CallbackContext context)
    {
        if(_playerState == PlayerAnimationState.idle)
        {
            canMove = false;
            canMoveActually = false;
            Debug.Log("Forward Jump -->>" + context.phase);
            if (context.phase == InputActionPhase.Started)
            {
                animator.SetBool("Walk Forward", false);
                animator.SetBool("Walk Backward", false);
                animator.ResetTrigger("UppercutTrigger");
                _playerManager_Player.EnableColliderForUpperCut(true, 0);
                animator.SetTrigger("UppercutTrigger");
                StartCoroutine(ResetMovement(1f));
            }
            _playerState = PlayerAnimationState.forwardUpperCut;
        }
    }

    private void Player_1_BackwardUpperCut(InputAction.CallbackContext context)
    {
        if(_playerState == PlayerAnimationState.idle)
        {
            canMove = false;
            canMoveActually = false;
            Debug.Log("Forward Jump -->>" + context.phase);
            if (context.phase == InputActionPhase.Started)
            {
                animator.SetBool("Walk Forward", false);
                animator.SetBool("Walk Backward", false);
                animator.ResetTrigger("UppercutTrigger");
                _playerManager_Player.EnableColliderForUpperCut(true, 0);
                animator.SetTrigger("UppercutTrigger");
                StartCoroutine(ResetMovement(1f));
            }
            _playerState = PlayerAnimationState.backwardUpperCut;
        }
    }

    private void Player_1_CrouchSweep(InputAction.CallbackContext context)
    {
         if(_playerState == PlayerAnimationState.crouch)
         {
             canMove = false;
             canMoveActually = false;
             Debug.Log("Forward Jump -->>" + context.phase);
             if (context.phase == InputActionPhase.Started)
             {
                 //animator.SetBool("Walk Forward", false);
                 //animator.SetBool("Walk Backward", false);
                 animator.SetBool("SweepTrigger", true);
                 _playerManager_Player.EnableColliderForCrouchSweep(true, 0);
                 StartCoroutine(ResetMovement(1f));
             }
         }
    }

    IEnumerator GoInAir(float toAnimWindow)
    {
        yield return new WaitForSeconds(toAnimWindow);
        InAir = true;
        animator.SetBool("InAir", true);
        yield return new WaitForSeconds(0.5f);
        InAir = false;
        animator.SetBool("InAir", false);
        yield return new WaitForSeconds(0.25f);
        canMove = true;
        yield return new WaitForSeconds(0.5f);
        canMoveActually = true;
        _playerState = PlayerAnimationState.idle;
        _playerManager_Player.DisableAllColliders(false, 0);
        animator.applyRootMotion = false;

    }

    IEnumerator ResetMovement(float _timeToResetMovement)
    {
        yield return new WaitForSeconds(_timeToResetMovement);
        canMove = true;
        yield return new WaitForSeconds(0.5f);
        canMoveActually = true;
        _playerState = PlayerAnimationState.idle;
        _playerManager_Player.DisableAllColliders(false, 0);
        animator.applyRootMotion = false;
    }
}
