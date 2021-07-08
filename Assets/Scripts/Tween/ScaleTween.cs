using DG.Tweening;
using UnityEngine;

public class ScaleTween : MonoBehaviour {
  [SerializeField]
  private float _duration;
  private Sequence _sequence;

  public void ScaleAnimation(Vector3 vectorScale) {
    if (_sequence != null) {
      Stop(vectorScale);
    }

    _sequence = DOTween.Sequence();

    _sequence.Append(transform.DOMove(vectorScale, _duration));
  }

  private void Stop(Vector3 vectorScale) {
    _sequence?.Kill();
    _sequence.Append(transform.DOMove(vectorScale, _duration));
  }
}