using System.Collections;
using UnityEngine;
using Cinemachine;

public class CinemachineCameraManager : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera _camera;
	private CinemachineBasicMultiChannelPerlin _noise;

	#region Singleton Implementation
	public static CinemachineCameraManager Instance { get; private set; }
	private void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this;
	}
	#endregion

	private void Start()
	{
		_noise = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
	}

	public void SetShake(float gain, float frequency)
	{
		StopShake();
		SetShakeParams(gain, frequency);
	}

	public void SetShake(float gain, float frequency, float duration)
	{
		StopShake();
		StartCoroutine(CameraShakeCRTN(gain, frequency, duration));
	}

	public void StopShake()
	{
		StopAllCoroutines();
		DisableShake();
	}

	private IEnumerator CameraShakeCRTN(float gain, float frequency, float duration)
	{
		SetShakeParams(gain, frequency);
		yield return new WaitForSeconds(duration);
		DisableShake();
	}

	private void SetShakeParams(float gain, float frequency)
	{
		_noise.m_AmplitudeGain = gain;
		_noise.m_FrequencyGain = frequency;
	}

	private void DisableShake()
	{
		_noise.m_AmplitudeGain = 0;
	}
}
