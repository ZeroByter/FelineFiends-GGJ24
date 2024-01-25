using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
	[SerializeField] private UnityEvent playerStartWalking;
	[SerializeField] private UnityEvent playerStopWalking;
	[SerializeField] private Rigidbody2D playerRb;
	[SerializeField] private float moveSpeed = 1;
	public float facingDirection = 1;
	private float walkingDirection = 0;

	private void Reset()
	{
		playerRb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		Vector2 walkForce = new(moveSpeed * walkingDirection * Time.fixedDeltaTime, 0);
		playerRb.AddForce(walkForce, ForceMode2D.Impulse);
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		walkingDirection = context.ReadValue<float>();
		if (context.performed)
		{
			facingDirection = walkingDirection;
		}
		else if (context.started)
		{
			playerStartWalking.Invoke();
			facingDirection = walkingDirection;
		}
		else if (context.canceled)
			playerStopWalking.Invoke();
	}
}
