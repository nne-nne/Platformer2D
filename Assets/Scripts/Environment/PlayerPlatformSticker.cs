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

        PlayerControllerLevel1 playerController1 = GetComponent<PlayerControllerLevel1>();
        PlayerControllerLevel2 playerController2 = GetComponent<PlayerControllerLevel2>();
        if(playerController1!= null)
        {
            originalMovespeed = playerController1.MoveSpeed;
            playerController1.MoveSpeed = originalMovespeed * stickedMoveSpeedPart;

            playerController1.ImpulseMovingEnabled = false;
        }
        else if(playerController2 != null)
        {
            originalMovespeed = playerController2.MoveSpeed;
            playerController2.MoveSpeed = originalMovespeed * stickedMoveSpeedPart;

            playerController2.ImpulseMovingEnabled = false;
        }
        

        isLocked = true;
    }

    private void Unlock()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        transform.parent = null;

        if (!isLocked)
            return;


        PlayerControllerLevel1 playerController1 = GetComponent<PlayerControllerLevel1>();
        PlayerControllerLevel2 playerController2 = GetComponent<PlayerControllerLevel2>();

        if (playerController1 != null)
        {
            playerController1.MoveSpeed = originalMovespeed;
        } else if (playerController2 != null)
        {
            playerController2.ImpulseMovingEnabled = true;
        }
        

       

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
        PlayerControllerLevel1 playerController1 = GetComponent<PlayerControllerLevel1>();
        PlayerControllerLevel2 playerController2 = GetComponent<PlayerControllerLevel2>();

        if (playerController1 != null)
        {
            playerController1.onJumpInput += OnJumpInput;
        }
        else if (playerController2 != null)
        {
            playerController2.onJumpInput += OnJumpInput;
        }
    }

    private void OnDisable()
    {
        PlayerControllerLevel1 playerController1 = GetComponent<PlayerControllerLevel1>();
        PlayerControllerLevel2 playerController2 = GetComponent<PlayerControllerLevel2>();

        if (playerController1 != null)
        {
            playerController1.onJumpInput -= OnJumpInput;
        }
        else if (playerController2 != null)
        {
            playerController2.onJumpInput -= OnJumpInput;
        }
    }

    private void Start()
    {
        PlayerControllerLevel1 playerController1 = GetComponent<PlayerControllerLevel1>();
        PlayerControllerLevel2 playerController2 = GetComponent<PlayerControllerLevel2>();

        if (playerController1 != null)
        {
            originalMovespeed = playerController1.MoveSpeed;
        }
        else if (playerController2 != null)
        {
            originalMovespeed = playerController2.MoveSpeed;
        }
        
    }
}
