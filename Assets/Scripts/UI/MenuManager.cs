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

		private void DisableAll()
		{
			_pauseMenu.SetActive(false);
		}

		public void ShowPauseMenu()
		{
			DisableAll();
			_pauseMenu.SetActive(true);
		}
	}
}
