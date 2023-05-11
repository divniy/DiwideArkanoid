using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Diwide.Arkanoid
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _ballPrefab;
        [SerializeField] private GameObject _obstaclePrefab;
        [SerializeField] private PlayerSpawn[] _playerSpawns;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<LaunchBallSignal>().OptionalSubscriber();
            Container.DeclareSignal<MissedBallSignal>();
            Container.DeclareSignal<CollideObstacleSignal>();
            Container.DeclareSignal<LevelCompleteSignal>();
            Container.DeclareSignal<GameCompleteSignal>();
            Container.DeclareSignal<LifesCountChange>();
            Container.DeclareSignal<GamePausedSignal>();

            Container.BindInstance(_playerSpawns);
            
            Container.BindFactory<PlayerSpawn, PlayerFacade, PlayerFacade.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<PlayerInstaller>(_playerPrefab);

            Container.BindFactory<BallFacade, BallFacade.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<BallInstaller>(_ballPrefab);

            Container.Bind<WellHandler>().FromComponentsInHierarchy().AsTransient();

            Container.Bind<ObstacleView>().FromComponentsInHierarchy(null, true).AsTransient();

            Container.BindMemoryPool<ObstacleView, ObstacleView.Pool>()
                .WithInitialSize(1).FromComponentInNewPrefab(_obstaclePrefab).UnderTransformGroup("Obstacles");

            Container.Bind<GameModel>().FromNew().AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameManager>()
                .AsSingle()
                .NonLazy();
            Container.BindSignal<MissedBallSignal>()
                .ToMethod<GameManager>(_ => _.OnBallMissing).FromResolve();

            Container.Bind<LevelManager>().AsSingle();
            Container.BindSignal<LevelCompleteSignal>()
                .ToMethod<GameManager>((c, s) =>
                {
                    c.OnLevelComplete(s.index);
                }).FromResolve();
            Container.BindSignal<GameCompleteSignal>()
                .ToMethod<GameManager>(_ => _.GameComplete).FromResolve();
            
            Container.BindSignal<GamePausedSignal>()
                .ToMethod<GameManager>((c, s) =>
                {
                    c.SetPause(s.isPaused);
                })
                .FromResolve();
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

    public class LifesCountChange
    {
        public int NewValue;
    }
    public class LevelCompleteSignal
    {
        public int index;
    }
    public class GameCompleteSignal {}

    public class GamePausedSignal
    {
        public bool isPaused;
    }
}