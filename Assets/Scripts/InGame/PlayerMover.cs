using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [HideInInspector] public bool isOnGround;

    [SerializeField] private Rigidbody2D _playerRb;
    [SerializeField] private float _moveSpeed = 1;
    [SerializeField] private float _jumpForce = 1;

    [SerializeField] private float jumpUpVelocityMultiplier = 1.5f;
    [SerializeField] private float jumpChargeMaxTime = 3;

    private float jumpChargeStart;
    private float jumpDirection = 1;
    private bool isJumping = false;

    private bool isWalking = false;

    private void Reset()
    {
        _playerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var walkingRight = Input.GetKey(KeyCode.D);
        var walkingLeft = Input.GetKey(KeyCode.A);

        isWalking = walkingLeft || walkingRight;

        if (isOnGround && isWalking && !isJumping)
        {
            isJumping = false;

            var walkingDirection = 1;
            if (walkingLeft)
            {
                walkingDirection = -1;
            }

            _playerRb.AddForce(Vector3.right * _moveSpeed * walkingDirection * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
    }

    private void Update()
    {
        if (isOnGround)
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

                _playerRb.AddForce((Vector3.up * jumpUpVelocityMultiplier + Vector3.right * jumpDirection) * jumpCharge * _jumpForce, ForceMode2D.Force);

                isJumping = false;
            }
        }
    }
}
