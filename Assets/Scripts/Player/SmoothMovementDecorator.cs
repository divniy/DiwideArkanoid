using UnityEngine;
using Zenject;

namespace Diwide.Arkanoid
{
    public class SmoothMovementDecorator : IPlayerMover
    {
        [Inject] private IPlayerMover _playerMover;
        [Inject] private float _smoothingSpeed;
        private Vector2 currentMove;
        private Vector2 smoothMoveVelocity;

        public void Move(Vector2 input)
        {
            currentMove = Vector2.SmoothDamp(currentMove, input, ref smoothMoveVelocity, _smoothingSpeed);
            _playerMover.Move(currentMove);
        }
    }
}