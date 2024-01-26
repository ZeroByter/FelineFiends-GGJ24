using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace InGame.Player
{
	public class PlayerMover : MonoBehaviour
	{
		[SerializeField] private UnityEvent<float> playerStartWalking;
		[SerializeField] private UnityEvent playerStopWalking;
		[SerializeField] private Rigidbody2D playerRb;
		[SerializeField] private float acceleration = 1;
		[SerializeField] private float maxSpeed = 2;
		private float inputDirection = 0;
		private float walkingDirection = 0;

		private float FacingDirection { set => PlayerManager.Instance.FacingDirection = value; }

		private void Reset()
		{
			playerRb = GetComponent<Rigidbody2D>();
		}

		private void OnDisable()
		{
			walkingDirection = 0;
		}

		private void OnEnable()
		{
			walkingDirection = inputDirection;
			if (walkingDirection != 0)
				FacingDirection = walkingDirection;
		}

		private void FixedUpdate()
		{
			Vector2 walkForce = new(acceleration * walkingDirection * Time.fixedDeltaTime, 0);
			playerRb.AddForce(walkForce, ForceMode2D.Impulse);
			playerRb.velocity = new Vector2(Mathf.Clamp(playerRb.velocity.x, -maxSpeed, maxSpeed), playerRb.velocity.y);
		}

		public void OnMove(InputAction.CallbackContext context)
		{
			inputDirection = context.ReadValue<float>();
			if (!enabled) return;
			walkingDirection = inputDirection;
			if (context.performed)
			{
				FacingDirection = walkingDirection;
			}
			else if (context.started)
			{
				playerStartWalking.Invoke(walkingDirection);
				FacingDirection = walkingDirection;
			}
			else if (context.canceled)
				playerStopWalking.Invoke();
		}
	}
}
