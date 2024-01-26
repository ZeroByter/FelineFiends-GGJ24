using Management;
using UnityEngine;

namespace UI
{
	public class MenuManager : MonoBehaviour
	{
		[SerializeField] private GameObject _pauseMenu;

		public static MenuManager Instance { get; private set; }

		private void Awake()
		{
			if (Instance == null)
				Instance = this;
		}

		public void ShowPauseMenu(bool show = true)
		{
			_pauseMenu.SetActive(show);
		}

		public void ReturnToGame()
		{
			_pauseMenu.SetActive(false);
			PauseManager.Instance.SetPaused(false);
		}

		public void RestartLevel() => GameManager.RestartLevel();

		public void ExitToMainMenu() => GameManager.ExitToMainMenu();
	}
}
