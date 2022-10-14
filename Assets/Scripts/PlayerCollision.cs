using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public const float playerAboveThreshold = 0.3f;
    [SerializeField] int startingHealth = 3;

    private int currentHealth;

    private float killOffset;
    private Vector2 startPosition;

    private void LoseHealth()
    {
        if (currentHealth <= 0)
            return;

        currentHealth -= 1;
        StartCoroutine(ResetPositionAfterFrame());

        if(currentHealth == 0)
        {
            Die();
        }
    }

    private IEnumerator ResetPositionAfterFrame()
    {
        yield return null;

        transform.position = startPosition;
    }

    private void Die()
    {
        Debug.Log("Player dead");
    }

    private void Awake()
    {
        startPosition = transform.position;
        currentHealth = startingHealth;
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
