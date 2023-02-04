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
    [SerializeField] private BoolVariable dashUnlocked;
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

    private float _horizontalVelocity;
    private bool _mooving;

    #endregion

#region Dash

    private bool _dashing;
    private bool _canDash;

    private float _dashVelocity;
    
    [Range(1,10)]
    [SerializeField] private float dashPower;
    
    [SerializeField] private float dashTime;
    
    

#endregion

#region WallJump

[SerializeField] private Transform wallCheck;
    [SerializeField] private float wallSlidingSpeed;
    private bool _wallSliding;

    [SerializeField] private LayerMask wallLayer;
    
    private bool _wallJumping;
    private float _wallJumpingDirection;
    private float _wallJumpingVelocity;
    
    private float _wallJumpingCounter;
    
    [SerializeField] private float wallJumpingTime;
    
    [SerializeField] private float wallJumpingPower;

#endregion
    

    
    [SerializeField] private CharacterController2D controller;


    private void Awake()
    {
        _canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        RotateControl();
        GravityControl();
        WallSlideControl();
        WallJumpControl();
    }

    public void Move()
    {
        Vector2 verticalAxis = new Vector2(0, _verticalVelocity);
        Vector2 horizontalAxis = new Vector2(_horizontalVelocity, 0);
        
        if(_wallJumpingVelocity != 0)
            horizontalAxis.x = _wallJumpingVelocity;
        
        if (_dashVelocity != 0)
            horizontalAxis.x = _dashVelocity;
        
        controller.Move((horizontalAxis * (moveSpeed * Time.deltaTime)) + (verticalAxis * Time.deltaTime));
    }

    private void GravityControl()
    {
        if (_verticalVelocity <= 0 && _dashing == false)
        {
            _gravity = gravityDown;
        }
        
        if (controller.IsGrounded && _verticalVelocity < 0)
        {
            _secondJumping = false;
            _canDash = true;
            _verticalVelocity = 0f;
        }
        else
        {
            if (_verticalVelocity > maxFallSpeed)
                _verticalVelocity += _gravity * Time.deltaTime;
        }
    }

    private void RotateControl()
    {
        if (_mooving == true)
        {
            float  localScaleX = transform.localScale.x;
            float  localScaleY = transform.localScale.y;
            
            if ((_horizontalVelocity > 0 && localScaleX < 0) || (_horizontalVelocity < 0 && localScaleX > 0))
                transform.localScale = new Vector2(localScaleX * -1, localScaleY);
            
        }
    }

    private void WallSlideControl()
    {
        if (IsWalled() && controller.IsGrounded == false && _horizontalVelocity != 0 && _verticalVelocity < 0)
        {
            _wallSliding = true;
            _verticalVelocity = Mathf.Clamp(_verticalVelocity, -wallSlidingSpeed, float.MaxValue);
        }
        else
        {
            _wallSliding = false;
        }
    }

    private void WallJumpControl()
    {
        if (IsWalled() && controller.IsGrounded == false)
        {
            _wallJumping = false;
            _wallJumpingDirection = -transform.localScale.x;
            _wallJumpingCounter = wallJumpingTime;
        }
        else
        {
            _wallJumpingCounter -= Time.deltaTime;
        }
    }

    // check if you make the sex with a wall
    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.1f, wallLayer);
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

    private IEnumerator DoWallJump()
    {
        _wallJumping = true;
        _wallJumpingCounter = 0f;
        _gravity = gravityUp;
        _verticalVelocity = _maximumYVelocity;
        _wallJumpingVelocity = _wallJumpingDirection * wallJumpingPower;
        yield return new WaitForSeconds(0.2f);
        _wallJumping = false;
        _wallJumpingVelocity = 0f;
    }

    private IEnumerator DoDash()
    {
        _canDash = false;
        _dashing = true;
        _gravity = 0;
        _verticalVelocity = 0;
        
        _dashVelocity = transform.localScale.x * dashPower;
        yield return new WaitForSeconds(dashTime);
        
        _dashing = false;
        _dashVelocity = 0;
    }

#region InputsCall
    public void OnMove(InputValue value)
    {
        Vector2 move = value.Get<Vector2>();

        _mooving = move.x != 0;
        _horizontalVelocity = move.x;
    }


    public void OnJump()
    {
        DoJump();
        
        if (_wallJumpingCounter > 0f)
            StartCoroutine(DoWallJump());
    }

    public void OnJumpRelease()
    {
        if (_verticalVelocity > _minimumYVelocity)
            _verticalVelocity = _minimumYVelocity;
    }

    public void OnDash()
    {
        if(_canDash == true && dashUnlocked.value == true)
            StartCoroutine(DoDash());
    }
    
#endregion
    
    
}
