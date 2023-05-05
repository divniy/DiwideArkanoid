using UnityEngine;
using Zenject;

namespace Diwide.Arkanoid
{
    public class BallInstaller : Installer<BallInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BallFacade>().AsSingle();
            Container.BindSignal<LaunchBallSignal>()
                .ToMethod<BallFacade>(x=>x.Launch).FromResolve();
            
            Container.Bind<Transform>().FromComponentOnRoot();
            Container.Bind<MeshRenderer>().FromComponentOnRoot();
            // Container.Bind<Color>().FromResolveGetter<MeshRenderer>(_ => _.material.color);
            Container.Bind<BallMover>().FromComponentOnRoot();
            
            Container.BindSignal<CollideObstacleSignal>()
                .ToMethod<BallMover>(x=>x.IncreaseSpeed).FromResolve();
        }
    }
}