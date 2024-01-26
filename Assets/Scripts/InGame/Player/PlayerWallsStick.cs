using UnityEngine;
using UnityEngine.Events;

namespace InGame.Player
{
	public class PlayerWallsStick : MonoBehaviour
	{
		[SerializeField] private UnityEvent startSlideEvent;
		[SerializeField] private Rigidbody2D playerRb;
		private float timeEnabled;

		private void Reset()
		{
			playerRb = GetComponent<Rigidbody2D>();
		}

		private void OnEnable()
		{
			playerRb.gravityScale = 0;
			playerRb.velocity = Vector3.zero;
			timeEnabled = Time.time;
		}

		private void OnDisable()
		{
			playerRb.gravityScale = 1;
		}

		private void Update()
		{
			playerRb.gravityScale = Mathf.InverseLerp(timeEnabled + 1, timeEnabled + 1 + 3, Time.time);
		}
	}
}
