using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager_Player_2 : MonoBehaviour
{
    public Animator animator;
    private PlayerInput _player_2_Input;
    private Player_2_Controls _player_2_Controls;

    private bool InAir = false;

    private bool canMove = true;
    private float rotationMultiplier = 1;

    public Rigidbody rb;
    public float movementSpeed = 2f;
    Vector3 movement;

    private void Start()
    {
        //animator = transform.GetChild(0).GetComponent<Animator>();
        _player_2_Input = GetComponent<PlayerInput>();

        _player_2_Controls = new Player_2_Controls();
        _player_2_Controls.Player.Enable();
        _player_2_Controls.Player.Jump.performed += Player_2_Jump;

        _player_2_Controls.Player.ForwardJump.started += Player_2_ForwardJump;
        _player_2_Controls.Player.BackwardJump.started += Player_2_BackwardJump;
    }

    public void GetRotationMultiplier()
    {
        rotationMultiplier = this.transform.GetChild(0).rotation.eulerAngles.y > 0 ? -1 : 1;
    }

    private void Update()
    {
        if (canMove)
        {
            Vector2 _movement = _player_2_Controls.Player.Move.ReadValue<Vector2>();
            movement = _player_2_Controls.Player.Move.ReadValue<Vector2>();
            if (_movement != Vector2.zero)
            {
                if (_movement.x * rotationMultiplier > 0)
                {
                    animator.SetBool("Walk Forward", true);
                }
                else if (_movement.x * rotationMultiplier < 0)
                {
                    animator.SetBool("Walk Backward", true);
                }
            }
            else
            {
                animator.SetBool("Walk Forward", false);
                animator.SetBool("Walk Backward", false);
            }
        }
    }

    void FixedUpdate()
    {
        moveCharacter(movement);
    }
    void moveCharacter(Vector3 direction)
    {
        // Convert direction into Rigidbody space.
        direction = this.transform.forward * direction.x;

        rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime);
    }

    private void Player_2_Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump -->>" + context.phase);
        //canMove = false;
        if (context.phase == InputActionPhase.Performed)
        {
            animator.SetTrigger("JumpTrigger");
            StartCoroutine(GoInAir(0.25f));
        }
    }

    private void Player_2_ForwardJump(InputAction.CallbackContext context)
    {
        canMove = false;
        Debug.Log("Forward Jump -->>" + context.phase);
        if (context.phase == InputActionPhase.Started)
        {
            animator.SetBool("Walk Forward", false);
            animator.SetBool("Walk Backward", false);
            animator.ResetTrigger("JumpTrigger");
            animator.SetTrigger("JumpForwardTrigger");
            StartCoroutine(GoInAir(0.25f));
        }
    }

    private void Player_2_BackwardJump(InputAction.CallbackContext context)
    {
        canMove = false;
        Debug.Log("Backward Jump -->>" + context.phase);
        if (context.phase == InputActionPhase.Started)
        {
            animator.SetBool("Walk Forward", false);
            animator.SetBool("Walk Backward", false);
            animator.ResetTrigger("JumpTrigger");
            animator.SetTrigger("JumpBackwardTrigger");
            StartCoroutine(GoInAir(0.25f));
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
        canMove = true;
    }
}
