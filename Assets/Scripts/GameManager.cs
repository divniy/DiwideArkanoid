using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Diwide.Arkanoid
{
    public class GameManager : IInitializable
    {
        [Inject] private Settings _settings;
        [Inject] private PlayerSpawn[] _playerSpawns;
        [Inject] private PlayerFacade.Factory _playerFactory;
        [Inject] private BallFacade.Factory _ballFactory;
        [Inject] private LevelManager _levelManager;
        private List<PlayerFacade> _playerFacades = new();
        private BallFacade _ballFacade;
        private int _lifesCounter;
        
        public void Initialize()
        {
            _lifesCounter = _settings.PlayerLifesCount;
            _playerFacades.Add(_playerFactory.Create(_playerSpawns[0]));
            _playerFacades.Add(_playerFactory.Create(_playerSpawns[1]));

            _ballFacade = _ballFactory.Create();
            _ballFacade.ResetToPlayer(_playerFacades.First());
            
            _levelManager.InitLevel(0);
        }

        public void OnBallMissing()
        {
            if (_lifesCounter > 1)
            {
                _lifesCounter--;
                Debug.Log($"Ball was passed away. You only have {_lifesCounter} tries to win.");
                ResetBallToClosestPlayer();
            }
            else
            {
                GameOver();
            }
        }

        public void OnLevelComplete(int levelIndex)
        {
            ResetBallToClosestPlayer();
            _levelManager.InitLevel(levelIndex + 1);
        }

        public void GameComplete()
        {
            Debug.Log("Game is over. Unsuccessfully - you win it.");
            EditorApplication.isPaused = true;
        }

        private void GameOver()
        {
            Debug.Log("Game is over. After all - you lose. As expected, so.");
            EditorApplication.isPaused = true;
        }

        private void ResetBallToClosestPlayer()
        {
            var player = _playerFacades
                .OrderBy(_ => (_.transform.position - _ballFacade.transform.position).sqrMagnitude).First();
            _ballFacade.ResetToPlayer(player);
        }
        
        [Serializable]
        public class Settings
        {
            public int PlayerLifesCount;
        }
    }
}