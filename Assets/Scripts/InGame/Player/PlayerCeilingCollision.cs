using InGame.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Player
{
    public class PlayerCeilingCollision : MonoBehaviour
    {
        private Rigidbody2D playerRb;

        private PhysicsMaterial2D physMaterial;

        private void UpdatePhysicalMaterial(float friction)
        {
            playerRb.sharedMaterial = null;

            physMaterial = new PhysicsMaterial2D();
            physMaterial.friction = friction;

            playerRb.sharedMaterial = physMaterial;
        }

        private void Awake()
        {
            playerRb = GetComponent<Rigidbody2D>();

            UpdatePhysicalMaterial(PlayerManager.GetPlayerFriction());
        }

        public void TriggerCeilingCollision()
        {
            UpdatePhysicalMaterial(0);

            playerRb.velocity = new Vector2(playerRb.velocity.x / 2, playerRb.velocity.y);
        }

        public void TriggerGroundCollision()
        {
            UpdatePhysicalMaterial(5);
        }
    }
}
