using Zenject;

namespace Diwide.Arkanoid
{
    public class GameModel
    {
        private int _lifesCounter;
        private bool _isPaused = false; 
        [Inject] private SignalBus _signalBus;

        public int LifesCounter
        {
            set
            {
                _lifesCounter = value;
                _signalBus.Fire(new LifesCountChange(){NewValue = value});
            }
            get { return _lifesCounter; }
        }
        
        public bool isPaused
        {
            get { return _isPaused; }
        }

        public void SetPause(bool value)
        {
            _isPaused = value;
        }
    }
}