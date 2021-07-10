using UnityEngine;

public class Side : MonoBehaviour {
  [SerializeField]
  private Vector3 _scaleVector3;

  [SerializeField]
  private GameObject _collider;

  private Vector3 _startPosition;
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
    Transform transform1 = transform;
    transform1.position = _startPosition;
    transform1.localScale = _startScale;
    _collider.gameObject.SetActive(false);
  }

  public void SetMergeMode(Vector3 _movePoint) {
    Transform transform1 = transform;
    transform1.localScale = _scaleVector3;
    transform1.SetParent(gameObject.transform.parent.parent);
    transform1.position = _movePoint;
    _collider.gameObject.SetActive(true);
  }
}