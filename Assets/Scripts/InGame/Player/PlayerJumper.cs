using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static Extensions.Extensions;

namespace InGame.Player
{
	public class PlayerJumper : MonoBehaviour
	{
		[SerializeField] private UnityEvent<float> playerJumpEvent;
		[SerializeField] private UnityEvent playerPrepareJumpEvent;
		[SerializeField] private Rigidbody2D playerRb;
		[SerializeField] private Vector2 force = new(100, 150);
		[SerializeField] private float chargeMaxTime = 1;
		private float startTime;

		public Vector2 JumpDirection => force * new Vector2(PlayerManager.Instance.FacingDirection, 1);
		public float JumpVelocity => JumpDirection.magnitude * TimePassed;
		private Vector2 JumpForce => JumpDirection * TimePassed;
		private float TimePassed => ClampMax(Time.time - startTime, chargeMaxTime);

		private void Reset()
		{
			playerRb = GetComponent<Rigidbody2D>();
		}

		public void OnJump(InputAction.CallbackContext context)
		{
			if (!enabled) return;
			if (context.started)
			{
				playerPrepareJumpEvent.Invoke();
				startTime = Time.time;
			}
			else if (context.canceled)
			{
				playerRb.velocity = JumpForce;
				playerJumpEvent.Invoke(TimePassed);
			}
		}
	}
}
