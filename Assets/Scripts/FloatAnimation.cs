using UnityEngine;
using DG.Tweening;

class FloatAnimation : MonoBehaviour
{
	[SerializeField] private float _movement = 0.5f;
	[SerializeField] private float _duration = 1f;

	private void OnEnable() => transform.DOLocalMoveY(_movement, _duration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);

	private void OnDisable() => DOTween.Kill(transform);
}
