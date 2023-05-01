using UnityEngine;
using Zenject;

namespace Diwide.Arkanoid
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform[] _playerSpawns;
        // [SerializeField] private GameObject _player2Spawn;

        public override void InstallBindings()
        {
            Container.BindFactory<PlayerFacade, PlayerFacade.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<PlayerInstaller>(_playerPrefab);

            Container.BindInterfacesTo<GameInitializer>()
                .AsSingle()
                .WithArguments(_playerSpawns)
                .NonLazy();
        }
    }
}