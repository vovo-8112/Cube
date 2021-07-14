using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TakeAnimation : MonoBehaviour {
  [SerializeField]
  private List<Vector3> _listScale;
  [SerializeField]
  private float _duration;
  public Sequence TakeSequence;

  public void AnimationRotation() {
    if (TakeSequence.IsActive()) {
      return;
    }

    TakeSequence = DOTween.Sequence();

    var durationScale = _duration / _listScale.Count;
    for (int i = 0; i < _listScale.Count; i++) {
      TakeSequence.Append(transform.DOScale(_listScale[i], durationScale));
    }
  }

  public void StopAnimation() {
    TakeSequence?.Kill();
    transform.DOScale(Vector3.one, _duration / _listScale.Count);
  }

  private void OnDestroy() {
    TakeSequence?.Kill();
  }
}