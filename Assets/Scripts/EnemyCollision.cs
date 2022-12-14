using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyCollision : MonoBehaviour, IMortalCreature
{
    [SerializeField] Animator animator;
    [SerializeField] float dieTime = 1f;

    bool isDead = false;

    public bool IsAlive()
    {
        return !isDead;
    }

    public void Die()
    {
        isDead = true;
        GetComponent<EnemyController>().enabled = false;
        GameManager.instance.KillEnemy();

        animator.SetBool("isDead", true);
        StartCoroutine(KillOnAnimationEnd());
    }

    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(dieTime);

        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
            return;

        if (collision.gameObject.tag != "Player")
            return;

        bool isPlayerAbove = (collision.collider.transform.position - transform.position).y > PlayerCollision.playerAboveThreshold;

        if (isPlayerAbove)
        {
            Die();
        }
    }
}
