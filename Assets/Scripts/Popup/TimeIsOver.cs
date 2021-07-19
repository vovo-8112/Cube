using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeIsOver : MonoBehaviour {
  [SerializeField]
  private Button _addTimeButton;
  [SerializeField]
  private Button _giveUpButton;
  [SerializeField]
  private Timer _timer;

  private void Awake() {
    _addTimeButton.onClick.AddListener(AddTimeButtonClick);
    _giveUpButton.onClick.AddListener(GiveUpButtonClick);
  }

  private void GiveUpButtonClick() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  private void AddTimeButtonClick() {
    _timer.RestartTimer();
    gameObject.SetActive(false);
  }
}