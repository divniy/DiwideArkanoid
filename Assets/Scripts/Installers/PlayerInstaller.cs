using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Diwide.Arkanoid
{
    public class PlayerInstaller : Installer<PlayerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerFacade>().AsSingle();
            Container.Bind<Transform>().FromComponentOnRoot();
            Container.Bind<PlayerInput>().FromComponentOnRoot();
            Container.Bind<PlayerInputHandler>().FromComponentOnRoot();
        }
    }
}