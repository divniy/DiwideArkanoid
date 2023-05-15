using TMPro;
using UnityEngine;

namespace Diwide.Arkanoid.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textField;

        public void OnLifesCountChange(int newValue)
        {
            if(_textField == null) return;
            _textField.text = newValue.ToString();
        }
    }
}