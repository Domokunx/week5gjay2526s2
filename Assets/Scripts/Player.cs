using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private BoxCollider2D _collider;
    private Rigidbody2D _rigidbody;
    
    [SerializeField] private InputActionAsset actionAsset;
    public LayerMask groundLayer;
    public Transform shootPoint;
    public Transform groundCheck;
    
    private InputActionMap movementMap;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;
    

    private Vector2 _movementVec;
    [HideInInspector] public bool isFacingRight = true;
    
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 50;
    [SerializeField] private float dashSpeed = 20;
    [SerializeField] private float groundCheckDist = 0.2f;

    private bool _canMove;
    private bool _isGrounded;
    private bool _hasDashed;
    private bool _hasJumped;
    private bool _hasAttacked;

    [Header("Weapons")]
    [SerializeField] private SwordWeapon? swordWeapon;
    [SerializeField] private GunWeapon? gunWeapon;
    private IWeapon? currentWeapon;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementMap = actionAsset.FindActionMap("Player");
        _collider = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (movementMap != null)
        {
            moveAction = movementMap.FindAction("Move");
            jumpAction = movementMap.FindAction("Jump");
            attackAction = movementMap.FindAction("Attack");
            attackAction.performed += Attack;
            movementMap.Enable();
        }

        WeaponType weaponToEquip = SelectedCharacterState.HasSelection
            ? SelectedCharacterState.StartingWeapon
            : WeaponType.Sword;

        EquipWeapon(weaponToEquip);
    }

    void Update()
    {
        _movementVec = moveAction?.ReadValue<Vector2>() ?? Vector2.zero;
        _hasJumped = jumpAction != null && jumpAction.IsPressed();
        _canMove = true;
        _isGrounded = IsGrounded();
        
        _rigidbody.gravityScale = _isGrounded ? 0 : 1;
        
        // Facing
        if (_movementVec.x > 0)
        {
            isFacingRight = true;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        } else if (_movementVec.x < 0)
        {
            isFacingRight = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
            
        
        
        if (_movementVec.x != 0)
        {
            // Add check for wall later (prevent stuck on wall)
            MoveX(_movementVec.x);
        }
        else
        {
            MoveX(0);
        }

        if (_hasJumped && _isGrounded)
        {
            // Add isgrounded check later (buggy??)
            Jump();
        }
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        currentWeapon?.Attack(this);
    }
    
    private void MoveX(float movementVecX)
    {
        if (!_canMove) return;
        
        _rigidbody.linearVelocity = new Vector2(movementVecX * movementSpeed, _rigidbody.linearVelocity.y);
    }

    private void Jump()
    {
        _isGrounded = false;
        
        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, 0);
        _rigidbody.linearVelocity += Vector2.up * jumpForce;
    }

    private void EquipWeapon(WeaponType weaponType)
    {
        if (swordWeapon != null)
            swordWeapon.gameObject.SetActive(weaponType == WeaponType.Sword);

        if (gunWeapon != null)
            gunWeapon.gameObject.SetActive(weaponType == WeaponType.Guns);

        if (weaponType == WeaponType.Guns && gunWeapon != null)
        {
            currentWeapon = gunWeapon;
            return;
        }

        if (weaponType == WeaponType.Sword && swordWeapon != null)
        {
            currentWeapon = swordWeapon;
            return;
        }

        currentWeapon = swordWeapon != null ? swordWeapon : gunWeapon;
    }

    bool IsGrounded()
    {
        _rigidbody.gravityScale = 0;
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDist, groundLayer);
    }

    private void OnDisable()
    {
        movementMap?.Disable();
    }
    
    
}
