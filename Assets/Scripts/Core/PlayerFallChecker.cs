using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallChecker : MonoBehaviour
{
    [SerializeField] string fallLevelTag = "FallLevel";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag(fallLevelTag))
        {
            GameManager.instance.GameOver();
        }
    }
}
