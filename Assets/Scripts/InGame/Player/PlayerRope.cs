using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Player
{
    [RequireComponent(typeof(FixedJoint2D))]
    public class PlayerRope : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D playerRb;
        [SerializeField] private FixedJoint2D fixedJoint;

        private void Reset()
        {
            playerRb = GetComponent<Rigidbody2D>();

            fixedJoint = GetComponent<FixedJoint2D>();
            fixedJoint.enabled = false;
        }

        public void OnEnterRope(Collider2D ropeCollider)
        {
            var preVelocity = playerRb.velocity;

            fixedJoint.enabled = true;
            fixedJoint.connectedBody = ropeCollider.attachedRigidbody;

            playerRb.velocity = preVelocity * 2;
        }

        private void FixedUpdate()
        {
            playerRb.AddForce(new Vector2(-10, 0), ForceMode2D.Impulse);
        }
    }
}
