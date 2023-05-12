using System;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Diwide.Arkanoid
{
    public class BallMover : MonoBehaviour
    {
        [Inject] private Settings _settings;
        // [Inject] private Rigidbody _rigidbody;
        [SerializeField, Range(5, 40)] private float _speed;
        [field:SerializeField] public bool IsMoving { get; private set; }

        public void StartMoving()
        {
            ResetSpeed();
            IsMoving = true;
        }

        public void StopMoving()
        {
            IsMoving = false;
        }

        private void FixedUpdate()
        {
            if(IsMoving) _move();
        }

        private void _move()
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
            // _rigidbody.velocity = transform.forward * _speed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var normal = collision.GetContact(0).normal;
            var reflection = Vector3.Reflect(transform.forward, normal);

            Debug.Log($"Forward: {transform.forward}, Normal: {normal}, Reflect: {reflection}");
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, reflection);
        }

        public void ResetSpeed()
        {
            _speed = _settings.launchSpeed;
        }
        public void IncreaseSpeed()
        {
            Debug.Log("Ball speed increased");
            _speed = Mathf.Clamp(_speed + _settings.speedIncrement,_settings.launchSpeed, _settings.maxSpeed);
        }

        // private void OnDrawGizmos()
        // {
            // Gizmos.color = Color.red;
            // Gizmos.DrawLine(transform.position, transform.forward);
        // }
        [Serializable]
        public class Settings
        {
            public float launchSpeed;
            public float maxSpeed;
            public float speedIncrement;
        }
    }
}