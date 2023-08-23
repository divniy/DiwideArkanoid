using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Diwide.Arkanoid.UI
{
    public class MenuManager : IInitializable
    {
        [Inject] private List<MenuAnimator> _menuAnimators;
        private Sequence _sequence;

        public Sequence Sequence
        {
            get {
                if (_sequence == null || !_sequence.IsActive())
                {
                    _sequence = DOTween.Sequence();
                }
                return _sequence;
            }
        }

        public void Initialize()
        {
            Debug.Log("Game started");
            foreach (var hidden in _menuAnimators.FindAll(_=> !_.isActiveAndEnabled))
            {
                hidden.transform.localScale = Vector3.zero;
                hidden.gameObject.SetActive(true);
            }
        }

        public void OpenMenu(Tween tween)
        {
            Sequence.Append(tween);
        }

        public void CloseMenu(Tween tween)
        {
            Sequence.Prepend(tween);
        }
    }
}
