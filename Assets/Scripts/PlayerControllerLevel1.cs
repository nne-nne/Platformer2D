using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerLevel1 : MonoBehaviour
{
    [SerializeField] float groundedTolerance = 0.7f;
    [SerializeField] float groundedSideTolerance = 0.5f;
    [SerializeField] float jumpForce = 6f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float moveSpeed = 0.1f;

    private Rigidbody2D rigidBody;

    public void Jump()
    {
        if (!IsGrounded())
            return; // no double-jump

        Debug.Log("Jumping");
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        //Vector3 raycastTop = transform.position;
        //Vector3 raycastBottom = raycastTop + Vector3.down * groundedTolerance;
        //if(Physics.CapsuleCast(raycastTop, raycastBottom, groundedSideTolerance, Vector3.down, groundedTolerance, groundLayer.value))
        //{
        //    return true;
        //}
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundedTolerance, groundLayer.value);

        if (hit.collider != null)
        {
            return true;
        }
        Debug.Log("Air-borne");
        return false;
    }

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.Translate(Vector3.right * Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime);
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
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
