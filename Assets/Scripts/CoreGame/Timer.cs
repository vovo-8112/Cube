using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
  [SerializeField]
  private float _maxValue;
  [SerializeField]
  private Slider _slider;
  [SerializeField]
  private ViewerController _viewerController;

  float _currCountdownValue;
  private Coroutine _coroutine;

  private void Awake() {
    _slider.maxValue = _maxValue;
    _slider.gameObject.SetActive(false);
  }

  public void RestartTimer() {
    if (!_slider.gameObject.activeSelf) {
      _slider.gameObject.SetActive(true);
    }

    if (_coroutine != null) {
      StopCoroutine(_coroutine);
      _coroutine = null;
    }

    _coroutine = StartCoroutine(StartCountdown(_maxValue));
    Core.SetGameStat(GameState.Default);
    _viewerController.SetVieState(Core.GetGameState());
  }

  private IEnumerator StartCountdown(float countdownValue) {
    _currCountdownValue = countdownValue;
    while (_currCountdownValue > 0) {
      yield return new WaitForSeconds(0.01f);
      _currCountdownValue = _currCountdownValue - 0.01f;
      SetSlider(_currCountdownValue);
    }

    Core.SetGameStat(GameState.TimeIsOver);
    _viewerController.SetVieState(Core.GetGameState());
  }

  private void SetSlider(float value) {
    _slider.value = value;
  }
}