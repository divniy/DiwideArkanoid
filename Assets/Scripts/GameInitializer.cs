using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Diwide.Arkanoid
{
    public class GameInitializer : IInitializable
    {
        [Inject] private Transform[] _playerSpawns;
        [Inject] private PlayerFacade.Factory _playerFactory;
        [Inject] private BallFacade.Factory _ballFactory;
        
        public void Initialize()
        {
            PlayerFacade p1 = _playerFactory.Create();
            p1.PlayerInput.SwitchCurrentControlScheme("Player 1", Keyboard.current);
            p1.transform.SetPositionAndRotation(_playerSpawns[0].position, _playerSpawns[0].rotation);

            var ball = _ballFactory.Create();
            ball.ResetToPlayer(p1);
            // PlayerFacade p2 = _playerFactory.Create();
            // p2.PlayerInput.SwitchCurrentControlScheme("Player 2", Keyboard.current);
            // p2.transform.SetPositionAndRotation(_playerSpawns[1].position, _playerSpawns[1].rotation);
        }
    }
}