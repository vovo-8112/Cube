using UnityEngine;
using UnityEngine.UI;

public class StreakView : MonoBehaviour {
  [SerializeField]
  private Image _circleStreak;

  [SerializeField]
  private int _canCollectStreak;

  private float _currentProgress;

  public void SetValue(int value = 1) {
    _currentProgress += (float) value / _canCollectStreak;
    Mathf.Clamp(_currentProgress, 0, 1);
    SetCircle(_currentProgress);
  }

  public void ResetCurrentProgress() {
    _currentProgress = 0;
    SetCircle(_currentProgress);
  }

  private void SetCircle(float value) {
    _circleStreak.fillAmount = value;
  }
}