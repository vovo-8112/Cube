using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class ButtonReload : MonoBehaviour {
  private IProgressSaver _progressSaver;

  [Inject]
  public void Construct(IProgressSaver progressSaver) {
    _progressSaver = progressSaver;
  }

  private void Start() {
    GetComponent<Button>().onClick.AddListener(ReloadScene);
  }

  private void ReloadScene() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    _progressSaver.ClearSideProgress();
  }
}