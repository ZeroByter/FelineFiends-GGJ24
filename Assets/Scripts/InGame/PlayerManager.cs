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

    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingCeiling;

    private void Awake()
    {
        Singleton = this;

        mover = GetComponent<PlayerMover>();
        wallsStick = GetComponent<PlayerWallsStick>();

        UpdateState();
    }

    private void UpdateState()
    {
        mover.enabled = isGrounded;
        wallsStick.enabled = !isGrounded && isTouchingWall;
    }

    public void SetGrounded(bool grounded)
    {
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
