using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager Singleton;

    public static Vector3 GetPosition()
    {
        if (Singleton == null) return Vector3.zero;

        return Singleton.transform.position;
    }

    public static float GetPlayerFriction()
    {
        if (Singleton == null) return 0;

        return Singleton.playerFriction;
    }

    [SerializeField] private float playerFriction = 5;

    public UnityEvent onCeilingCollision;
    public UnityEvent onGroundCollision;

    private PlayerMover mover;
    private PlayerJumper jumper;
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
        jumper = GetComponent<PlayerJumper>();
        wallsStick = GetComponent<PlayerWallsStick>();
        ceilingCollision = GetComponent<PlayerCeilingCollision>();

        UpdateState();
    }

    private void UpdateState()
    {
        if(!lastIsGrounded && isGrounded)
        {
            onGroundCollision.Invoke();
            hasJumped = false;
        }

        mover.enabled = isGrounded;
        jumper.enabled = isGrounded;
        wallsStick.enabled = !isGrounded && isTouchingWall;

        if (!lastIsTouchingCeiling && isTouchingCeiling)
        {
            onCeilingCollision.Invoke();
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
