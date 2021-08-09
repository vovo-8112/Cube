using UnityEngine;

public class JokerController {
  private int _amountJokers;

  private string JokersAmountPrefKey = "JokersAmountPrefKey";

  public void AddJoker(int value) {
    _amountJokers += value;
    SavePrefKey();
  }

  private void SavePrefKey() {
    PlayerPrefs.SetInt(JokersAmountPrefKey, _amountJokers);
    PlayerPrefs.Save();
  }
}