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
        // public PlayerFacade PlayerFacade;

        // [SerializeField] public GameObject PlayerFacade;
        

        private void OnTriggerEnter(Collider other)
        {
            // _signalBus.Fire(new MissedBallSignal(){BallFacade = other.GetComponent<BallFacade>()});
            // _signalBus.Fire(new MissedBallSignal(){ player = PlayerFacade });
            _signalBus.Fire<MissedBallSignal>();
        }
    }
}
