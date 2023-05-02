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
            // _signalBus.Fire(new MissedBallSignal(){BallFacade = other.GetComponent<BallFacade>()});
            _signalBus.Fire<MissedBallSignal>();
        }
    }
}
