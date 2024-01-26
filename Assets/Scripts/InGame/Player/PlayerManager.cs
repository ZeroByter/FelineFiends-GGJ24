using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using InGame.Collectibles;

namespace InGame.Player
{
	public class PlayerManager : MonoBehaviour
	{
		public static PlayerManager Instance;

		public static float GetPlayerFriction()
        {
			if (Instance == null) return 0;

			return Instance.playerFriction;
        }

		public static Rigidbody2D GetRigidbody()
        {
			if (Instance == null) return null;

			return Instance.playerRb;
        }

		[Header("Movement Components")]
		[SerializeField] private Rigidbody2D playerRb;
		[SerializeField] private PlayerMover move;
		[SerializeField] private PlayerJumper jump;
		[SerializeField] private PlayerWallsStick wallsStick;
		[Header("Directional Triggers")]
		[SerializeField] private List<GameObject> sidewaysTriggers;
		[Header("Player properties")]
		[SerializeField] private float playerFriction = 5;
		[Header("Events")]
		[SerializeField] private UnityEvent onBumpCeiling;
		[SerializeField] private UnityEvent onLand;
		[SerializeField] private UnityEvent<BaseCollectible> onCollect;

		public float FacingDirection { get; set; }

		private void Reset()
		{
			playerRb = GetComponent<Rigidbody2D>();

			move = GetComponent<PlayerMover>();
			jump = GetComponent<PlayerJumper>();
			wallsStick = GetComponent<PlayerWallsStick>();
		}

		private void Awake()
		{
			Instance = this;
			FacingDirection = 1;
		}

		public static Vector3 GetPosition()
		{
			if (Instance == null)
				return Vector3.zero;

			return Instance.transform.position;
		}

		public void Start()
		{
			OnLand();
		}

		public void OnJump() => OnLeaveGround();

		public void OnLeaveGround()
		{
			move.enabled = false;
			jump.enabled = false;
			wallsStick.enabled = false;
			sidewaysTriggers.ForEach(t => t.SetActive(true));

			PlayerAnimator.SetIsOnGround(false);
		}

		public void OnLand()
		{
			move.enabled = true;
			jump.enabled = true;
			wallsStick.enabled = false;
			sidewaysTriggers.ForEach(t => t.SetActive(false));
			onLand.Invoke();

			PlayerAnimator.SetIsOnGround(true);
		}

		public void OnStickToWall()
		{
			FacingDirection *= -1;
			jump.enabled = true;
			wallsStick.OnWallStick();
		}

		public void OnStartSlide() { }

		public void OnBumpCeiling()
		{
			onBumpCeiling.Invoke();
		}

		public void OnCollectItem(BaseCollectible collectible)
		{
			onCollect.Invoke(collectible);
		}
	}
}
