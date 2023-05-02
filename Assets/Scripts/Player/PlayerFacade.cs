using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Diwide.Arkanoid
{
    public class PlayerFacade
    {
        public PlayerInput PlayerInput;
        
        public PlayerFacade(PlayerInput playerInput)
        {
            PlayerInput = playerInput;
        }

        [Inject]
        public Transform transform
        {
            get; private set;
        }
        
        public class Factory : PlaceholderFactory<PlayerFacade> { }
    }
}