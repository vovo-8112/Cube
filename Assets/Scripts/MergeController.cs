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
  private SideSet _sideSet;

  private RaycastHit _raycastHit;
  private Side _side;

  private void LateUpdate() {
    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
    if (Input.GetMouseButtonDown(0))
      if (Physics.Raycast(ray, out _raycastHit))
        if (_raycastHit.collider != null && _side == null)
          Invoke(nameof(Click), _durationTap);

    if (Input.GetMouseButtonUp(0))
      if (_side == null)
        CancelInvoke(nameof(Click));

    if (Input.GetMouseButtonDown(0))
      if (_side != null)
        if (Physics.Raycast(ray, out _raycastHit))
          TryMerge(_raycastHit);
  }

  private void TryMerge(RaycastHit hit) {
    if (_side != null && _side._state == Side.StateSide.Merge) {
      _side = hit.collider.GetComponent<Side>();

      SetDefaultSide();
      _side = null;
    }
  }

  private void Click() {
    EnableMode(_raycastHit);
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
    if (_side.CanMerge())
      _side.Merge(_sideSet.GetRandomNum());
    else {
      _side.MergeDenied();
      _cube.ResetRotation();
      //TODO RESET SIDE : IF CAN`T Merge
    }
  }
}