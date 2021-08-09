using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace CoreGame {
  public class ProgressSaver : IProgressSaver {
    private const string SidesNumsPrefKey = "SidesNumsPrefKey";

    public List<int> LoadSideDate() {
      if (PlayerPrefs.HasKey(SidesNumsPrefKey)) {
        var listsaveNums = (JsonConvert.DeserializeObject<List<int>>(PlayerPrefs.GetString(SidesNumsPrefKey)));
        return listsaveNums;
      }

      return null;
    }

    public void SaveSide(List<int> sidesNums) {
      PlayerPrefs.SetString(SidesNumsPrefKey, JsonConvert.SerializeObject(sidesNums));
      PlayerPrefs.Save();
    }

    public void ClearSideProgress() {
      PlayerPrefs.DeleteKey(SidesNumsPrefKey);
      PlayerPrefs.Save();
    }
  }
}