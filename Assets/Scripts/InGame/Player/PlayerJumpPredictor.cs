using UnityEngine;

namespace InGame.Player
{
	class ObjectLaunchPredictor : MonoBehaviour
	{
		[SerializeField] private Rigidbody2D playerRb;
		[SerializeField] private PlayerJumper jump;
		[SerializeField] private LineRenderer lineRenderer;
		[SerializeField] private int steps;
		[SerializeField] private float distance;

		private Vector2 JumpVelocity => jump.JumpVelocity;
		private Transform JumpRoot => jump.transform;

		private void OnValidate()
		{
			lineRenderer.positionCount = steps;
		}

		private void Update()
		{
			float delta = distance / steps;
			for (int i = 0; i < steps; i++)
				lineRenderer.SetPosition(i, GetPosition(i * delta));
		}

		private Vector2 GetPosition(float t)
		{
			return (Vector2)JumpRoot.position + (t * JumpVelocity) + 0.5f * t * t * Physics2D.gravity * playerRb.gravityScale;
		}
	}
}
