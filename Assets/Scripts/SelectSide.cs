using System.Collections;
using UnityEngine;

public class SelectSide : MonoBehaviour {
  [SerializeField]
  private Camera _camera;

  [SerializeField]
  private Transform _movePoint;

  [SerializeField]
  private float _durationTap;

  [SerializeField]
  private Transform _cubeTransform;

  private SideController _side;

  private Coroutine _coroutine;

  private void LateUpdate() {
    if (Input.GetMouseButtonDown(0)) {
      Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit)) {
        if (hit.collider != null && _coroutine == null) {
          _coroutine = StartCoroutine(Click(hit));
        }
      }
    }

    if (Input.GetMouseButtonUp(0)) {
      DisableModeMerge();
    }
  }

  private void DisableModeMerge() {
    if (_coroutine == null) {
      return;
    }

    StopCoroutine(_coroutine);
    SetDefaultSide();
    _coroutine = null;
  }

  private IEnumerator Click(RaycastHit hit) {
    yield return new WaitForSeconds(_durationTap);
    EnableMode(hit);
    yield return null;
  }

  private void EnableMode(RaycastHit hit) {
    _side = hit.collider.GetComponent<SideController>();
    if (_side != null) {
      _side.SetMergeMode(_movePoint.position);
    }
  }

  private void SetDefaultSide() {
    if (_side != null) {
      _side.transform.SetParent(_cubeTransform);
      _side.ResetSide();
    }
  }
}