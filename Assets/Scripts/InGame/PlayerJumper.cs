using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static Extensions.Extensions;

public class PlayerJumper : MonoBehaviour
{
    [SerializeField] private UnityEvent<float> playerJumpEvent;
    [SerializeField] private UnityEvent playerPrepareJumpEvent;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private Vector2 force = new(100, 150);
    [SerializeField] private float chargeMaxTime = 1;
    private float startTime;
    private float facingDirection = 1; //TODO Move this to PlayerManager

    private void Reset()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    public void OnJump(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
            Debug.Log(startTime);
		}
		else if (context.started)
        {
			playerPrepareJumpEvent.Invoke();
			startTime = (float)context.startTime;
		}
        else if (context.canceled)
        {
            float timePassed = (float)context.time - startTime;
			timePassed = ClampMax(timePassed, chargeMaxTime);
			Vector2 jumpForce = force * new Vector2(facingDirection, 1) * timePassed;
			playerRb.AddForce(jumpForce, ForceMode2D.Force);
            playerJumpEvent.Invoke(timePassed);
        }
    }
}
