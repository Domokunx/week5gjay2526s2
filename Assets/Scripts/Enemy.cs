using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float health = 100f;

    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float jumpForce = 5f;
    
    private Rigidbody2D _rigidbody;   
    private Player player;
    private Vector2 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.transform.position - transform.position;
        
        MoveX(direction.x > 0 ? 1f : -1f);
    }
    private void MoveX(float movementVecX)
    {
        _rigidbody.linearVelocity = new Vector2(movementVecX * movementSpeed, _rigidbody.linearVelocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Jump();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Jump();
    }

    private void Jump()
    {
        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, 0);
        _rigidbody.linearVelocity += Vector2.up * jumpForce;
    }
}
