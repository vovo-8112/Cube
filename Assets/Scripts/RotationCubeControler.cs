using DG.Tweening;
using UnityEngine;

public class RotationCubeControler : MonoBehaviour {
  [SerializeField]
  private float _duration;

  [SerializeField]
  private Vector3 _point;

  [SerializeField]
  private GameObject _testGameObject;

  private Sequence _sequence;

  private void Awake() {
    SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.A)) {
      RotationTest(SwipeDirection.Right);
    } else if (Input.GetKeyDown(KeyCode.D))
      RotationTest(SwipeDirection.Left);
    else if (Input.GetKeyDown(KeyCode.W))
      RotationTest(SwipeDirection.Up);
    else if (Input.GetKeyDown(KeyCode.S))
      RotationTest(SwipeDirection.Down);
  }

  private void SwipeDetector_OnSwipe(SwipeData data) {
    RotationTest(data.Direction);
  }

  private void RotationTest(SwipeDirection data) {
    switch (data) {
      case SwipeDirection.Right:
        _testGameObject.transform.RotateAround(_point, Vector3.up, 90);
        break;
      case SwipeDirection.Left:
        _testGameObject.transform.RotateAround(_point, Vector3.down, 90);
        break;
      case SwipeDirection.Up:
        _testGameObject.transform.RotateAround(_point, Vector3.right, 90);
        break;
      case SwipeDirection.Down:
        _testGameObject.transform.RotateAround(_point, Vector3.left, 90);
        break;
    }

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
}