using DG.Tweening;
using UnityEngine;

public class MoveToPointTween : MonoBehaviour {
  [SerializeField]
  private float _duration;
  private Sequence _sequence;

  public void MoveAnimation(Vector3 vectorMove) {
    if (_sequence != null) {
      Stop(vectorMove);
    }

    _sequence = DOTween.Sequence();

    _sequence.Append(transform.DOMove(vectorMove, _duration));
  }

  private void Stop(Vector3 vectorMove) {
    _sequence?.Kill();
    _sequence.Append(transform.DOMove(vectorMove, _duration));
  }
}