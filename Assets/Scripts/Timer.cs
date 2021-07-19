using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
  [SerializeField]
  private float _maxValue;
  [SerializeField]
  private Slider _slider;
  float _currCountdownValue;

  private void Awake() {
    _slider.maxValue = _maxValue;
    gameObject.SetActive(false);
  }

  public void StartTime() {
    gameObject.SetActive(true);

    StartCoroutine(StartCountdown(_maxValue));
  }

  private IEnumerator StartCountdown(float countdownValue) {
    _currCountdownValue = countdownValue;
    while (_currCountdownValue > 0) {
      yield return new WaitForSeconds(0.01f);
      _currCountdownValue = _currCountdownValue - 0.01f;
      SetSlider(_currCountdownValue);
    }
  }

  private void SetSlider(float value) {
    _slider.value = value;
  }
}