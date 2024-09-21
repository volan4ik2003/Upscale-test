using Code.Runtime.Infrastructure.AssetManagement;
using CodeBase.Infrastructure;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IInputService>().To<StandaloneInputService>().FromNew().AsSingle(); 
        Container.Bind<GameStateMachine>().FromNew().AsSingle();
        Container.Bind<IGameFactory>().To<GameFactory>().FromNew().AsSingle(); 
        Container.Bind<IAssetProvider>().To<AssetProvider>().FromNew().AsSingle();
    }
}
