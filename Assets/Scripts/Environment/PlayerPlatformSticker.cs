using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sticks the player to moving platforms
/// </summary>
public class PlayerPlatformSticker : MonoBehaviour
{
    [SerializeField] string movingPlatformTag = "MovingPlatform";
    [SerializeField] float stickedMoveSpeedPart = 0.5f;

    private float originalMovespeed;
    private bool isLocked = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag(movingPlatformTag))
        {
            Lock(other);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag(movingPlatformTag))
        {
            Unlock();
        }
    }

    private void Lock(GameObject platform)
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
        transform.parent = platform.transform;

        if (isLocked)
            return;

        PlayerControllerLevel1 playerController = GetComponent<PlayerControllerLevel1>();
        originalMovespeed = playerController.MoveSpeed;
        playerController.MoveSpeed = originalMovespeed * stickedMoveSpeedPart;

        playerController.ImpulseMovingEnabled = false;

        isLocked = true;
    }

    private void Unlock()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        transform.parent = null;

        if (!isLocked)
            return;

        PlayerControllerLevel1 playerController = GetComponent<PlayerControllerLevel1>();
        playerController.MoveSpeed = originalMovespeed;

        playerController.ImpulseMovingEnabled = true;

        isLocked = false;
    }

    private void OnJumpInput()
    {
        if (transform.parent != null)
        {
            Unlock();
        }
    }

    private void OnEnable()
    {
        GetComponent<PlayerControllerLevel1>().onJumpInput += OnJumpInput;
    }

    private void OnDisable()
    {
        GetComponent<PlayerControllerLevel1>().onJumpInput -= OnJumpInput;
    }

    private void Start()
    {
        originalMovespeed = GetComponent<PlayerControllerLevel1>().MoveSpeed;
    }
}
