using UnityEngine;
using Zenject;

namespace Diwide.Arkanoid
{
    public class SmoothMovementDecorator : IPlayerMover
    {
        [Inject] private float _smoothingSpeed;
        private IPlayerMover _playerMover;
        private Vector2 currentMove;
        private Vector2 smoothMoveVelocity;

        public SmoothMovementDecorator(IPlayerMover playerMover)
        {
            _playerMover = playerMover;
        }

        public void Move(Vector2 input)
        {
            currentMove = Vector2.SmoothDamp(currentMove, input, ref smoothMoveVelocity, _smoothingSpeed);
            _playerMover.Move(currentMove);
        }
    }
}