using UnityEngine;

namespace InGame.Player
{
	class ObjectLaunchPredictor : MonoBehaviour
	{
		[SerializeField] private PlayerJumper jump;
		[SerializeField] private LineRenderer lineRenderer;
		[SerializeField] private int steps;
		[SerializeField] private float distance;

		private float LaunchVelocity => jump.JumpVelocity;
		private Vector2 LaunchDirection => jump.JumpDirection.normalized;
		private Transform ObjectShootRoot => jump.transform;

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
			return (Vector2)ObjectShootRoot.position + (LaunchVelocity * t * LaunchDirection) + 0.5f * t * t * Physics2D.gravity;
		}
	}
}
