using System;
using DG.Tweening;
using UnityEngine;

public class RotationCubeController : MonoBehaviour {
  [SerializeField]
  private float _duration;

  [SerializeField]
  private Vector3 _point;

  [SerializeField]
  private GameObject _testGameObject;

  public Sequence _sequence;
  private Quaternion _oldRotation;
  public static event Action RotationText;

  public void SaveRotation() {
    _oldRotation = _testGameObject.transform.rotation;
  }

  public void ResetRotation() {
    _testGameObject.transform.rotation = _oldRotation;
    AnimRotationCube(_testGameObject.transform.rotation);
  }

  private void Awake() {
    SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.A)) {
      RotationAnim(SwipeDirection.Left);
    } else if (Input.GetKeyDown(KeyCode.D))
      RotationAnim(SwipeDirection.Right);
    else if (Input.GetKeyDown(KeyCode.W))
      RotationAnim(SwipeDirection.Up);
    else if (Input.GetKeyDown(KeyCode.S))
      RotationAnim(SwipeDirection.Down);
  }

  private void SwipeDetector_OnSwipe(SwipeData data) {
    RotationAnim(data.Direction);
  }

  private void RotationAnim(SwipeDirection data) {
    switch (data) {
      case SwipeDirection.Left:
        _testGameObject.transform.RotateAround(_point, Vector3.up, 90);
        break;
      case SwipeDirection.Right:
        _testGameObject.transform.RotateAround(_point, Vector3.down, 90);
        break;
      case SwipeDirection.Up:
        _testGameObject.transform.RotateAround(_point, Vector3.right, 90);
        break;
      case SwipeDirection.Down:
        _testGameObject.transform.RotateAround(_point, Vector3.left, 90);
        break;
    }

    RotationText?.Invoke();
    AnimRotationCube(_testGameObject.transform.rotation);
  }

  private void AnimRotationCube(Quaternion quaternion) {
    if (_sequence != null) {
      StopAnim();
    }

    _sequence = DOTween.Sequence();

    _sequence.Append(transform.DORotateQuaternion(quaternion, _duration).SetAutoKill(true));
  }

  private void StopAnim() {
    _sequence.Kill();
    _sequence.Append(transform.DORotateQuaternion(_testGameObject.transform.rotation, _duration));
  }

  private void OnDestroy() {
    SwipeDetector.OnSwipe -= SwipeDetector_OnSwipe;
  }
}