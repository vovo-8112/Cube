using DG.Tweening;
using UnityEngine;

public class ShuffleAnimation : MonoBehaviour {
  [SerializeField]
  private float _duration;

  [SerializeField]
  private Ease _typeAnimation;

  private Sequence _sequenceShuffleAnimation;

  public Sequence ShuffleAnim( ) {
    _sequenceShuffleAnimation = DOTween.Sequence();
    var halftime = _duration / 2;
    _sequenceShuffleAnimation.Append(transform.DOScale(Vector3.zero, halftime).SetEase(_typeAnimation));

    _sequenceShuffleAnimation.Append(transform.DOScale(Vector3.one, halftime).SetEase(_typeAnimation));
    return _sequenceShuffleAnimation;
  }
}