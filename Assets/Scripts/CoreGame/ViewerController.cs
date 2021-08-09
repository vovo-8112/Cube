using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewerController : MonoBehaviour {
  [SerializeField]
  private GameObject _gamePanel;



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

  }

  private void EnableTimeIsOverPane() {
    _gamePanel.SetActive(false);
  }
}