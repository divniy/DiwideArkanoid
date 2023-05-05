using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Diwide.Arkanoid
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private PlayerInput _playerInput;
        [Inject] private IPlayerMover _mover;
        
        private Vector2 m_Move = Vector2.zero;

        private void Update()
        {
            _mover.Move(m_Move);
        }

        void OnMove(InputValue value)
        {
            m_Move = value.Get<Vector2>();
        }

        void OnLaunch()
        {
            _signalBus.Fire<LaunchBallSignal>();
        }
    }
}