using UnityEngine;
using UnityEngine.Events;

public class OnTrigger: MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEnter;
	[SerializeField] private UnityEvent onTriggerExit;
    [SerializeField] private LayerMask terrainMask;


	private void OnTriggerEnter2D(Collider2D collision) => onTriggerEnter.Invoke();

	private void OnTriggerExit2D(Collider2D collision) => onTriggerExit.Invoke();
}
