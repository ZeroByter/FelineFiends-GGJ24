using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UI;

namespace Management
{
	public class PauseManager : MonoBehaviour
	{
		private const string playerActionMapName = "Player";

		[SerializeField] private List<GameObject> objectsToToggle;
		[SerializeField] private InputActionAsset playerInputActions;
		private bool paused = false;

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

		private void OnApplicationFocus(bool focus)
		{
			if (!focus)
				SetPaused(true);
		}

		public void OnPause(InputAction.CallbackContext context)
		{
			if (context.started)
			{
				ToggleState();
				ForceCurrentState();
			}
		}

		public void SetPaused(bool paused)
		{
			this.paused = paused;
			Time.timeScale = paused ? 0 : 1;
			objectsToToggle.ForEach(o => o.SetActive(!paused));
			MenuManager.Instance.ShowPauseMenu(paused);
			if (paused)
				playerInputActions.FindActionMap(playerActionMapName).Disable();
			else
				playerInputActions.FindActionMap(playerActionMapName).Enable();
		}

		private void ForceCurrentState()
		{
			SetPaused(paused);
		}

		private void ToggleState()
		{
			paused = !paused;
		}
	}
}
