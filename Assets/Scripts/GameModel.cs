using Zenject;

namespace Diwide.Arkanoid
{
    public class GameModel
    {
        private int _lifesCounter;
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
    }
}