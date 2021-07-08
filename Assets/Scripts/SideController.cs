using UnityEngine;

public class SideController : MonoBehaviour {
  [SerializeField]
  private Vector3 _scaleVector3;

  private Vector3 _startPosition;
  private Quaternion _startRotation;
  private Vector3 _startScale;

  private void Awake() {
    GetStartVector();
  }

  private void GetStartVector() {
    Transform transform1 = transform;
    _startPosition = transform1.position;
    _startScale = transform1.localScale;
  }

  public void ResetSide() {
    var transform1 = transform;
    transform1.position = _startPosition;
    transform1.localScale = _startScale;
  }

  public void SetMergeMode(Vector3 _movePoint) {
    Transform transform1 = transform;
    transform1.localScale = _scaleVector3;
    transform1.SetParent(gameObject.transform.parent.parent);
    transform1.position = _movePoint;
  }
}