using System.Collections.Generic;
using UnityEngine;

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
		[Header("Directional Colliders")]
		[SerializeField] private GameObject upCollider;
		[SerializeField] private GameObject downCollider;
		[SerializeField] private List<GameObject> sidewaysColliders;
		[Header("Player properties")]
		[SerializeField] private float playerFriction = 5;

		public float FacingDirection { get; set; }

		private void Reset()
		{
			move = GetComponent<PlayerMover>();
			jump = GetComponent<PlayerJumper>();
			wallsStick = GetComponent<PlayerWallsStick>();
		}

		private void Awake()
		{
			Instance = this;
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

		public void OnJump()
		{
			move.enabled = false;
			jump.enabled = false;
			upCollider.SetActive(true);
			sidewaysColliders.ForEach(g => g.SetActive(true));
		}

		public void OnLeaveGround() => OnJump();

		public void OnLand()
		{
			move.enabled = true;
			jump.enabled = true;
			wallsStick.enabled = false;
			upCollider.SetActive(false);
			sidewaysColliders.ForEach(g => g.SetActive(false));
		}

		public void OnStickToWall()
		{
			FacingDirection *= -1;
			jump.enabled = true;
			wallsStick.enabled = true;
			sidewaysColliders.ForEach(g => g.SetActive(false));
			upCollider.SetActive(false);
			//downCollider.SetActive(false);
		}

		public void OnStartSlide()
		{
			downCollider.SetActive(true);
		}

		public void OnBumpCeiling()
		{
			sidewaysColliders.ForEach(g => g.SetActive(false));
			upCollider.SetActive(false);
		}
	}
}
