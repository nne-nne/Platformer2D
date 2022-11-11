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
            if (!LevelGenerator.Instance.ShouldFinish)
            {
                LevelGenerator.Instance.AddPiece();
            }
            LevelGenerator.Instance.RemoveOldestPiece();
        }
    }
}
