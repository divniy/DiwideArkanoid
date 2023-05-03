using UnityEngine;
using Zenject;

namespace Diwide.Arkanoid
{
    public class BallInstaller : Installer<BallInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<BallFacade>().AsSingle();
            Container.BindSignal<LaunchBallSignal>()
                .ToMethod<BallFacade>(x=>x.Launch).FromResolve();
            
            Container.Bind<Transform>().FromComponentOnRoot();
            Container.Bind<BallMover>().FromComponentOnRoot();
            
            Container.BindSignal<CollideObstacleSignal>()
                .ToMethod<BallMover>(x=>x.IncreaseSpeed).FromResolve();
        }
    }
}