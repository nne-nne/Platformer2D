using System;
using System.Collections.Generic;
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
        transform.position = startPosition;

        if(currentHealth == 0)
        {
            Die();
        }
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
        IMortalCreature mortal = collision.otherCollider.GetComponent<IMortalCreature>();
        if (mortal != null && !mortal.IsAlive())
            return;  // dead enemy

        bool isPlayerAbove = (transform.position - collision.transform.position).y > playerAboveThreshold;
        if(collision.gameObject.tag == "Enemy" && !isPlayerAbove)
        {
            LoseHealth();
        }
    }
}
