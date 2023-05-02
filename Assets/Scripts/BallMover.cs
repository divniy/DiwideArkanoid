using System;
using UnityEditor;
using UnityEngine;

namespace Diwide.Arkanoid
{
    public class BallMover : MonoBehaviour
    {
        private float _speed;
        [SerializeField] private float _launchSpeed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _speedIncrement;
        [HideInInspector] public bool IsMoving = false;

        private void Start()
        {
            _speed = _launchSpeed;
        }

        private void Update()
        {
            if(!IsMoving) return;
            transform.position += transform.forward * _speed * Time.deltaTime;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var normal = collision.GetContact(0).normal;
            var reflection = Vector3.Reflect(transform.forward, normal);
            Debug.Log($"Forward: {transform.forward}, Normal: {normal}, Reflect: {reflection}");
            // transform.rotation = Quaternion.FromToRotation(transform.forward, reflection);
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, reflection);
            if (collision.gameObject.CompareTag("Breakable"))
            {
                IncreaseSpeed();
                Destroy(collision.gameObject);
            }
        }

        public void IncreaseSpeed()
        {
            _speed = Mathf.Clamp(_speed + _speedIncrement, _launchSpeed, _maxSpeed);
        }

        // private void OnDrawGizmos()
        // {
            // Gizmos.color = Color.red;
            // Gizmos.DrawLine(transform.position, transform.forward);
        // }
    }
}