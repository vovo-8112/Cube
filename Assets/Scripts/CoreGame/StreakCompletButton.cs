using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StreakCompletButton : MonoBehaviour {
  [SerializeField]
  private Button _buttonCollectRewardStreak;

  private StreakBonusConfig _streakBonusConfig;
  private StreakController _streakController;
  private SideController _sideController;

  [Inject]
  public void Construct(StreakController streakController, StreakBonusConfig streakBonusConfig,
    SideController sideController) {
    _streakController = streakController;
    _streakBonusConfig = streakBonusConfig;
    _sideController = sideController;
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
    var reward = _streakBonusConfig.StreakRewardCheat(RewardStreakType.UpAllSidesValue);
    if (reward._type == RewardStreakType.UpAllSidesValue) {
      _sideController.UpSides(reward._value);
    }

    ControllActiveGameObj(false);
  }
}