using UnityEngine;

public class InputController : MonoBehaviour {
  [SerializeField]
  private SwipeDetector _swipeDetector;
  [SerializeField]
  private MergeController _mergeController;

  public void LockInput() {
    SetEnable(false);
  }

  public void EnableInput() {
    SetEnable(true);
  }

  private void SetEnable(bool enable) {
    _swipeDetector.enabled = enable;
    _mergeController.enabled = enable;
  }
}