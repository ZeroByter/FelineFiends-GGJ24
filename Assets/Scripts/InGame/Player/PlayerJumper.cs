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
		[SerializeField] private Vector2 velocity = new(5, 7.5f);
		[SerializeField] private float chargeMaxTime = 1;
		[SerializeField] private float minJumpVelocity = 1;
		private float startTime;

		public Vector2 JumpVelocity => ClampMin(velocity * new Vector2(PlayerManager.Instance.FacingDirection, 1) * TimePassed, minJumpVelocity);
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
				Vector2 jumpVelocity = JumpVelocity;
				if (jumpVelocity.magnitude < minJumpVelocity)
					jumpVelocity = JumpVelocity.normalized * minJumpVelocity;
				playerRb.velocity = jumpVelocity;
				playerJumpEvent.Invoke(TimePassed);
			}
		}
	}
}
