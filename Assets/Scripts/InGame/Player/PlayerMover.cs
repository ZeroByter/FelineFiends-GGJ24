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
		}

		private void FixedUpdate()
		{
			Vector2 walkForce = new(acceleration * walkingDirection * Time.fixedDeltaTime, 0);
			playerRb.AddForce(walkForce, ForceMode2D.Impulse);
			playerRb.velocity = Vector2.ClampMagnitude(playerRb.velocity, maxSpeed);
		}

		public void OnMove(InputAction.CallbackContext context)
		{
			inputDirection = context.ReadValue<float>();
			if (!enabled) return;
			walkingDirection = inputDirection;
			if (context.performed)
			{
				PlayerManager.Instance.FacingDirection = walkingDirection;
			}
			else if (context.started)
			{
				playerStartWalking.Invoke(walkingDirection);
				PlayerManager.Instance.FacingDirection = walkingDirection;
			}
			else if (context.canceled)
				playerStopWalking.Invoke();
		}
	}
}
