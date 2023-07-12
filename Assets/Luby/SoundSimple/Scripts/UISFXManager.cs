using System;
using System.Collections;
using System.Collections.Generic;
using LubyLib.Core;
using UnityEngine;

namespace LubyLib.Sound
{
    public class UISFXManager : MonoBehaviour
    {
        private static UISFXManager _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            _instance = this;
            
            DontDestroyOnLoad(gameObject);

            _uiEffects = new Dictionary<UISFXType, UISFX>();
            for (int i = 0; i < uiEffectsList.Count; i++)
            {
                _uiEffects.Add(uiEffectsList[i].type, uiEffectsList[i]);
            }
        }

        [SerializeField] private List<UISFX> uiEffectsList;

        private Dictionary<UISFXType, UISFX> _uiEffects;

        public static void PlayEffect(UISFXType type)
        {
            SoundManager.PlayEffect(_instance._uiEffects[type].GetAudioClip());
        }

        public static void PlayCustomUIEffect(AudioClip clip)
        {
            SoundManager.PlayEffect(clip);
        }
        
        [System.Serializable]
        public class UISFX
        {
            public UISFXType type;
            public AudioClip[] clips;

            public AudioClip GetAudioClip()
            {
                if (clips.Length > 1)
                    return RandomUtils.FromArray(clips);
                else
                    return clips[0];

            }
        }
    }
}
