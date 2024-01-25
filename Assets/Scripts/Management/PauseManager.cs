using UnityEngine;
using UnityEngine.InputSystem;

namespace Management
{
	public class PauseManager : MonoBehaviour
	{
		[SerializeField] private CameraPause _camera;
		[SerializeField] private GameObject[] _objectsToToggle;
		private bool _paused = false;

		public static PauseManager Instance { get; private set; }

		private void Awake()
		{
			if (Instance == null)
				Instance = this;
		}

		private void Start()
		{
			SetPaused(false);
		}

		public void OnPause(InputAction.CallbackContext context)
		{
			if (context.performed)
			{
				ToggleState();
				ForceCurrentState();
			}
		}

		public void SetPaused(bool paused)
		{
			_paused = paused;
			_camera.SetPaused(paused);
			SetAllObjects(!paused);
		}

		private void ForceCurrentState()
		{
			SetPaused(_paused);
		}

		private void SetAllObjects(bool active)
		{
			foreach (var obj in _objectsToToggle)
				obj.SetActive(active);
		}

		private void ToggleState()
		{
			_paused = !_paused;
		}

		private void OnApplicationFocus(bool focus)
		{
			if (!focus)
				SetPaused(true);
		}
	}
}
