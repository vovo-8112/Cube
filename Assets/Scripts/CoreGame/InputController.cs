using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour {
  [SerializeField]
  private SwipeDetector _swipeDetector;
  [SerializeField]
  private MergeController _mergeController;
  [SerializeField]
  private EventSystem _input;



  //use in animation
  public void LockInput() {
    SetEnable(false);
  }

  //use in animation
  public void EnableInput() {
    SetEnable(true);
  }

  private void SetEnable(bool enable) {
    _input.enabled = enable;
    _swipeDetector.enabled = enable;
    _mergeController.enabled = enable;
  }
}