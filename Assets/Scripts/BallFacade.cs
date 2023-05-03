using UnityEngine;
using Zenject;

namespace Diwide.Arkanoid
{
    public class BallFacade
    {
        [Inject] private BallMover _ballMover;
        [Inject] public Transform transform;

        public void ResetToPlayer(PlayerFacade playerFacade)
        {
            _ballMover.StopMoving();
            transform.SetParent(playerFacade.transform, false);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        public void Launch()
        {
            if(_ballMover.IsMoving) return;
            Debug.Log("Launch ball");
            transform.SetParent(null);
            _ballMover.StartMoving();
        }

        public class Factory : PlaceholderFactory<BallFacade> { }
    }
}