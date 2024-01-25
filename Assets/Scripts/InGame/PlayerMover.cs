using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float jumpForce = 1;
    [SerializeField] private float jumpUpVelocityMultiplier = 1.5f;
    [SerializeField] private float jumpChargeMaxTime = 3;

    private float jumpChargeStart;
    private float jumpDirection = 1;
    private bool isJumping = false;
    private bool isWalking = false;
	private bool isGrounded;

	private void Reset()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var walkingRight = Input.GetKey(KeyCode.D);
        var walkingLeft = Input.GetKey(KeyCode.A);

        isWalking = walkingLeft || walkingRight;

        if (isGrounded && isWalking && !isJumping)
        {
            isJumping = false;

            var walkingDirection = 1;
            if (walkingLeft)
            {
                walkingDirection = -1;
            }

            playerRb.AddForce(Vector3.right * moveSpeed * walkingDirection * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
    }

    private void Update()
    {
        if (isGrounded)
        {
            if (!isWalking)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    jumpChargeStart = Time.time;
                    isJumping = true;
                    jumpDirection = 1;
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    jumpChargeStart = Time.time;
                    isJumping = true;
                    jumpDirection = -1;
                }
            }

            if (isJumping && (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E)))
            {
                var jumpCharge = Time.time - jumpChargeStart;

                playerRb.AddForce((Vector3.up * jumpUpVelocityMultiplier + Vector3.right * jumpDirection) * jumpCharge * jumpForce, ForceMode2D.Force);

                isJumping = false;
            }
        }
    }

    public void SetGrounded(bool grounded) => isGrounded = grounded;
}
