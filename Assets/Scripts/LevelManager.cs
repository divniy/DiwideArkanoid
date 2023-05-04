using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Diwide.Arkanoid
{
    public class LevelManager : IInitializable
    {
        [Inject] private LevelProperties[] _levels;
        [Inject] private ObstacleView.Pool _pool;
        private int _currentLevelIndex;
        [Inject] private List<ObstacleView> _obstacleViews;
        [Inject] private SignalBus _signalBus;

        public void Initialize()
        {
            InitLevel(0);
        }
        
        public void InitLevel(int index)
        {
            if(_levels.Length == 0) return;
            if (_levels[index] == null) throw new ApplicationException("Error init unexistent level");
            Debug.Log($"Init {index} level");
            foreach (Vector3 position in _levels[index].obstaclePositions)
            {
                SpawnObstacle(position);
            }
            _currentLevelIndex = index;
        }

        public void SpawnObstacle(Vector3 position)
        {
            AddObstacle(_pool.Spawn(position));
        }

        private void AddObstacle(ObstacleView obstacleView)
        {
            _obstacleViews.Add(obstacleView);
        }

        public void RemoveObstacle(ObstacleView obstacleView)
        {
            _pool.Despawn(obstacleView);
            _obstacleViews.Remove(obstacleView);
            if (_obstacleViews.Where(_=>_.isActiveAndEnabled).IsEmpty())
            {
                NothingObstaclesLeft();
            }
        }

        private void NothingObstaclesLeft()
        {
            Debug.Log($"Level {_currentLevelIndex} complete");
            if (_currentLevelIndex + 1 < _levels.Length)
            {
                _signalBus.Fire(new LevelCompleteSignal() { index = _currentLevelIndex });
            }
            else
            {
                _signalBus.Fire<GameCompleteSignal>();
            }
        }
    }
}