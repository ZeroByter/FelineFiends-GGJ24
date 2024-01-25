using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCeilingCollision : MonoBehaviour
{
    private Rigidbody2D playerRb;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    public void TriggerCeilingCollision()
    {
        //playerRb.velocity = new Vector2(playerRb.velocity.x / 2, playerRb.velocity.y);
    }
}
