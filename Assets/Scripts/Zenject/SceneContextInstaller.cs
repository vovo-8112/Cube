using UnityEngine;
using Zenject;

public class SceneContextInstaller : MonoInstaller<SceneContextInstaller> {
  [SerializeField]
  private SideController _sideController;

  [SerializeField]
  private InputController _inputController;

  [SerializeField]
  private StreakBonusConfig _streakBonusConfig;

  public override void InstallBindings() {
    Container.Bind<IProgressSaver>().To<ProgressSaver>().AsSingle().NonLazy();
    Container.Bind<StreakBonusConfig>().FromInstance(_streakBonusConfig).AsSingle().NonLazy();
    Container.Bind<SideController>().FromInstance(_sideController).AsSingle();
    Container.Bind<InputController>().FromInstance(_inputController).AsSingle();
    Container.Bind<StreakController>().AsSingle();
  }
}