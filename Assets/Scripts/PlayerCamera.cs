using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	[SerializeField] private float _landShakeGain = 1;
	[SerializeField] private float _landShakeFreq = 1;
	[SerializeField] private float _landShakeTime = 1;
	[SerializeField] private float _landShakeMinVelocity = 1;
	[SerializeField] private CinemachineCameraManager _cameraManager;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		float impactVelocity = collision.relativeVelocity.magnitude;
		if (impactVelocity > _landShakeMinVelocity)
			OnLand(impactVelocity);
	}

	private void OnLand(float force = 1)
	{
		_cameraManager.SetShake(_landShakeGain * force, _landShakeFreq, _landShakeTime);
	}
}
