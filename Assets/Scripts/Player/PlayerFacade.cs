using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Diwide.Arkanoid
{
    public class PlayerFacade : IInitializable
    {
        [Inject] private PlayerSpawn _spawnPoint;

        [Inject]
        public Transform transform
        {
            get; private set;
        }

        public void Initialize()
        {
            transform.SetPositionAndRotation(_spawnPoint.transform.position, _spawnPoint.transform.rotation);
        }
        
        public class Factory : PlaceholderFactory<PlayerSpawn, PlayerFacade> { }
    }
}