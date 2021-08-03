using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

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

  [FormerlySerializedAs("_sideSet"), SerializeField]
  private SideController _sideController;

  private RaycastHit _raycastHit;
  private Side _side;

  private void LateUpdate() {
    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
    if (Input.GetMouseButtonDown(0)) {
      if (Physics.Raycast(ray, out _raycastHit)) {
        if (_raycastHit.collider != null && _side == null) {
          Invoke(nameof(Click), _durationTap);
          _raycastHit.collider.GetComponent<Side>().TakeAnimation();
        }
      }
    }

    if (Input.GetMouseButtonUp(0)) {
      if (_side == null) {
        if (_raycastHit.collider != null) {
          _raycastHit.collider.GetComponent<Side>().TakeAnimationStop();
          CancelInvoke(nameof(Click));
          _swipeDetector.MergeMode(false);
        }
      }
    }

    if (Input.GetMouseButtonUp(0)) {
      if (_side == null) return;
      {
        TryMerge();
        _swipeDetector.MergeMode(false);
      }
    }
  }

  private void TryMerge() {
    if (_side.state == Side.StateSide.Merge) {
      _side.transform.SetParent(_cube.transform);
      if (_side.CanMerge())
        Merge();
      else {
        MergeDenied();
      }

      CheckGameOver();
      _side = null;
    }
  }

  private void Merge() {
    _side.Merge(_sideController.GetRandomNum());
  }

  private void MergeDenied() {
    _side.MergeDenied();
    _cube.ResetRotation();
  }

  private void Click() {
    EnableMode(_raycastHit);
  }

  private void EnableMode(RaycastHit hit) {
    _side = hit.collider.GetComponent<Side>();
    if (_side != null) {
      _cube.SaveRotation();
      _swipeDetector.MergeMode(true);
      _side.SetMergeMode(_movePoint.position);
    }
  }

  private void CheckGameOver() {
    if (_sideController.IsGameOver()) {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
  }
}