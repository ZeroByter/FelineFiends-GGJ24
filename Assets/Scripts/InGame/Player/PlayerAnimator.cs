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
    }

    public class PlayerAnimator : MonoBehaviour
    {
        private static PlayerAnimator Instance;

        public static void SetInstanceState(AnimatorState state)
        {
            if (Instance == null) return;

            Instance.SetState(state);
        }

        [SerializeField] private SpriteRenderer spriteRenderer;

        [SerializeField] private List<AnimationData> animationStates = new List<AnimationData>();

        private Dictionary<AnimatorState, AnimationData> animationStatesMap = new Dictionary<AnimatorState, AnimationData>();

        private AnimatorState currentStateKey = AnimatorState.IDLE;

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

            if(Time.time > lastFrameChange + GetCurrentAnimationSpeed())
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
                return PlayerManager.GetRigidbody().velocity.magnitude;
            }
            else
            {
                return currentState.speed;
            }
        }

        public void SetState(AnimatorState state)
        {
            currentStateKey = state;
            currentFrame = 0;

            spriteRenderer.sprite = animationStatesMap[currentStateKey].sprites[currentFrame];
        }
    }
}
