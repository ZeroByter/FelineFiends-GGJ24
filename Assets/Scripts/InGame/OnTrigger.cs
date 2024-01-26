using UnityEngine;
using UnityEngine.Events;

namespace InGame
{
	public class OnTrigger : MonoBehaviour
	{
		[SerializeField] private UnityEvent<Collider2D> onTriggerEnter;
		[SerializeField] private UnityEvent<Collider2D> onTriggerExit;
		[SerializeField] private LayerMask terrainMask;

		private void OnTriggerEnter2D(Collider2D collision) => onTriggerEnter.Invoke(collision);

		private void OnTriggerExit2D(Collider2D collision) => onTriggerExit.Invoke(collision);
	}
}
