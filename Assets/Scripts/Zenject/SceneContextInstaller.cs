using UnityEngine;
using Zenject;

public class SceneContextInstaller : MonoInstaller<SceneContextInstaller> {
  [SerializeField]
  private SideController _sideController;

  [SerializeField]
  private InputController _inputController;

  public override void InstallBindings() {
    Container.Bind<SideController>().FromInstance(_sideController).AsSingle();
    Container.Bind<InputController>().FromInstance(_inputController).AsSingle();
  }
}