using System;
using UnityEngine;

public class SwipeDetector : MonoBehaviour {
  private Vector2 _fingerDownPosition;
  private Vector2 _fingerUpPosition;

  [SerializeField]
  private bool _detectSwipeOnlyAfterRelease;

  [SerializeField]
  private float _minDistanceForSwipe = 20f;

  private bool _skipSwipe;

  public static event Action<SwipeData> OnSwipe = delegate { };

  public void MergeMode(bool release) {
    _detectSwipeOnlyAfterRelease = !release;
  }

  public void SkipSwipe() {
    _skipSwipe = true;
  }

  private void Awake() {
    Input.multiTouchEnabled = false;
  }

  private void Update() {
#if UNITY_EDITOR

    if (Input.GetMouseButtonDown(0)) {
      _fingerUpPosition = Input.mousePosition;
      _fingerDownPosition = Input.mousePosition;
    }

    if (!_detectSwipeOnlyAfterRelease && !Input.GetMouseButtonDown(0)) {
      _fingerDownPosition = Input.mousePosition;
      DetectSwipe();
    }

    if (Input.GetMouseButtonUp(0)) {
      _fingerDownPosition = Input.mousePosition;
      DetectSwipe();
    }
#else
{
      foreach (Touch touch in Input.touches) {
      if (touch.phase == TouchPhase.Began) {
        _fingerUpPosition = touch.position;
        _fingerDownPosition = touch.position;
      }

      if (!_detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved) {
        _fingerDownPosition = touch.position;
        DetectSwipe();
      }

      if (touch.phase == TouchPhase.Ended) {
        _fingerDownPosition = touch.position;
        DetectSwipe();
      }
    }
}
#endif
  }

  private void DetectSwipe() {
    if (!_skipSwipe) {
      if (SwipeDistanceCheckMet()) {
        if (IsVerticalSwipe()) {
          SwipeDirection direction =
            _fingerDownPosition.y - _fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
          SendSwipe(direction);
        } else {
          SwipeDirection direction = _fingerDownPosition.x - _fingerUpPosition.x > 0
            ? SwipeDirection.Right
            : SwipeDirection.Left;
          SendSwipe(direction);
        }

        _fingerUpPosition = _fingerDownPosition;
      }
    }

    _skipSwipe = false;
  }

  private bool IsVerticalSwipe() {
    return VerticalMovementDistance() > HorizontalMovementDistance();
  }

  private bool SwipeDistanceCheckMet() {
    return VerticalMovementDistance() > _minDistanceForSwipe || HorizontalMovementDistance() > _minDistanceForSwipe;
  }

  private float VerticalMovementDistance() {
    return Mathf.Abs(_fingerDownPosition.y - _fingerUpPosition.y);
  }

  private float HorizontalMovementDistance() {
    return Mathf.Abs(_fingerDownPosition.x - _fingerUpPosition.x);
  }

  private void SendSwipe(SwipeDirection direction) {
    SwipeData swipeData = new SwipeData() {
      Direction = direction,
      StartPosition = _fingerDownPosition,
      EndPosition = _fingerUpPosition
    };
    OnSwipe?.Invoke(swipeData);
  }
}

public struct SwipeData {
  public Vector2 StartPosition;
  public Vector2 EndPosition;
  public SwipeDirection Direction;
}

public enum SwipeDirection {
  Up,
  Down,
  Left,
  Right
}