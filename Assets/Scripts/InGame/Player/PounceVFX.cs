using Extensions;
using UnityEngine;
using static Extensions.Extensions;

namespace InGame.Player
{
	public class PounceVFX : MonoBehaviour
	{
		[SerializeField] private Transform vfxRoot;
		[SerializeField] private ParticleSystem particleSystem;
		[SerializeField] private Rigidbody2D playerRb;
		private float maxEmissionRate;

		private Vector2 VFXDirection => playerRb.velocity;

		private void Start()
		{
			maxEmissionRate = particleSystem.emission.rateOverTimeMultiplier;
		}

		public void OnJump(float force)
		{
			var emission = particleSystem.emission;
			emission.rateOverTime = maxEmissionRate * force;
			Vector2 target = (Vector2)vfxRoot.position + VFXDirection;
			vfxRoot.up = Direction(vfxRoot.position, target);
			particleSystem.Play();
		}
	}
}
