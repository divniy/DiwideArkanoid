using UnityEngine;

namespace Diwide.Arkanoid
{
    [CreateAssetMenu(fileName = "Level", menuName = "Levels/LevelProperties", order = 0)]
    public class LevelProperties : ScriptableObject
    {
        public Vector3[] obstaclePositions = {};
    }
}