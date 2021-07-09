using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonReload : MonoBehaviour {
  private void Awake() {
    GetComponent<Button>().onClick.AddListener(ReloadScene);
  }

  private void ReloadScene() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}