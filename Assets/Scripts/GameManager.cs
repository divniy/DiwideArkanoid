using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Diwide.Arkanoid
{
    public class GameManager : IInitializable, IDisposable
    {
        [Inject] private Transform[] _playerSpawns;
        // [Inject] private WellHandler[] _wellHandlers;
        // [Inject] private Transform[] _playerSpawns;
        [Inject] private PlayerFacade.Factory _playerFactory;
        [Inject] private BallFacade.Factory _ballFactory;
        [Inject] private SignalBus _signalBus;
        // private PlayerFacade[] _playerFacades;
        private List<PlayerFacade> _playerFacades = new();
        private BallFacade _ballFacade;
        // public PlayerFacade[] Players => _playerFacades;
        
        public void Initialize()
        {
            PlayerFacade p1 = _playerFactory.Create();
            p1.PlayerInput.SwitchCurrentControlScheme("Player 1", Keyboard.current);
            p1.transform.SetPositionAndRotation(_playerSpawns[0].position, _playerSpawns[0].rotation);
            _playerFacades.Add(p1);
            // _playerFacades.SetValue(p1, 0);
            // _wellHandlers[0].PlayerFacade = p1;

            _ballFacade = _ballFactory.Create();
            _ballFacade.ResetToPlayer(p1);

            _signalBus.Subscribe<MissedBallSignal>(ResetBallToClosestPlayer);
        }


        public void Dispose()
        {
            _signalBus.Unsubscribe<MissedBallSignal>(ResetBallToClosestPlayer);
        }

        public void ResetBallToClosestPlayer()
        {
            Dictionary<float, PlayerFacade> distances = new();
            
            foreach (var playerFacade in _playerFacades)
            {
                var playerBallDistance =
                    (playerFacade.transform.position - _ballFacade.transform.position).sqrMagnitude;
                    // Vector3.SqrMagnitude(playerFacade.transform.position - _ballFacade.transform.position);
                distances.Add(playerBallDistance, playerFacade);
            }
            var player = distances.OrderBy(_ => _.Key).First().Value;
            _ballFacade.ResetToPlayer(player);

        }
    }
}