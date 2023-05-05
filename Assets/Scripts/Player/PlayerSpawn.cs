using System;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Diwide.Arkanoid
{
    public class PlayerSpawn : MonoBehaviour
    {
        [SerializeField] public Vector3 boundsSize;

        public Bounds GetMoveBounds(Bounds wall)
        {
            return new Bounds(wall.center, boundsSize - wall.size);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, boundsSize);
        }
    }

    // [CustomEditor(typeof(PlayerSpawn))]
    // public class PlayerSpawnEditor : Editor
    // {
    //     public void OnSceneGUI()
    //     {
    //         var t = target as PlayerSpawn;
    //         Handles.color = Color.green;
    //         Handles.DrawWireCube(t.transform.position, t.boundsSize);
    //     }
    // }
}