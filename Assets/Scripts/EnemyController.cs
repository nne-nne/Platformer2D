using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float xMinRelative = -1f;
    [SerializeField] float xMaxRelative = 1f;
    [SerializeField] float moveSpeed;
    [SerializeField] Animator animator;

    private bool isMovingRight = true;
    private bool isFacingRight = true;

    private float movingDirection = 1f;
    private float startX;

    private float GlobalXMin => startX + xMinRelative;
    private float GlobalXMax => startX + xMaxRelative;

    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        movingDirection = GetMovingDirection();
        if (movingDirection > 0 && !isFacingRight || movingDirection < 0 && isFacingRight)
        {
            Flip();
        }

        transform.Translate(Vector3.right * movingDirection * moveSpeed * Time.deltaTime);
    }

    private float GetMovingDirection()
    {
        float currX = transform.position.x;

        bool reachedXMax = movingDirection > 0 && currX >= GlobalXMax;
        if (reachedXMax)
            return -1f;

        bool reachedXMin = movingDirection < 0 && currX <= GlobalXMin;
        if (reachedXMin)
            return 1f;

        return movingDirection; // don't change direction
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            startX = transform.position.x;
        }

        // Draw movement bounds
        Gizmos.color = Color.red;

        Vector3 min = new Vector3(GlobalXMax, transform.position.y, transform.position.z);
        Vector3 max = new Vector3(GlobalXMin, transform.position.y, transform.position.z);
        Gizmos.DrawLine(min, max);
    }
}
