using UnityEngine;
using Zenject;


namespace Diwide.Arkanoid
{
    public interface IPlayerMover
    {
        void Move(Vector2 input);
    }
    
    public class PlayerController : IPlayerMover
    {
        [Inject] private Transform _transform;
        [Inject] private float _moveSpeed;
        
        public void Move(Vector2 input)
        {
            var scaledMoveSpeed = _moveSpeed * Time.deltaTime;
            var moveVector = _transform.TransformDirection(new Vector3(input.x, input.y,0));
            _transform.position += moveVector * scaledMoveSpeed;
        }
    }
}