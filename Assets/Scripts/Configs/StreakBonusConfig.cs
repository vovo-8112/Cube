using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(fileName = "StreakBonusConfig", menuName = "ScriptableObjects/StreakBonusConfig", order = 1)]
public class StreakBonusConfig : ScriptableObject {
  [SerializeField]
  private List<StreakReward> _streakRewards;

  public StreakReward GetStreakReward() {
    Random r = new Random();
    List<StreakReward> newList = new List<StreakReward>();
    foreach (var streakReward in _streakRewards) {
      if (streakReward._value != 0) {
        newList.Add(streakReward);
      }
    }

    return newList[r.Next(0, _streakRewards.Count)];
  }

  public StreakReward StreakRewardCheat(RewardStreakType type) {
    foreach (var streakReward in _streakRewards) {
      if (streakReward._type == type) {
        return streakReward;
      }
    }

    return null;
  }

  [Serializable]
  public class StreakReward {
    public RewardStreakType _type;
    [Range(0, 30)]
    public int _value;
  }
}

public enum RewardStreakType {
  AddTime,
  SpawnJoker,
  AddShuffle,
  UpAllSidesValue
}