using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StreakView : MonoBehaviour {
  [SerializeField]
  private Image _circleStreak;

  private StreakController _streakController;

  [Inject]
  public void Construct(StreakController streakController) {
    _streakController = streakController;
  }

  private void Start() {
    _streakController.OnStreakValueUpdate += SetValue;
    _streakController.OnStreakReset += ResetCurrentProgress;
  }

  private void SetValue(int value) {
    float progres = (float) value / _streakController.MaxStreakValue;
    SetCircle(progres);
  }

  private void ResetCurrentProgress(int value) {
    float progres = (float) value / _streakController.MaxStreakValue;
    SetCircle(progres);
  }

  private void SetCircle(float value) {
    _circleStreak.fillAmount = value;
  }
}