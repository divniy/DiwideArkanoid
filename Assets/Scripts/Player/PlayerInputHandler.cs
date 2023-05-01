using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Diwide.Arkanoid
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 10;
        [SerializeField] private float _smoothMoveSpeed = .3f;
        
        private Vector2 m_Move = Vector2.zero;
        private Vector2 currentMove;
        private Vector2 smoothMoveVelocity;
        
        void OnMove(InputValue value)
        {
            m_Move = value.Get<Vector2>();
        }
        
        // public void OnMove(InputAction.CallbackContext context)
        // {
            // m_Move = context.ReadValue<Vector2>();
        // }

        void OnStart()
        {
            Debug.Log("Start button pressed");
        }

        private void Update()
        {
            Move(m_Move);
        }

        private void Move(Vector2 input)
        {
            // if (input.sqrMagnitude < 0.01)
                // return;
            currentMove = Vector2.SmoothDamp(currentMove, input, ref smoothMoveVelocity, _smoothMoveSpeed);
            var scaledMoveSpeed = _moveSpeed * Time.deltaTime;
            // For simplicity's sake, we just keep movement in a single plane here. Rotate
            // direction according to world Y rotation of player.
            // var move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
            var move = new Vector3(currentMove.x, currentMove.y,0);
            transform.position += move * scaledMoveSpeed;
        }
    }
}