using UnityEngine;
using Zenject;

namespace Diwide.Arkanoid
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _ballPrefab;
        [SerializeField] private GameObject[] _playerSpawns;
        // [SerializeField] private WellHandler[] _wellHandlers;


        // [SerializeField] private GameObject _player2Spawn;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<LaunchBallSignal>().OptionalSubscriber();
            Container.DeclareSignal<MissedBallSignal>();
            
            Container.BindFactory<GameObject, string, PlayerFacade, PlayerFacade.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<PlayerInstaller>(_playerPrefab);

            Container.BindFactory<BallFacade, BallFacade.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<BallInstaller>(_ballPrefab);

            // Container.Bind<WellHandler>().FromComponentsInHierarchy().AsTransient();
            // Container.BindInstances(_wellHandlers);

            // Container.Bind<GameManager>().AsSingle();
            
            Container.BindInterfacesTo<GameManager>()
                .AsSingle()
                .WithArguments(_playerSpawns)
                .NonLazy();
        }
    }
    
    public class LaunchBallSignal {}

    public class MissedBallSignal
    {
        // public PlayerFacade player;
        //   public BallFacade BallFacade;
    }
}