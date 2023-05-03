using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Diwide.Arkanoid
{
    public class PlayerFacade : IInitializable
    {
        [Inject] private GameObject _spawnPoint;

        [Inject]
        public Transform transform
        {
            get; private set;
        }

        public void Initialize()
        {
            transform.SetPositionAndRotation(_spawnPoint.transform.position, _spawnPoint.transform.rotation);
        }
        
        public class Factory : PlaceholderFactory<GameObject, PlayerFacade> { }
    }
}