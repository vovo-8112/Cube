using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class ProgressSaver {
  private const string SidesNumsPrefKey = "SidesNumsPrefKey";

  private List<int> LoadSideDate() {
    if (PlayerPrefs.HasKey(SidesNumsPrefKey)) {
      var listsaveNums = (JsonConvert.DeserializeObject<List<int>>(PlayerPrefs.GetString(SidesNumsPrefKey)));
      return listsaveNums;
    }

    return null;
  }

  public void SaveSide(List<int> list) {
    PlayerPrefs.SetString(SidesNumsPrefKey, JsonConvert.SerializeObject(list));
    PlayerPrefs.Save();
  }
}