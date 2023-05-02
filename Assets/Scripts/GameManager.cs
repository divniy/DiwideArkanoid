using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Diwide.Arkanoid
{
    public class GameManager : IInitializable, IDisposable
    {
        [Inject] private Transform[] _playerSpawns;
        [Inject] private PlayerFacade.Factory _playerFactory;
        [Inject] private BallFacade.Factory _ballFactory;
        [Inject] private SignalBus _signalBus;
        private PlayerFacade[] _playerFacades = new PlayerFacade[2];
        private BallFacade _ballFacade;
        // public PlayerFacade[] Players => _playerFacades;
        
        public void Initialize()
        {
            PlayerFacade p1 = _playerFactory.Create();
            p1.PlayerInput.SwitchCurrentControlScheme("Player 1", Keyboard.current);
            p1.transform.SetPositionAndRotation(_playerSpawns[0].position, _playerSpawns[0].rotation);

            _ballFacade = _ballFactory.Create();
            _ballFacade.ResetToPlayer(p1);
            _playerFacades.SetValue(p1, 0);
            
            _signalBus.Subscribe<MissedBallSignal>(ResetBallToClosestPlayer);
        }


        public void Dispose()
        {
            _signalBus.Unsubscribe<MissedBallSignal>(ResetBallToClosestPlayer);
        }

        public void ResetBallToClosestPlayer()
        {
            
            
            
        }
    }
}