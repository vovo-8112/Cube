using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeController : MonoBehaviour {
  [SerializeField]
  private Camera _camera;

  [SerializeField]
  private Transform _movePoint;

  [SerializeField]
  private float _durationTap;

  [SerializeField]
  private Transform _cubeTransform;

  [SerializeField]
  private SwipeDetector _swipeDetector;

  [SerializeField]
  private List<Transform> _spawnPoint;

  private Side _side;
  private Vector3 _nextPosition;

  private Coroutine _coroutine;

  private void LateUpdate() {
    if (Input.GetMouseButtonDown(0)) {
      Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit)) {
        if (hit.collider != null && _coroutine == null&&_side==null) {
          _coroutine = StartCoroutine(Click(hit));
        }
      }
    }

    if (Input.GetMouseButtonDown(0)) {
      if (_side != null) {
        DisableModeMerge();
      }
    }
  }

  private void DisableModeMerge() {
    if (_coroutine == null) {
      return;
    }

    StopCoroutine(_coroutine);
    FindPoint();
    SetDefaultSide();
    _side.transform.position = _nextPosition;
    _coroutine = null;
    _side = null;
  }

  private IEnumerator Click(RaycastHit hit) {
    yield return new WaitForSeconds(_durationTap);
    EnableMode(hit);
    yield return null;
  }

  private void EnableMode(RaycastHit hit) {
    _side = hit.collider.GetComponent<Side>();
    if (_side != null) {
      _swipeDetector.SkipSwipe();
      _side.SetMergeMode(_movePoint.position);
    }
  }

  private void SetDefaultSide() {
    if (_side != null) {
      _side.transform.SetParent(_cubeTransform);
      _side.Merge();
      // _side.ResetSide();
      //TODO RESET SIDE : IF CAN`T Merge
    }
  }

  private void FindPoint() {
    float distanceToClosestEnemy = Mathf.Infinity;
    Transform point = null;

    foreach (Transform currentEnemy in _spawnPoint) {
      float distanceToEnemy = (currentEnemy.transform.position - _side.transform.position).sqrMagnitude;
      if (distanceToEnemy < distanceToClosestEnemy) {
        distanceToClosestEnemy = distanceToEnemy;
        point = currentEnemy;
        _nextPosition = point.position;
      }
    }

    if (point is { }) Debug.DrawLine(transform.position, point.transform.position);
  }
}