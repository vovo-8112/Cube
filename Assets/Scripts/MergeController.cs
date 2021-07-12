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
  private RotationCubeController _cube;

  [SerializeField]
  private SwipeDetector _swipeDetector;

  [SerializeField]
  private List<Transform> _spawnPoint;

  private Side _side;
  private Vector3 _nextPosition;

  private Coroutine _coroutine;

  private void LateUpdate() {
    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    if (Input.GetMouseButtonDown(0)) {
      if (Physics.Raycast(ray, out hit)) {
        if (hit.collider != null && _coroutine == null && _side == null) {
          _coroutine = StartCoroutine(Click(hit));
        }
      }
    }

    if (Input.GetMouseButtonUp(0)) {
      if (_side == null&&_coroutine!=null) {
        StopCoroutine(_coroutine);
        _coroutine = null;
      }
    }

    if (Input.GetMouseButtonDown(0)) {
      if (_side != null) {
        if (Physics.Raycast(ray, out hit)) {
          TryMerge(hit);
        }
      }
    }
  }

  private void TryMerge(RaycastHit hit) {
    if (_coroutine == null) {
      return;
    }

    if (_side != null && _side._state == Side.StateSide.Merge) {
      _side = hit.collider.GetComponent<Side>();

      StopCoroutine(_coroutine);
      FindPoint();
      SetDefaultSide();
      _coroutine = null;
      _side = null;
    }
  }

  private IEnumerator Click(RaycastHit hit) {
    yield return new WaitForSeconds(_durationTap);
    EnableMode(hit);
    yield return null;
  }

  private void EnableMode(RaycastHit hit) {
    _side = hit.collider.GetComponent<Side>();
    if (_side != null) {
      _cube.SaveRotation();
      _swipeDetector.SkipSwipe();
      _side.SetMergeMode(_movePoint.position);
    }
  }

  private void SetDefaultSide() {
    _side.transform.SetParent(_cube.transform);
    if (_side.CanMerge()) {
      _side.Merge();
    } else {
      _side.MergeDenied();
      _cube.ResetRotation();
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