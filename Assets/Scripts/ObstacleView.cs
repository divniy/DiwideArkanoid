using UnityEngine;
using Zenject;

namespace Diwide.Arkanoid
{
    public class ObstacleView : MonoBehaviour
    {
        // [Inject] private BallFacade _ballFacade;
        [Inject] private SignalBus _signalBus;
        void OnCollisionEnter(Collision other)
        {
            // Debug.Log("ObstacleView OnCollisionEnter");
            _signalBus.Fire<CollideObstacleSignal>();
            Destroy(gameObject);
        }
    }
}