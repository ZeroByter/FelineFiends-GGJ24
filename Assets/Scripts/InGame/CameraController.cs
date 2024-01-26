using InGame.Player;
using UnityEngine;

namespace InGame
{
	public class CameraController : MonoBehaviour
	{
		private void Update()
		{
			transform.position = PlayerManager.GetPosition() + new Vector3(0, 0, -10);
		}
	}
}
