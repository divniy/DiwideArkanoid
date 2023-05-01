using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Diwide.Arkanoid
{
    public class GameInitializer : IInitializable
    {
        [Inject] private Transform[] _playerSpawns;
        [Inject] private PlayerFacade.Factory _playerFactory;
        
        public void Initialize()
        {
            PlayerFacade p1 = _playerFactory.Create();
            p1.PlayerInput.SwitchCurrentControlScheme("Player 1", Keyboard.current);
            p1.Transform.position = _playerSpawns[0].position;
            p1.Transform.rotation = _playerSpawns[0].rotation;
            // Debug.Log(player1);
            // Debug.Log(player2);
            
            PlayerFacade p2 = _playerFactory.Create();
            p2.PlayerInput.SwitchCurrentControlScheme("Player 2", Keyboard.current);
            p2.Transform.position = _playerSpawns[1].position;
            p2.Transform.rotation = _playerSpawns[1].rotation;
        }
    }
}