using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
	[SerializeField] private Rigidbody2D _playerRb;
	[SerializeField] private float _moveSpeed = 1;
	[SerializeField] private float _jumpForce = 1;

	private void Reset()
	{
		_playerRb = GetComponent<Rigidbody2D>();
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		Debug.Log(context.ReadValue<float>());
		Vector2 move = (context.ReadValue<float>() * _moveSpeed) * Vector2.right;
		_playerRb.AddForce(move);
	}

	public void PlayerJump(InputAction.CallbackContext context)
	{
		Vector2 jump = (context.ReadValue<float>() * _jumpForce) * Vector2.up;
		_playerRb.AddForce(jump);
	}
}
