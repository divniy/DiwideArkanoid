using UnityEngine;
using Zenject;
using Vector2 = System.Numerics.Vector2;

namespace Diwide.Arkanoid
{
    public class ObstacleView : MonoBehaviour
    {
        // [Inject] private BallFacade _ballFacade;
        [Inject] private SignalBus _signalBus;
        [Inject] private LevelManager _levelManager;
        
        void OnCollisionEnter(Collision other)
        {
            // Debug.Log("ObstacleView OnCollisionEnter");
            _signalBus.Fire<CollideObstacleSignal>();
            _levelManager.RemoveObstacle(this);
        }
        
        public class Pool : MonoMemoryPool<Vector3, ObstacleView>
        {
            protected override void Reinitialize(Vector3 p1, ObstacleView item)
            {
                item.transform.position = p1;
            }
        }
    }
}