using UnityEngine;
using UnityEngine.InputSystem;
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
            Container.DeclareSignal<CollideObstacleSignal>();
            
            Container.BindFactory<GameObject, PlayerFacade, PlayerFacade.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<PlayerInstaller>(_playerPrefab);

            Container.BindFactory<BallFacade, BallFacade.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<BallInstaller>(_ballPrefab);

            Container.Bind<WellHandler>().FromComponentsInHierarchy().AsTransient();

            Container.Bind<ObstacleView>().FromComponentsInHierarchy().AsTransient();
            
            Container.BindInterfacesAndSelfTo<GameManager>()
                .AsSingle()
                .WithArguments(_playerSpawns)
                .NonLazy();
            Container.BindSignal<MissedBallSignal>()
                .ToMethod<GameManager>(_ => _.OnBallMissing).FromResolve();
        }
        
        // Hijqck PlayerInput to force single device on it!
        // (Seems like Input System's issue/feature. Needs better workaround. )
        private void OnPlayerJoined(PlayerInput pi)
        {
            pi.SwitchCurrentControlScheme($"Player {pi.playerIndex + 1}", Keyboard.current);
        }
    }
    
    public class LaunchBallSignal {}
    public class MissedBallSignal { }
    public class CollideObstacleSignal {}
}