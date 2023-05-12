using Zenject;

namespace Diwide.Arkanoid.UI
{
    public class PauseMenu : MainMenu
    {
        [Inject] private SignalBus _signalBus;
        public void OnResumeGame()
        {
            _signalBus.Fire(new GamePausedSignal(){isPaused = false});
        }
    }
}