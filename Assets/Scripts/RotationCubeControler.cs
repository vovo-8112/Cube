using System;
using DG.Tweening;
using UnityEngine;

public class RotationCubeControler : MonoBehaviour {
  [SerializeField]
  private float _duration;

  [SerializeField]
  private Camera _camera;

  private Sequence _sequence;

  private void Awake() {
    SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.A)) {
      RotationAnim(SwipeDirection.Left);
    }

    if (Input.GetKeyDown(KeyCode.W)) {
      RotationAnim(SwipeDirection.Up);
    }

    if (Input.GetKeyDown(KeyCode.D)) {
      RotationAnim(SwipeDirection.Right);
    }

    if (Input.GetKeyDown(KeyCode.S)) {
      RotationAnim(SwipeDirection.Down);
    }
  }

  private void SwipeDetector_OnSwipe(SwipeData data) {
    RotationAnim(data.Direction);
  }

  public void RotationAnim(SwipeDirection data) {
    if (_sequence == null) {
      _sequence = DOTween.Sequence();
    }

    var transform1 = transform;
    var rotation = transform1.localRotation;

    switch (data) {
      case SwipeDirection.Right:
        _sequence.Append(transform.DOLocalRotate(
          new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y + 90,
            rotation.eulerAngles.z), _duration));
        break;
      case SwipeDirection.Left:
        _sequence.Append(transform.DOLocalRotate(
          new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y - 90,
            rotation.eulerAngles.z), _duration));
        break;
      case SwipeDirection.Up:
        _sequence.Append(transform.DOLocalRotate(
          new Vector3(rotation.eulerAngles.x + 90, rotation.eulerAngles.y,
            rotation.eulerAngles.z), _duration));
        break;
      case SwipeDirection.Down:
        _sequence.Append(transform.DOLocalRotate(
          new Vector3(rotation.eulerAngles.x - 90, rotation.eulerAngles.y,
            rotation.eulerAngles.z), _duration));
        break;
    }

    // switch (data) {
    //   case SwipeDirection.Right:
    //     transform1.LookAt(_camera.transform.position, Vector3.up);
    //     break;
    //   case SwipeDirection.Left:
    //
    //     transform1.LookAt(_camera.transform.position, Vector3.down);
    //     break;
    //   case SwipeDirection.Up:
    //     transform1.LookAt(_camera.transform.position, Vector3.right);
    //
    //     break;
    //   case SwipeDirection.Down:
    //     transform1.LookAt(_camera.transform.position, Vector3.left);
    //     break;
    // }
  }
}