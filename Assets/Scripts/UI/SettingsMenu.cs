using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Diwide.Arkanoid.UI
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private Toggle _isVolumeOnToggle;
        [SerializeField] private Slider _volumeSlider;
        [SerializeField] private Toggle _easyToggle;
        [SerializeField] private Toggle _mediumToggle;
        [SerializeField] private Toggle _hardToggle;
        private Dictionary<Difficulty, Toggle> _difficultyTogglesDictionary;

        private bool _isVolumeOn = true;
        private int _volume = 100;
        private Difficulty _difficulty = Difficulty.Medium;
        
        private void Awake()
        {
            if(PlayerPrefs.HasKey("VolumeOn"))
                _isVolumeOn = PlayerPrefs.GetInt("VolumeOn") == 1 ? true : false;
            if (PlayerPrefs.HasKey("Volume"))
                _volume = PlayerPrefs.GetInt("Volume");
            if (PlayerPrefs.HasKey("Difficulty"))
                _difficulty = (Difficulty)PlayerPrefs.GetInt("Difficulty");
        }

        private void Start()
        {
            InitDifficultyDictionary();
            _isVolumeOnToggle.isOn = _isVolumeOn;
            _volumeSlider.value = _volume;
            _difficultyTogglesDictionary[_difficulty].isOn = true;
        }
        private void OnEnable()
        {
            _isVolumeOnToggle.onValueChanged.AddListener(OnVolumeEnabled);
            _volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
        public void OnVolumeEnabled(bool value) => _isVolumeOn = value;
        public void OnVolumeChanged(float value) => _volume = (int) value;

        private void OnDisable()
        {
            _isVolumeOnToggle.onValueChanged.RemoveListener(OnVolumeEnabled);
            _volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
            _difficulty = _difficultyTogglesDictionary.FirstOrDefault(_ => _.Value.isOn).Key;
            SavePlayerPrefs();
        }

        private void SavePlayerPrefs()
        {
            PlayerPrefs.SetInt("VolumeOn", _isVolumeOn ? 1 : 0);
            PlayerPrefs.SetInt("Volume", _volume);
            PlayerPrefs.SetInt("Difficulty", (int)_difficulty);
        }

        private void InitDifficultyDictionary()
        {
            _difficultyTogglesDictionary = new()
            {
                { Difficulty.Easy, _easyToggle },
                { Difficulty.Medium, _mediumToggle },
                { Difficulty.Hard, _hardToggle }
            };
        }
    }

    public enum Difficulty
    {
        Easy = 0,
        Medium = 1,
        Hard = 2
    }
}