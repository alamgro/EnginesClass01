using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace UI
{
    public class SoundMenuUI : MonoBehaviour
    {
        [SerializeField]
        private UIDocument _menuUIDocument;
        [SerializeField]
        private InputActionReference _openMenuInputReference;
        [SerializeField]
        private AudioMixer _audioMixer;
        [SerializeField]
        private float _minimumDbVolume;
        [SerializeField]
        private float _maximumDbVolume;
        [SerializeField]
        private UnityEvent _onOpenedSoundMenu;
        [SerializeField]
        private UnityEvent _onClosedSoundMenu;

        private Slider _sliderMaster;
        private Slider _sliderMusic;
        private Slider _sliderSFX;

        private bool _isMenuOpen = false;

        private void Awake()
        {
            _sliderMaster = _menuUIDocument.rootVisualElement.Q<Slider>("SliderMaster");
            _sliderMusic = _menuUIDocument.rootVisualElement.Q<Slider>("SliderMusic");
            _sliderSFX = _menuUIDocument.rootVisualElement.Q<Slider>("SliderSFX");

            _sliderMaster.RegisterValueChangedCallback(OnSliderValueChanged);
            _sliderMusic.RegisterValueChangedCallback(OnSliderValueChanged);
            _sliderSFX.RegisterValueChangedCallback(OnSliderValueChanged);
        }

        private void Start()
        {
            InitializeMixerVolumes();
        }

        private void OnEnable()
        {
            _openMenuInputReference.action.performed += ToggleMenu;
            _openMenuInputReference.action.Enable();
        }

        private void OnDisable()
        {
            _openMenuInputReference.action.performed -= ToggleMenu;
            _openMenuInputReference.action.Disable();
        }

        private void ToggleMenu(InputAction.CallbackContext callbackContext)
        {
            _isMenuOpen = !_isMenuOpen;
            _menuUIDocument.enabled = _isMenuOpen;

            if (_isMenuOpen) 
                _onOpenedSoundMenu?.Invoke();
            else 
                _onClosedSoundMenu?.Invoke();
        }

        private void OnSliderValueChanged(ChangeEvent<float> changeEvent)
        {
            if(changeEvent.target == _sliderMaster)
            {
                Debug.Log(GetDecibelsValue(_sliderMaster.value));
                _audioMixer.SetFloat("MasterVolume", GetDecibelsValue(_sliderMaster.value));
            }
            else if(changeEvent.target == _sliderMusic)
            {
                Debug.Log($"Music: {_sliderMusic.value}");
                _audioMixer.SetFloat("MusicVolume", GetDecibelsValue(_sliderMusic.value));
            }
            else if(changeEvent.target == _sliderSFX)
            {
                Debug.Log($"Music: {_sliderSFX.value}");
                _audioMixer.SetFloat("SFXVolume", GetDecibelsValue(_sliderMusic.value));
            }
        }

        private void InitializeMixerVolumes()
        {
            _audioMixer.SetFloat("MasterVolume", GetDecibelsValue(_sliderMaster.value));
            _audioMixer.SetFloat("MusicVolume", GetDecibelsValue(_sliderMusic.value));
            _audioMixer.SetFloat("SFXVolume", GetDecibelsValue(_sliderMusic.value));
        }

        private float GetDecibelsValue(float inputValue)
        {
            float outputValue = Mathf.Lerp(_minimumDbVolume, _maximumDbVolume, inputValue / 100f);

            // If the output is small enough mutes the audio group
            if (outputValue == _minimumDbVolume) return -100f;

            return outputValue;
        }
    } 
}