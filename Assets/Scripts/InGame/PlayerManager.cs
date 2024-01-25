using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager Singleton;

    public static Vector3 GetPosition()
    {
        if (Singleton == null) return Vector3.zero;

        return Singleton.transform.position;
    }

    private PlayerMover mover;
    private PlayerWallsStick wallsStick;
    private PlayerCeilingCollision ceilingCollision;

    private bool hasJumped;

    private bool lastIsGrounded;
    private bool isGrounded;

    private bool isTouchingWall;

    private bool lastIsTouchingCeiling;
    private bool isTouchingCeiling;

    private void Awake()
    {
        Singleton = this;

        mover = GetComponent<PlayerMover>();
        wallsStick = GetComponent<PlayerWallsStick>();
        ceilingCollision = GetComponent<PlayerCeilingCollision>();

        UpdateState();
    }

    private void UpdateState()
    {
        if(!lastIsGrounded && isGrounded)
        {
            hasJumped = false;
        }

        mover.enabled = isGrounded;
        wallsStick.enabled = !isGrounded && isTouchingWall;

        if (!lastIsTouchingCeiling && isTouchingCeiling)
        {
            ceilingCollision.TriggerCeilingCollision();
        }
    }

    public void SetHasJumped(bool jumped)
    {
        hasJumped = jumped;
        UpdateState();
    }
    public bool GetHasJumped() => hasJumped;

    public void SetGrounded(bool grounded)
    {
        lastIsGrounded = isGrounded;
        isGrounded = grounded;
        UpdateState();
    }
    public void SetIsTouchingWall(bool touchingWall)
    {
        isTouchingWall = touchingWall;
        UpdateState();
    }
    public void SetIsTouchingCeiling(bool touchingCeiling)
    {
        lastIsTouchingCeiling = isTouchingCeiling;
        isTouchingCeiling = touchingCeiling;
        UpdateState();
    }
}
