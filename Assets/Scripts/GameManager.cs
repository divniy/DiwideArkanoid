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
        private List<PlayerFacade> _playerFacades = new();
        private BallFacade _ballFacade;
        
        public void Initialize()
        {
            _playerFacades.Add(_playerFactory.Create(_playerSpawns[0]));
            _playerFacades.Add(_playerFactory.Create(_playerSpawns[1]));

            _ballFacade = _ballFactory.Create();
            _ballFacade.ResetToPlayer(_playerFacades.First());

            _signalBus.Subscribe<MissedBallSignal>(ResetBallToClosestPlayer);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<MissedBallSignal>(ResetBallToClosestPlayer);
        }

        public void ResetBallToClosestPlayer()
        {
            var player = _playerFacades
                .OrderBy(_ => (_.transform.position - _ballFacade.transform.position).sqrMagnitude).First();
            _ballFacade.ResetToPlayer(player);
        }
    }
}