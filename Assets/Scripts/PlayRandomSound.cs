using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _clipsToPlay;

	public void Play()
	{
		Play(GetRandomClip());
	}

	private void Play(AudioClip clip)
	{
		_audioSource.clip = clip;
		_audioSource.Play();
	}

	private AudioClip GetRandomClip()
	{
		AudioClip ret;
		do
			ret = _clipsToPlay[Random.Range(0, _clipsToPlay.Length)];
		while (ret == _audioSource.clip);
		return ret;
	}
}
