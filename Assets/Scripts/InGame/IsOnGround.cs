using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsOnGround: MonoBehaviour
{
    [SerializeField] private PlayerMover playerMover;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerMover.isOnGround = (LayerMask.GetMask("Terrain") & (1 << collision.gameObject.layer)) != 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerMover.isOnGround = false;
    }
}
