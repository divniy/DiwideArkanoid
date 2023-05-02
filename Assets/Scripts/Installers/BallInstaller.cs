using UnityEngine;
using Zenject;

namespace Diwide.Arkanoid
{
    public class BallInstaller : Installer<BallInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<BallFacade>().AsSingle();
            Container.Bind<Transform>().FromComponentOnRoot();
            Container.Bind<BallMover>().FromComponentOnRoot();
        }
    }
}