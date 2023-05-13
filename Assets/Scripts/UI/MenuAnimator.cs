using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Diwide.Arkanoid.UI
{
    public class MenuAnimator : MonoBehaviour
    {
        [Inject] private MenuManager _menuManager;
        [SerializeField] private bool _animatedOnStart = false;
        private float _duration = 1f;
        public Selectable[] _interactables;

        private void Start()
        {
            _interactables = GetComponentsInChildren<Selectable>();
            if(_animatedOnStart) Open();
        }

        public void Open()
        {
            Tween tween = transform.DOScale(Vector3.one, _duration).From(Vector3.zero);
            InteractionsWrapper(tween);
            _menuManager.OpenMenu(tween);
        }

        public void Close()
        {
            Tween tween = transform.DOScale(Vector3.zero, _duration).From(Vector3.one);
            InteractionsWrapper(tween);
            _menuManager.CloseMenu(tween);
        }

        private void InteractionsWrapper(Tween tween)
        {
            if (_interactables.Any())
            {
                tween
                    .OnStart(() => SetInteractionsAvailability(false))
                    .OnComplete(() => SetInteractionsAvailability(true));
            }
        }

        private void SetInteractionsAvailability(bool value)
        {
            foreach (var control in _interactables)
            {
                control.interactable = value;
            }
        }
    }
}