using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask groundLayer;
    private Vector2 colliderOffset;

    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        colliderOffset = GetComponent<BoxCollider2D>().offset;
    }

    private void Move(float velocity)
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        bool isFacingRight = horizontalInput >= 0;
        Vector2 targetPosition = (Vector2)transform.position + new Vector2(horizontalInput, 0f) * moveSpeed * Time.deltaTime;
        Vector2 rayOrigin = targetPosition + new Vector2(0f, 0.5f); 
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 1f, groundLayer);
        Vector2 currentPosition = (Vector2)transform.position + colliderOffset; 
        Vector2 actualTargetPosition = hit.collider != null ? hit.point : targetPosition;
        Vector2 moveDirection = (actualTargetPosition - currentPosition).normalized;
        if (!isFacingRight) moveDirection.x *= -1; 
        rb2D.velocity = moveDirection * moveSpeed;
    }

    private void FixedUpdate()
    {
        Move(moveSpeed);
    }
}

