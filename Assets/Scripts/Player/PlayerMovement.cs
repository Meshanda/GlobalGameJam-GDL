using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

#region Unlockable
    [SerializeField] private BoolVariable secondJumpUnlocked;
#endregion

#region Jump
    [SerializeField] private float maximumJumpHeight;
    [SerializeField] private float minimumJumpHeight;

    private bool _secondJumping;
#endregion

#region Gravity
    [SerializeField] private float gravityUp;
    [SerializeField] private float gravityDown;
    private float _gravity;
    
    [SerializeField] private float maxFallSpeed;
    private float _maximumYVelocity => Mathf.Sqrt(_gravity * -2f * maximumJumpHeight);
    private float _minimumYVelocity => Mathf.Sqrt(_gravity * -2f * minimumJumpHeight);

    private float _verticalVelocity;
#endregion

#region SideMovement
    [Range(1, 50)]
    [SerializeField] private float moveSpeed;
    
    private Vector2 _horizontalAxis;
#endregion
    

    
    [SerializeField] private CharacterController2D controller;


    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        GravityControll();
    }

    public void Move()
    {
        Vector2 fall = new Vector2(0, _verticalVelocity);
        
        controller.Move((_horizontalAxis * (moveSpeed * Time.deltaTime)) + (fall * Time.deltaTime));
    }

    private void GravityControll()
    {
        if (_verticalVelocity <= 0)
        {
            _gravity = gravityDown;
        }
        
        if (controller.IsGrounded && _verticalVelocity < 0)
        {
            _secondJumping = false;
            _verticalVelocity = 0f;
        }
        else
        {
            if (_verticalVelocity > maxFallSpeed)
                _verticalVelocity += _gravity * Time.deltaTime;
        }
    }
    
    private void DoJump()
    {
        if(_secondJumping == true)
        {
            if(secondJumpUnlocked.value == true)
            {
                _secondJumping = false;
                _verticalVelocity = _maximumYVelocity;
            }
        }
        
        if (controller.IsGrounded)
        {
            _secondJumping = true;
            _gravity = gravityUp;
            _verticalVelocity = _maximumYVelocity;
        }
    }

#region InputsCall
    public void OnMove(InputValue value)
    {
        Vector2 move = value.Get<Vector2>();
        
        _horizontalAxis.x = move.x;
        _horizontalAxis.Normalize();
    }


    public void OnJump()
    {
        DoJump();
    }

    public void OnJumpRelease()
    {
        if (_verticalVelocity > _minimumYVelocity)
            _verticalVelocity = _minimumYVelocity;
    }
    
#endregion
    
    
}
