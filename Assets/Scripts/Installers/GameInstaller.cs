using UnityEngine;
using Zenject;

namespace Diwide.Arkanoid
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _ballPrefab;
        [SerializeField] private Transform[] _playerSpawns;


        // [SerializeField] private GameObject _player2Spawn;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<LaunchBallSignal>().OptionalSubscriber();
            
            Container.BindFactory<PlayerFacade, PlayerFacade.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<PlayerInstaller>(_playerPrefab);

            Container.BindFactory<BallFacade, BallFacade.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<BallInstaller>(_ballPrefab);

            Container.Bind<WellHandler>().FromComponentsInHierarchy();

            Container.Bind<GameManager>().AsSingle();
            
            Container.BindInterfacesTo<GameInitializer>()
                .AsSingle()
                .WithArguments(_playerSpawns)
                .NonLazy();
        }
    }
    
    public class LaunchBallSignal {}

    public class MissedBallSignal
    {
     //   public BallFacade BallFacade;
    }
}