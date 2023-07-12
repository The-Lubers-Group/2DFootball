using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LubyLib.Core;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace LubyLib.Sound
{
    public class SoundManager : MonoBehaviour
    {
        public AudioSource musicSource, effectSource;
        private float musicVolume = 0;
        private float sfxVolume = 0;
        
        [SerializeField] private AudioMixer mixer;
        

        public bool MusicOn { get; private set; }
        public bool EffectOn { get; private set; }
        public static SoundManager Instance { get; private set; }

        public delegate void OnMusicSwitch(bool musicOn);

        public event OnMusicSwitch HandleMusicSwitch;

        public delegate void OnEffectSwitch(bool effectOn);

        public event OnEffectSwitch HandleEffectSwitch;

        [HideInInspector] public bool stopTemporaryMusic = false;

        private void Awake()
        {
	        // Singleton Pattern
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            LoadSettings();
        }

        /// <summary>
        /// Play an effect.
        /// </summary>
        /// <param name="clip">Clip to play.</param>
        /// <param name="duration">Duration, default 1.</param>
        /// <param name="pitch">Pitch, default 1.</param>
        public static void PlayEffect(AudioClip clip, float duration = 1f)
        {
            if (!Instance.EffectOn) return;

            //Instance.effectSource.pitch = pitch;
            Instance.effectSource.PlayOneShot(clip, duration);
        }

        /// <summary>
        /// Play a random effect.
        /// </summary>
        /// <param name="clips">Array of Clips to play.</param>
        /// <param name="duration">Duration, default 1.</param>
        /// <param name="lowPitchRange">Low Pitch, defalt 0.95</param>
        /// <param name="highPitchRange">High Pitch, defaltt 1.05</param>
        public static void PlayRandomEffect(AudioClip[] clips, float duration = 1f)
        {
            int random = Random.Range(0, clips.Length);
            PlayEffect(clips[random], duration);
        }
        

        /// <summary>
        /// Play a music.
        /// </summary>
        /// <param name="clip">Music to play.</param>
        public static void PlayMusic(AudioClip clip)
        {
            Instance.musicSource.clip = clip;
            if (!Instance.MusicOn) return;

            Instance.musicSource.Play();
        }

        public static void StopMusic()
        {
            Instance.musicSource.Stop();
        }

        public static void TemporaryMusic(AudioClip newMusic)
        {
            float oldMusicTime = Instance.musicSource.time;
            AudioClip oldMusicClip = Instance.musicSource.clip;
            PlayMusic(newMusic);
            Instance.musicSource.time = 0;
            Instance.StartCoroutine(Instance.TemporaryMusicEnumerator(oldMusicTime, newMusic.length, oldMusicClip));
        }

        public IEnumerator TemporaryMusicEnumerator(float oldMusicTime, float newMusicLength, AudioClip oldMusicClip)
        {
            while (musicSource.time != newMusicLength && !stopTemporaryMusic)
            {
                yield return null;
            }

            stopTemporaryMusic = false;
            StopMusic();
            PlayMusic(oldMusicClip);
            Instance.musicSource.time = oldMusicTime;
        }


        /// <summary>
        /// Switch music on or off. 
        /// </summary>
        /// <returns>Returns actual state.</returns>
        public static void SwitchMusic()
        {
            Instance.MusicOn = !Instance.MusicOn;

            if (!Instance.MusicOn)
            {
                Instance.musicSource.Stop();
                Instance.mixer.SetFloat("MusicVolume",-80);
            }
            else
            {
                Instance.musicSource.Play();
                Instance.mixer.SetFloat("MusicVolume", Instance.musicVolume);
            }

            if (Instance.HandleMusicSwitch != null) Instance.HandleMusicSwitch(Instance.MusicOn);
            
            
            Instance.SaveSettings();
        }

        /// <summary>
        /// Switch effects on or off.
        /// </summary>
        /// <returns>Returns actual state.</returns>
        public static void SwitchEffect()
        {
            Instance.EffectOn = !Instance.EffectOn;
            
            if (!Instance.EffectOn)
            {
                Instance.mixer.SetFloat("SFXVolume",-80);
            }
            else
            {
                Instance.mixer.SetFloat("SFXVolume", Instance.sfxVolume);
            }

            if (Instance.HandleEffectSwitch != null) Instance.HandleEffectSwitch(Instance.EffectOn);
            Instance.SaveSettings();
        }
        

        private void LoadSettings()
        {
            MusicOn = Utils.IntToBool(PlayerPrefs.GetInt("Music", 1));
            EffectOn = Utils.IntToBool(PlayerPrefs.GetInt("SFX", 1));
            

            if (MusicOn)
            {
                musicSource.Play();
            }

        }

        private void SaveSettings()
        {
            PlayerPrefs.SetInt("Music", Utils.BoolToInt(MusicOn));
            PlayerPrefs.SetInt("SFX", Utils.BoolToInt(EffectOn));
        }


        private void OnApplicationQuit()
        {
            SaveSettings();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus) SaveSettings();
        }
    }
}