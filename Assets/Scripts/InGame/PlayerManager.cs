using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerMover mover;
    private PlayerWallsStick wallsStick;

    private bool hasJumped;

    private bool lastIsGrounded;
    private bool isGrounded;

    private bool isTouchingWall;
    private bool isTouchingCeiling;

    private void Awake()
    {
        mover = GetComponent<PlayerMover>();
        wallsStick = GetComponent<PlayerWallsStick>();

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
        isTouchingCeiling = touchingCeiling;
        UpdateState();
    }
}
