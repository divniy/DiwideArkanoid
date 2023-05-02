using UnityEngine;
using Zenject;

namespace Diwide.Arkanoid
{
    public class BallFacade
    {
        [Inject] private BallMover _ballMover;
        [Inject] public Transform transform;
        [Inject] private SignalBus _signalBus;

        public void ResetToPlayer(PlayerFacade playerFacade)
        {
            transform.SetParent(playerFacade.transform, false);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            _ballMover.IsMoving = false;
            _signalBus.Subscribe<LaunchBallSignal>(Launch);
        }

        public void Launch()
        {
            transform.SetParent(null);
            _ballMover.IsMoving = true;
            _signalBus.Unsubscribe<LaunchBallSignal>(Launch);
        }
        
        public class Factory : PlaceholderFactory<BallFacade> { }
    }
}