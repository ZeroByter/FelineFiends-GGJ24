using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Management
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance { get; private set; }

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
				DontDestroyOnLoad(this);
			}
		}

		public static void StartGame()
		{
			SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Single);
		}

		public static void RestartLevel()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		public static void ExitToMainMenu()
		{
			DOTween.KillAll();
			SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Single);
		}

		public static void ExitGame()
		{
			Application.Quit();
		}
	}
}
