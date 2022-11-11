using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerLevel2 : MonoBehaviour
{
    [SerializeField] float groundedTolerance = 0.7f;
    [SerializeField] float groundedSideTolerance = 0.5f;
    [SerializeField] float jumpForce = 6f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float moveSpeed = 0.1f;
    [SerializeField] float dump = 0.6f;

    public event Action onJumpInput;

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    public bool ImpulseMovingEnabled { get; set; } = true;

    private Rigidbody2D rigidBody;
    private bool isFacingRight = true;
    private Animator animator;
    public void Jump()
    {
        if (!IsGrounded())
            return; // no double-jump

        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void MoveRight()
    {
        if (rigidBody.velocity.x < moveSpeed)
        {
            rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);
            //rigidBody.AddForce(Vector2.right * dump, ForceMode2D.Force);
        }
    }

    private void MoveLeft()
    {
        if (rigidBody.velocity.x > -moveSpeed)
        {
            rigidBody.velocity = new Vector2(-moveSpeed, rigidBody.velocity.y);
            //rigidBody.AddForce(Vector2.left * dump, ForceMode2D.Force);
        }
    }

    private bool IsGrounded()
    {
        Vector2 raycastTop = transform.position;
        Vector2 raycastBottom = raycastTop + Vector2.down * groundedTolerance;

        Vector2 raycastCenter = (raycastTop + raycastBottom) / 2;
        Vector2 raycastSize = new Vector2(groundedSideTolerance, (raycastTop - raycastCenter).y);

        RaycastHit2D hit = Physics2D.BoxCast(raycastCenter, raycastSize, 0, Vector2.down, groundedTolerance, groundLayer.value);

        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Update()
    {
        if(GameManager.instance.currentGameState == GameState.GS_GAME)
        {
            if (rigidBody.velocity.x < moveSpeed)
            {
                rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);
            }

            float hor = Input.GetAxis("Horizontal");

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                onJumpInput?.Invoke();
                Jump();
            }
            animator.SetBool("isGrounded", IsGrounded());
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw grounded tolerance
        Gizmos.color = Color.blue;

        Vector3 groundedExtent = transform.position + Vector3.down * groundedTolerance;

        Gizmos.DrawLine(transform.position, groundedExtent);

        Gizmos.DrawLine(groundedExtent, groundedExtent + Vector3.left * groundedSideTolerance);
        Gizmos.DrawLine(groundedExtent, groundedExtent - Vector3.left * groundedSideTolerance);
    }
}
