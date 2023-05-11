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
        [Inject] private readonly GameModel _gameModel;
        private List<PlayerFacade> _playerFacades = new();
        private BallFacade _ballFacade;

        public void Initialize()
        {
            _playerFacades.Add(_playerFactory.Create(_playerSpawns[0]));
            _playerFacades.Add(_playerFactory.Create(_playerSpawns[1]));
            _gameModel.LifesCounter = _settings.PlayerLifesCount;

            _ballFacade = _ballFactory.Create();
            _ballFacade.ResetToPlayer(_playerFacades.First());
            
            _levelManager.InitLevel(0);
        }

        public void OnBallMissing()
        {
            _gameModel.LifesCounter--;
            if (_gameModel.LifesCounter > 0)
            {
                Debug.Log($"Ball was passed away. You only have {_gameModel.LifesCounter} tries to win.");
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

        public void SetPause(bool active)
        {
            _gameModel.SetPause(active);
            Time.timeScale = active ? 0 : 1;
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