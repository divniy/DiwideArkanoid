using Diwide.Arkanoid.UI;
using Zenject;

public class MenuInstaller : MonoInstaller<MenuInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<MenuManager>().FromNew().AsSingle();
        Container.Bind<MenuAnimator>().FromComponentsInHierarchy().AsTransient();
    }
}