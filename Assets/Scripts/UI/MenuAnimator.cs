using System;
using DG.Tweening;
using UnityEngine;

namespace Diwide.Arkanoid.UI
{
    public class MenuAnimator : MonoBehaviour
    {
        private void OnEnable()
        {
            transform.DOScale(Vector3.one, 3).From(Vector3.zero, true);
        }

        private void OnDisable()
        {
            transform.DOScale(Vector3.zero, 3).From(Vector3.one, true);
        }
    }
}