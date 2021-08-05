using System;

public class StreakController {
  public Action<int> OnStreakReset;

  public Action<int> OnStreakValueUpdate;
  public int _currentProgress;

  public int MaxStreakValue = 4;

  public StreakController() {
    ResetStreakProggres();
  }

  public void AddProgres() {
    if (_currentProgress == MaxStreakValue) {
      return;
    }

    _currentProgress++;
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