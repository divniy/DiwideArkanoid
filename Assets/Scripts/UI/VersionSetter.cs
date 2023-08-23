using System;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Diwide.Arkanoid.UI
{
    public class VersionSetter : MonoBehaviour
    {
        private void Start()
        {
            #if UNITY_EDITOR
                var textField = GetComponent<TMP_Text>();
                textField.text = $"ver. {PlayerSettings.bundleVersion}";
            #endif
        }
    }
}