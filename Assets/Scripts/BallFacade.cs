using UnityEngine;
using Zenject;

namespace Diwide.Arkanoid
{
    public class BallFacade : IInitializable
    {
        [Inject] private BallMover _ballMover;
        [Inject] public Transform transform;
        [Inject] private MeshRenderer _renderer;
        private Color _originColor;
        private Color _transparentColor;

        public void Initialize()
        {
            _originColor = _renderer.material.color;
            _transparentColor = new Color(_originColor.r, _originColor.g, _originColor.b, .5f);
        }
        public void ResetToPlayer(PlayerFacade playerFacade)
        {
            _ballMover.StopMoving();
            transform.SetParent(playerFacade.transform, false);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            _renderer.material.color = _transparentColor;
        }

        public void Launch()
        {
            if(_ballMover.IsMoving) return;
            Debug.Log("Launch ball");
            transform.SetParent(null);
            _renderer.material.color = _originColor;
            _ballMover.StartMoving();
        }

        public class Factory : PlaceholderFactory<BallFacade> { }
    }
}