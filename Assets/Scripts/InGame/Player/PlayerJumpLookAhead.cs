using DG.Tweening;
using UnityEngine;

namespace InGame.Player
{
	class PlayerJumpLookAhead : MonoBehaviour
	{
		[SerializeField] private PlayerJumper jump;
		[SerializeField] private Vector2 lookAheadAmount = Vector2.one;
		[SerializeField] private float timeToReset = 1f;

		private Vector2 CurrentLookAheadAmount => (lookAheadAmount * jump.JumpForcePercentage) * new Vector2(PlayerManager.Instance.FacingDirection, 1);

		private void OnDisable()
		{
			transform.DOLocalMove(Vector2.zero, timeToReset);
		}

		private void Update()
		{
			DOTween.Kill(transform);
			transform.localPosition = CurrentLookAheadAmount;
		}
	}
}
