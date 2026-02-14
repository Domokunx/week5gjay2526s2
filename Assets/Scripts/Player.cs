using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private BoxCollider2D _collider;
    private Rigidbody2D _rigidbody;
    
    
    [SerializeField] private InputActionAsset actionAsset;
    private InputActionMap movementMap;

    private Vector2 _movementVec;
    
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 50;
    [SerializeField] private float dashSpeed = 20;
    [SerializeField] private float groundCheckDist = 0.2f;

    private bool _canMove;
    private bool _isGrounded;
    private bool _hasDashed;
    private bool _hasJumped;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementMap = actionAsset.FindActionMap("Player");
        _collider = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _movementVec = movementMap.FindAction("Move").ReadValue<Vector2>();
        _hasJumped = movementMap.FindAction("Jump").IsPressed();

        _canMove = true;
        _isGrounded = IsGrounded();
        
        if (_movementVec.x != 0)
        {
            // Add check for wall later (prevent stuck on wall)
            MoveX(_movementVec.x);
        }
        else
        {
            MoveX(0);
        }

        if (_hasJumped)
        {
            // Add isgrounded check later (buggy??)
            Jump();
        }
    }

    private void MoveX(float movementVecX)
    {
        if (!_canMove) return;
        
        _rigidbody.linearVelocity = new Vector2(movementVecX * movementSpeed, _rigidbody.linearVelocity.y);
    }

    private void Jump()
    {
        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, 0);
        _rigidbody.linearVelocity += Vector2.up * jumpForce;
    }

    bool IsGrounded()
    {
        return _rigidbody.linearVelocityY == 0 && 
               Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundCheckDist);
    }
}
