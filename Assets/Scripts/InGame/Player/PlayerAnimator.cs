using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Player
{
    public enum AnimatorState
    {
        IDLE,
        WALKING,
        JUMP_CHARGE,
        JUMP, // in air
        WALL_STICK,
    }

    [Serializable]
    public struct AnimationData
    {
        public AnimatorState state;
        public Sprite[] sprites;
        public float speed;
        public bool velocityBasedSpeed;
        public bool inverseDirection;
    }

    public class PlayerAnimator : MonoBehaviour
    {
        private static PlayerAnimator Instance;

        public static void SetIsWalking(bool isWalking)
        {
            if (Instance == null) return;

            Instance.isWalking = isWalking;
            Instance.UpdateCurrentState();
        }

        public static void SetIsJumpCharging(bool isJumpCharging)
        {
            if (Instance == null) return;

            Instance.isJumpCharging = isJumpCharging;
            Instance.UpdateCurrentState();
        }

        public static void SetIsOnGround(bool isOnGround)
        {
            if (Instance == null) return;

            Instance.isOnGround = isOnGround;
            Instance.UpdateCurrentState();
        }

        public static void SetIsStickingWall(bool isStickingWall)
        {
            if (Instance == null) return;

            Instance.isStickingWall = isStickingWall;
            Instance.UpdateCurrentState();
        }

        [SerializeField] private SpriteRenderer spriteRenderer;

        [SerializeField] private List<AnimationData> animationStates = new List<AnimationData>();

        private Dictionary<AnimatorState, AnimationData> animationStatesMap = new Dictionary<AnimatorState, AnimationData>();

        private AnimatorState currentStateKey = AnimatorState.IDLE;

        private bool isWalking;
        private bool isJumpCharging;
        private bool isOnGround;
        private bool isStickingWall;

        private float lastFrameChange;
        private int currentFrame;

        private void Reset()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Awake()
        {
            Instance = this;

            foreach (var data in animationStates)
            {
                animationStatesMap[data.state] = data;
            }
        }

        private void Update()
        {
            var currentState = animationStatesMap[currentStateKey];

            spriteRenderer.flipX = PlayerManager.Instance.FacingDirection == -1f;
            if (currentState.inverseDirection)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }

            var animationSpeed = GetCurrentAnimationSpeed();

            if (animationSpeed > 0 && Time.time > lastFrameChange + animationSpeed)
            {
                lastFrameChange = Time.time;

                currentFrame++;
                if(currentFrame > currentState.sprites.Length - 1)
                {
                    currentFrame = 0;
                }

                spriteRenderer.sprite = currentState.sprites[currentFrame];
            }
        }

        private float GetCurrentAnimationSpeed()
        {
            var currentState = animationStatesMap[currentStateKey];

            if (currentState.velocityBasedSpeed)
            {
                return PlayerManager.GetRigidbody().velocity.magnitude * currentState.speed;
            }
            else
            {
                return currentState.speed;
            }
        }

        public void UpdateCurrentState()
        {
            if (isOnGround)
            {
                if (isWalking)
                {
                    currentStateKey = AnimatorState.WALKING;
                }
                else if (isJumpCharging)
                {
                    currentStateKey = AnimatorState.JUMP_CHARGE;
                }
                else
                {
                    currentStateKey = AnimatorState.IDLE;
                }
            }
            else
            {
                if (isStickingWall)
                {
                    currentStateKey = AnimatorState.WALL_STICK;
                }
                else
                {
                    currentStateKey = AnimatorState.JUMP;
                }
            }

            currentFrame = 0;

            spriteRenderer.sprite = animationStatesMap[currentStateKey].sprites[currentFrame];
        }
    }
}
