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
		[SerializeField] private UnityEvent playerJumpInterruptedEvent;
		[SerializeField] private Rigidbody2D playerRb;
		[SerializeField] private Vector2 velocity = new(5, 7.5f);
		[SerializeField] private float chargeMaxTime = 1;
		[SerializeField] private float minJumpVelocity = 1;
		private float startTime = float.NaN;

		public bool JumpCharging { get => !float.IsNaN(startTime); private set => startTime = float.NaN; }
		public Vector2 JumpVelocity => ClampMin(velocity * new Vector2(PlayerManager.Instance.FacingDirection, 1) * TimePassed, minJumpVelocity);
		private float TimePassed => ClampMax(Time.time - startTime, chargeMaxTime);
		public float JumpForcePercentage => TimePassed / chargeMaxTime;

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

				PlayerAnimator.SetIsJumpCharging(true);
			}
			else if (context.canceled) 
			{
				if (!JumpCharging) return;
				Vector2 jumpVelocity = JumpVelocity;
				if (jumpVelocity.magnitude < minJumpVelocity)
					jumpVelocity = JumpVelocity.normalized * minJumpVelocity;
				playerRb.velocity = jumpVelocity;
				playerJumpEvent.Invoke(JumpForcePercentage);
				JumpCharging = false;

				PlayerAnimator.SetIsJumpCharging(false);
			}
		}

		public void ForceInterruptCharge()
		{
			JumpCharging = false;
			playerJumpInterruptedEvent.Invoke();
		}
	}
}
