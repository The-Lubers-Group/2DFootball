using System;
using UnityEngine;
using UnityEngine.UI;

namespace LubyLib.Sound
{
    [RequireComponent(typeof(Toggle))]
    public class SoundToggle : MonoBehaviour
    {

        public SoundType soundType;
        private Toggle toggle;

        private void Awake()
        {
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(OnToggleChanged);
        }

        private void Start()
        {
            switch (soundType)
            {
                case SoundType.Music:
                    toggle.SetIsOnWithoutNotify(SoundManager.Instance.MusicOn);
                    SoundManager.Instance.HandleMusicSwitch += ValueUpdated;
                    break;
                case SoundType.SFX:
                    toggle.SetIsOnWithoutNotify(SoundManager.Instance.EffectOn);
                    SoundManager.Instance.HandleEffectSwitch += ValueUpdated;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ValueUpdated(bool isOn)
        {
            toggle.SetIsOnWithoutNotify(isOn);
        }
        
        public void OnToggleChanged(bool value)
        {
            switch (soundType)
            {
                case SoundType.Music:
                    SoundManager.SwitchMusic();
                    break;
                case SoundType.SFX:
                    SoundManager.SwitchEffect();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private void OnDestroy()
        {
            switch (soundType)
            {
                case SoundType.Music:
                    SoundManager.Instance.HandleMusicSwitch -= ValueUpdated;
                    break;
                case SoundType.SFX:
                    SoundManager.Instance.HandleEffectSwitch -= ValueUpdated;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        


    }
}