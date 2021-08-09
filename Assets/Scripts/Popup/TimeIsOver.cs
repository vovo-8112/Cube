using UnityEngine;

using UnityEngine.SceneManagement;

public class TimeIsOver : MonoBehaviour {

  [SerializeField]
  private Timer _timer;

  private void Awake() {

  }

  private void GiveUpButtonClick() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  private void AddTimeButtonClick() {
    _timer.RestartTimer();
    gameObject.SetActive(false);
  }
}