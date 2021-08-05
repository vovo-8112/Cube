using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StreakCompletButton : MonoBehaviour {
  [SerializeField]
  private Button _buttonCollectRewardStreak;

  private StreakBonusConfig _streakBonusConfig;
  private StreakController _streakController;

  [Inject]
  public void Construct(StreakController streakController, StreakBonusConfig streakBonusConfig) {
    _streakController = streakController;
    _streakBonusConfig = streakBonusConfig;
  }

  private void Start() {
    _buttonCollectRewardStreak.onClick.AddListener(CollectRewardStreakClick);
    _streakController.IsStreakComplete += ControllActiveGameObj;
    gameObject.SetActive(false);
  }

  private void ControllActiveGameObj(bool active) {
    gameObject.SetActive(active);
  }

  private void CollectRewardStreakClick() {
    _streakController.ResetStreakProggres();
    var reward = _streakBonusConfig.GetStreakReward();
    Debug.Log(reward._type + reward._value);
    ControllActiveGameObj(false);
  }
}