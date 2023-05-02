using System;
using UnityEditor;
using UnityEngine;

namespace Diwide.Arkanoid
{
    public class BallMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        public bool IsMoving = false;

        private void Update()
        {
            if(!IsMoving) return;
            transform.position += transform.forward * _speed * Time.deltaTime;
        }

        private void OnCollisionEnter(Collision collision)
        {
            // IsMoving = false;
            var normal = collision.GetContact(0).normal;
            var reflection = Vector3.Reflect(transform.forward, normal);
            Debug.Log(transform.forward);
            Debug.Log(normal);
            Debug.Log(reflection);
            transform.rotation = Quaternion.FromToRotation(transform.forward, reflection);
            // EditorApplication.isPaused = true;
            // transform.LookAt(reflection);
        }

        // private void OnDrawGizmos()
        // {
            // Gizmos.color = Color.red;
            // Gizmos.DrawLine(transform.position, transform.forward);
        // }
    }
}