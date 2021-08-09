using System;

public class StreakController {
  public Action<int> OnStreakReset;

  public Action<int> OnStreakValueUpdate;

  public Action<bool> IsStreakComplete;

  public int MaxStreakValue = 5;

  private int _currentProgress;

  public StreakController() {
    ResetStreakProggres();
  }

  public void AddProgres() {
    if (_currentProgress == MaxStreakValue) {
      return;
    }

    _currentProgress++;
    if (_currentProgress == MaxStreakValue) {
      IsStreakComplete?.Invoke(true);
    }

    OnStreakValueUpdate?.Invoke(_currentProgress);
  }

  public void StreakReset() {
    if (_currentProgress < MaxStreakValue) {
      ResetStreakProggres();
    }
  }

  public void ResetStreakProggres() {
    _currentProgress = 0;
    OnStreakReset?.Invoke(_currentProgress);
  }
}