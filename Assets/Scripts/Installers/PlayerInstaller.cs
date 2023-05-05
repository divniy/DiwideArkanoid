using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Diwide.Arkanoid
{
    public class PlayerInstaller : Installer<PlayerInstaller>
    {
        [Inject] private Settings _settings;
        [Inject] private PlayerSpawn _spawnPoint;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_spawnPoint);
            Container.BindInterfacesAndSelfTo<PlayerFacade>().AsSingle().WithArguments(_spawnPoint);
            Container.Bind<Transform>().FromComponentOnRoot();
            Container.Bind<PlayerInput>().FromComponentOnRoot();
            Container.Bind<PlayerInputHandler>().FromComponentOnRoot();
            // Container.BindInstance(_inputScheme).WhenInjectedInto<PlayerInputHandler>();
            Container.Bind<IPlayerMover>().To<PlayerController>().AsSingle().WithArguments(_settings.moveSpeed);
            Container.Decorate<IPlayerMover>().With<SmoothMovementDecorator>().WithArguments(_settings.smoothingSpeed);
        }
        
        [Serializable]
        public class Settings
        {
            public float moveSpeed;
            public float smoothingSpeed;
        }
    }
}