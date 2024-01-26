using UnityEngine;
using UnityEngine.Events;

namespace InGame.Player
{
	public class PlayerWallsStick : MonoBehaviour
	{
		[SerializeField] private UnityEvent startSlideEvent;
		[SerializeField] private Rigidbody2D playerRb;
		[SerializeField] private float stickDuration = 1;
		[SerializeField] private float slideDuration = 3;
		[SerializeField] private float defaultGravityScale = 1;
		private float timeEnabled;

		private void OnValidate()
		{
			if (playerRb != null)
				defaultGravityScale = playerRb.gravityScale;
		}

		private void Reset()
		{
			playerRb = GetComponent<Rigidbody2D>();
		}

		public void OnWallStick()
		{
			enabled = true;
			playerRb.gravityScale = 0;
			playerRb.velocity = Vector3.zero;
			timeEnabled = Time.time;
			startSlideEvent.Invoke();
			PlayerAnimator.SetIsStickingWall(true);
		}

		public void OnWallLeap()
		{
			playerRb.gravityScale = defaultGravityScale;
			enabled = false;
			PlayerAnimator.SetIsStickingWall(false);
		}

		private void Update()
		{
			playerRb.gravityScale = Mathf.InverseLerp(timeEnabled + stickDuration, timeEnabled + stickDuration + slideDuration, Time.time) * defaultGravityScale;
		}
	}
}
