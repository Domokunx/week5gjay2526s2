using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public LayerMask groundLayer;
    
    [Header("Enemy Stats")]
    [SerializeField] private float health = 100f;
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float jumpForce = 5f;
    
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;   
    private Player player;
    private Vector2 direction;

    private bool _isGrounded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = IsGrounded();

        _rigidbody.gravityScale = _isGrounded ? 0 : 1;
        direction = player.transform.position - transform.position;
        
        MoveX(direction.x > 0 ? 1f : -1f);
    }
    private void MoveX(float movementVecX)
    {
        if (Physics2D.Raycast(transform.position, new Vector2(movementVecX, 0), 0.7f , groundLayer)
            && _isGrounded)
        {
            Jump();
        }
        _rigidbody.linearVelocity = new Vector2(movementVecX * movementSpeed, _rigidbody.linearVelocity.y);
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.55f, groundLayer);
    }

    private void Jump()
    {
        _isGrounded = false;
        
        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, 0);
        _rigidbody.linearVelocity += Vector2.up * jumpForce;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        StartCoroutine(nameof(TakeDamageIndicator));
        if (health <= 0)
        {
            // Some powerup logic later
            Destroy(gameObject);
        }
    }

    private IEnumerator TakeDamageIndicator()
    {
        _spriteRenderer.color = Color.blue;
        yield return new WaitForSeconds(0.5f);
        _spriteRenderer.color = Color.red;
    }
}
