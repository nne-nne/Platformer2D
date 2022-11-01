using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag(GameTags.PlayerTag))
        {
            LevelGenerator.Instance.AddPiece();
            LevelGenerator.Instance.RemoveOldestPiece();
        }
    }
}
