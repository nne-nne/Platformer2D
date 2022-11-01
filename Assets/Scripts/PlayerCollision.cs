using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public const float playerAboveThreshold = 0.3f;
    [SerializeField] bool resetPositionAfterCollision = true;

    private float killOffset;
    private Vector2 startPosition;

    private void LoseHealth()
    {
        GameManager.instance.LoseLive();

        if (resetPositionAfterCollision)
        {
            StartCoroutine(ResetPositionAfterFrame());
        }
        else
        {
            // TODO: show being hit
        }
    }

    private IEnumerator ResetPositionAfterFrame()
    {
        yield return null;

        transform.position = startPosition;
    }

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Enemy")
            return;

        IMortalCreature mortal = collision.collider.GetComponent<IMortalCreature>();
        if(mortal == null)
        {
            Debug.Log("Immortal");
        }
        if (mortal != null && !mortal.IsAlive())
            return;  // dead enemy

        bool isPlayerAbove = (transform.position - collision.collider.transform.position).y > playerAboveThreshold;
        if(!isPlayerAbove)
        {
            LoseHealth();
        }
    }
}
