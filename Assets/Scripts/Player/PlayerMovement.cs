using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : GenericSingleton<PlayerMovement>
{

#region Unlockable
    [SerializeField] private BoolVariable secondJumpUnlocked;
    [SerializeField] private BoolVariable dashUnlocked;
    [SerializeField] private BoolVariable wallJumpUnlocked;
    [SerializeField] private BoolVariable axeUnlocked;
#endregion

#region Jump
    [SerializeField] private float maximumJumpHeight;
    [SerializeField] private float minimumJumpHeight;
    [SerializeField] private float coyoteTime = 0.2f;
    
    private bool _isInCoyoteTime;
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

#region HeadBump

    [SerializeField] private Transform headCheck;

    #endregion

#region Axe

    [SerializeField] private GameObject axeToSpawn;
    [SerializeField] private Transform axeSocket;
    
    private GameObject axeSpawned;

    
#endregion

public bool Movable { get; set; } = true;
    [SerializeField] private Animator _playerAnimator;
    private bool _grapplingOccurs;
    
    public bool GrapplingOccurs
    {
        get
        {
            return _grapplingOccurs;
        }
        set
        {
            _grapplingOccurs = value;
            if (_grapplingOccurs)
            {
                AudioManager.Instance.Play("grappin");
                _verticalVelocity = 0f;
                rigidbody2D.gravityScale = 8.0f;
            }
            else
            {
                rigidbody2D.gravityScale = 0f;
                _verticalVelocity = rigidbody2D.velocity.y * 0.5f;
                //rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.y, 0);
            }
        }
    }

    [SerializeField] private CharacterController2D controller;
    [SerializeField] private Rigidbody2D rigidbody2D;


    private void Awake()
    {
        base.Awake();
        _canDash = true;
        EquipAxe();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Movable) return; 
        Move();
        RotateControl();
        GravityControl();
        WallSlideControl();
        WallJumpControl();
        HeadBumpControl();
        CoyoteTime();

        AnimationParameters();
    }

    private void AnimationParameters()
    {
        _playerAnimator.SetFloat("Speed", Mathf.Abs(_horizontalVelocity));
        _playerAnimator.SetBool("IsJumping", !controller.IsGrounded);
        _playerAnimator.SetBool("IsGrapling", GrapplingOccurs);
        _playerAnimator.SetBool("IsWalled", IsWalled());
    }

    private bool _lastGrounded = false;
    private void CoyoteTime()
    {
        if (_lastGrounded && !controller.IsGrounded)
        {
            _isInCoyoteTime = true;
            StopCoroutine(nameof(EndCoyoteCoroutine));
            StartCoroutine(nameof(EndCoyoteCoroutine));
        }

        _lastGrounded = controller.IsGrounded;
        if (_lastGrounded)
        {
            _isInCoyoteTime = false;
            StopCoroutine(nameof(EndCoyoteCoroutine));
        }
    }

    public IEnumerator EndCoyoteCoroutine()
    {
        yield return new WaitForSeconds(coyoteTime);
        _isInCoyoteTime = false;
    }

    public void Move()
    {
        Vector2 verticalAxis = new Vector2(0, _verticalVelocity);
        Vector2 horizontalAxis = new Vector2(_horizontalVelocity, 0);

        if(_wallJumpingVelocity != 0)
            horizontalAxis.x = _wallJumpingVelocity;
        
        if (_dashVelocity != 0)
            horizontalAxis.x = _dashVelocity;
        
        if (GrapplingOccurs == true)
            horizontalAxis = Vector2.zero;
        
        if(horizontalAxis != Vector2.zero)
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
        
        controller.Move((horizontalAxis * (moveSpeed * Time.deltaTime)) + (verticalAxis * Time.deltaTime));
    }

    private void GravityControl()
    {
        if (GrapplingOccurs == true)
            return;
        
        if (_verticalVelocity <= 0 && _dashing == false)
        {
            _gravity = gravityDown;
        }
        
        if (controller.IsGrounded && _verticalVelocity < 0)
        {
            rigidbody2D.velocity = Vector2.zero;
            _secondJumping = false;
            _canDash = true;
            _verticalVelocity = -0.1f;
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
        if (wallJumpUnlocked.value == false)
            return;
        
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
        if (wallJumpUnlocked.value == false)
            return;
        
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

    private void HeadBumpControl()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(headCheck.position, new Vector2(Mathf.Abs(transform.localScale.x), 0.1f), 0);
        
        foreach(var hit in hits)
        {
            if(hit == controller.Collider2D || hit.isTrigger)
                continue;
            
            ColliderDistance2D colliderDistance = hit.Distance(controller.Collider2D);
            
            if (Vector2.Angle(colliderDistance.normal, Vector2.up) > (180 - 0.05f) && _verticalVelocity >= 0)
            {
                _verticalVelocity = -1.0f;
            }
            return;
        }
    }
    

    private void EquipAxe()
    {
        if (axeSpawned)
            return;
        
        axeSpawned = Instantiate(axeToSpawn, axeSocket, false);
        axeSpawned.SetActive(false);
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
                if(GrapplingOccurs)
                    GrapplingOccurs = false;
                
                _secondJumping = false;
                _gravity = gravityUp;
                _verticalVelocity = _maximumYVelocity;
                _isInCoyoteTime = false;
                AudioManager.Instance.Play("jump");
            }
        }
        
        
        if (GrapplingOccurs == true)
            return;
        
        if (controller.IsGrounded || _isInCoyoteTime)
        {
            _isInCoyoteTime = false;
            _secondJumping = true;
            _gravity = gravityUp;
            _verticalVelocity = _maximumYVelocity;

            _playerAnimator.SetBool("IsJumping", true);
            AudioManager.Instance.Play("jump");
        }
    }

    private IEnumerator DoWallJump()
    {
        _wallJumping = true;
        _wallJumpingCounter = 0f;
        _gravity = gravityUp;
        _verticalVelocity = _maximumYVelocity;
        _wallJumpingVelocity = _wallJumpingDirection * wallJumpingPower;
        
        AudioManager.Instance.Play("jump");
        
        yield return new WaitForSeconds(0.2f);
        _wallJumping = false;
        _wallJumpingVelocity = 0f;
    }

    private IEnumerator DoDash()
    {
        _playerAnimator.SetBool("IsDashing", true);
        AudioManager.Instance.Play("dash");

        _canDash = false;
        _dashing = true;
        _gravity = 0;
        _verticalVelocity = 0;
        rigidbody2D.velocity = Vector2.zero;
        
        _dashVelocity = transform.localScale.x * dashPower;
        yield return new WaitForSeconds(dashTime);

        _playerAnimator.SetBool("IsDashing", false);
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
        
        
        if (GrapplingOccurs == true)
            return;
        
        if (_wallJumpingCounter > 0f && wallJumpUnlocked.value == true && controller.IsGrounded == false)
            StartCoroutine(DoWallJump());
    }

    public void OnJumpRelease()
    {
        if (_verticalVelocity > _minimumYVelocity)
            _verticalVelocity = _minimumYVelocity;
    }

    public void OnDash()
    {
        if (GrapplingOccurs == true)
            return;
        
        if(_canDash == true && dashUnlocked.value == true)
            StartCoroutine(DoDash());
    }

    public void OnAxe()
    {
        if(axeSpawned && axeUnlocked.value == true)
        {
            _playerAnimator.SetBool("Attack", true);
            AudioManager.Instance.Play("axe");
            StartCoroutine(StopAttackCoroutine());        
        }
    }
    
#endregion

    public void StartAttack()
    {
        axeSpawned.SetActive(true);
    }

    public void StopAttack()
    {
        axeSpawned.SetActive(false);
    }

    private IEnumerator StopAttackCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        _playerAnimator.SetBool("Attack", false);
    }
    
}
