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
        [Inject] private GameObject[] _playerSpawns;
        [Inject] private PlayerFacade.Factory _playerFactory;
        [Inject] private BallFacade.Factory _ballFactory;
        [Inject] private SignalBus _signalBus;
        private List<PlayerFacade> _playerFacades;
        private BallFacade _ballFacade;
        
        public void Initialize()
        {
            PlayerFacade p1 = _playerFactory.Create(_playerSpawns[0], "Player 1");
            PlayerFacade p2 = _playerFactory.Create(_playerSpawns[1], "Player 2");
            _playerFacades = new() { p1, p2 };

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