using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerLevel1 : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    public float jumpForce = 6f;
    private Rigidbody2D rigidBody;
    private bool isFacingRight = true;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
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
        float hor = Input.GetAxisRaw("Horizontal");
        if (hor != 0)
        {
            if(hor>0&&!isFacingRight || hor < 0 && isFacingRight)
            {
                Flip();
            }
        }


        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        //}
        // powy¿ej wersja dok³adnie wg instrukcji
        transform.Translate(Vector3.right * Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime);
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
