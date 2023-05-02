using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Diwide.Arkanoid
{
    public class PlayerInstaller : Installer<PlayerInstaller>
    {
        [Inject] private GameObject _spawnPoint;
        [Inject] private string _inputScheme;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerFacade>().AsSingle().WithArguments(_spawnPoint);
            Container.Bind<Transform>().FromComponentOnRoot();
            Container.Bind<PlayerInput>().FromComponentOnRoot();
            Container.Bind<PlayerInputHandler>().FromComponentOnRoot();
            Container.BindInstance(_inputScheme).WhenInjectedInto<PlayerInputHandler>();
        }
    }
}