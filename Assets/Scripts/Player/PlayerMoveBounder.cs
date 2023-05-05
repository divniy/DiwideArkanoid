using UnityEngine;
using Zenject;

namespace Diwide.Arkanoid
{
    public class PlayerMoveBounder : MonoBehaviour
    {
        [Inject] private PlayerSpawn _playerSpawn;
        [SerializeField] private MeshRenderer _wall; 
        private Bounds _moveBounds;

        private void Start()
        {
            _moveBounds = _playerSpawn.GetMoveBounds(_wall.bounds);
        }

        private void LateUpdate()
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(transform.position.x, _moveBounds.min.x, _moveBounds.max.x);
            pos.y = Mathf.Clamp(transform.position.y, _moveBounds.min.x, _moveBounds.max.x);
            transform.position = pos;
        }
    }
}