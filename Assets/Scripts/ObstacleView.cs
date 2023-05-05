using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;
using Vector2 = System.Numerics.Vector2;

namespace Diwide.Arkanoid
{
    public class ObstacleView : MonoBehaviour
    {
        // [Inject] private BallFacade _ballFacade;
        [Inject] private SignalBus _signalBus;
        [Inject] private LevelManager _levelManager;
        [SerializeField] private MeshRenderer _renderer;
        
        
        void OnCollisionEnter(Collision other)
        {
            // Debug.Log("ObstacleView OnCollisionEnter");
            _signalBus.Fire<CollideObstacleSignal>();
            _levelManager.RemoveObstacle(this);
        }

        void Reset(Vector3 position)
        {
            transform.SetPositionAndRotation(position, Random.rotation);
            _renderer.material.color = Random.ColorHSV();
        }

        public class Pool : MonoMemoryPool<Vector3, ObstacleView>
        {
            protected override void Reinitialize(Vector3 position, ObstacleView item)
            {
                item.Reset(position);
            }
        }
    }
}