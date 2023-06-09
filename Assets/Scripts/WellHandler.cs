using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Diwide.Arkanoid
{
    public class WellHandler : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<BallMover>() != null)
            {
                // Debug.Log($"Well collide with {other.name}");
                _signalBus.Fire<MissedBallSignal>();
            }
        }
    }
}
