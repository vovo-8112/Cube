using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewerController : MonoBehaviour {
  [SerializeField]
  private GameObject _gamePanel;

  [SerializeField]
  private TimeIsOver _timeIsOverPanel;

  public void SetVieState(GameState gameState) {
    if (gameState == GameState.Default) {
      EnableGamePanel();
    } else {
      if (gameState == GameState.TimeIsOver) {
        EnableTimeIsOverPane();
      }
    }
  }

  private void EnableGamePanel() {
    _gamePanel.SetActive(true);
    _timeIsOverPanel.gameObject.SetActive(false);
  }

  private void EnableTimeIsOverPane() {
    _gamePanel.SetActive(false);
    _timeIsOverPanel.gameObject.SetActive(true);
  }
}