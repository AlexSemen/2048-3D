using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Yandex;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        private const string Group = "Value";

        private readonly float _turnedOn = 0;
        private readonly float _turnedOff = -80;

        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        [SerializeField] private CheckingFocus _ñheckingFocus;
        [SerializeField] private VideoAdd _videoAdd;
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _spriteOn;
        [SerializeField] private Sprite _spriteOff;

        private bool _isTurnedOn = true;

        public bool IsTurnedOn => _isTurnedOn;

        private void Awake()
        {
            SetFloatValue(_isTurnedOn);
        }

        private void OnEnable()
        {
            _ñheckingFocus.ChangeFocus += SetWork;
            _videoAdd.ChangeAudio += SetWork;
        }

        private void OnDisable()
        {
            _ñheckingFocus.ChangeFocus -= SetWork;
            _videoAdd.ChangeAudio -= SetWork;
        }

        public void OnClickButton()
        {
            _isTurnedOn = !_isTurnedOn;

            SetFloatValue(_isTurnedOn);
        }

        public void SetIsTurnedOn(bool isTurnedOn)
        {
            _isTurnedOn = isTurnedOn;

            SetFloatValue(_isTurnedOn);
        }

        private void SetWork(bool work)
        {
            SetFloatValue(_isTurnedOn && work);
        }

        private void SetFloatValue(bool turnOn)
        {
            if (turnOn)
            {
                _audioMixerGroup.audioMixer.SetFloat(Group, _turnedOn);
                _image.sprite = _spriteOn;
            }
            else
            {
                _audioMixerGroup.audioMixer.SetFloat(Group, _turnedOff);
                _image.sprite = _spriteOff;
            }
        }
    }
}
