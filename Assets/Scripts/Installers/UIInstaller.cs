using Diwide.Arkanoid;
using Diwide.Arkanoid.UI;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private PauseMenu _pauseMenu;
    
    public override void InstallBindings()
    {
        Container.Bind<PauseMenu>().FromInstance(_pauseMenu);
        Container.BindSignal<GamePausedSignal>()
            .ToMethod<PauseMenu>((c,s)=> 
                c.gameObject.SetActive(s.isPaused)).FromResolve();
    }
}