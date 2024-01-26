using Extensions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

		[Header("Movement Components")]
		[SerializeField] private PlayerMover move;
		[SerializeField] private PlayerJumper jump;
		[SerializeField] private PlayerWallsStick wallsStick;
		[SerializeField] private PlayerRope rope;
		[Header("Directional Triggers")]
		[SerializeField] private List<GameObject> sidewaysTriggers;
		[Header("Player properties")]
		[SerializeField] private float playerFriction = 5;
		[Header("Events")]
		[SerializeField] private UnityEvent onBumpCeiling;
		[SerializeField] private UnityEvent onLand;
		[SerializeField] private UnityEvent<Collider2D> onEnterRope;
		[Header("Layer masks")]
		[SerializeField] private LayerMask ropeLayerMask;

		public float FacingDirection { get; set; }

		private void Reset()
		{
			move = GetComponent<PlayerMover>();
			jump = GetComponent<PlayerJumper>();
			wallsStick = GetComponent<PlayerWallsStick>();
			rope = GetComponent<PlayerRope>();
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
			rope.enabled = false;
			sidewaysTriggers.ForEach(t => t.SetActive(true));
		}

		public void OnLand()
		{
			move.enabled = true;
			jump.enabled = true;
			wallsStick.enabled = false;
			rope.enabled = false;
			sidewaysTriggers.ForEach(t => t.SetActive(false));
			onLand.Invoke();
		}

		public void OnStickToWall(Collider2D collider)
		{
            if (ropeLayerMask.Contains(collider.gameObject.layer))
            {
				rope.enabled = true;
				onEnterRope.Invoke(collider);
			}
            else
            {
				FacingDirection *= -1;
				jump.enabled = true;
				wallsStick.enabled = true;
			}
		}

		public void OnStartSlide() { }

		public void OnBumpCeiling()
		{
			onBumpCeiling.Invoke();
		}
	}
}
