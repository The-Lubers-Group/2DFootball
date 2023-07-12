using System;
using UnityEngine;

namespace LubyLib.UIExtras.Fader
{
    public static class Fader 
    {
        private static bool _fading;
        private static bool _fadingIn;
        private static bool _fadingOut;

        private const float BaseFullFadeDuration = 1f;
        private const float BaseInOutRation = .6f;
        private const float BaseTimeInFullBlack = .05f;
        private static readonly Color BaseColor = Color.black;

        public static bool FadingIn => _fadingIn;
        public static bool FadingOut => _fadingOut;


        private static readonly string PrefabPath = "Luby/UIExtras/Fader/FaderBehaviour";
        
        #region InOut

        public static void FadeInOut(Action OnFadeStart = null, Action OnFadeMiddle = null, Action OnFadeEnded = null, Color color = default)
        {
            FadeInOut(BaseFullFadeDuration, BaseTimeInFullBlack, OnFadeStart, OnFadeMiddle, OnFadeEnded, color);
        }

        public static void FadeInOut(float duration, Action OnFadeStart = null, Action OnFadeMiddle = null, Action OnFadeEnded = null, Color color = default)
        {
            FadeInOut(duration, BaseTimeInFullBlack,  OnFadeStart, OnFadeMiddle, OnFadeEnded, color);
        }

        public static void FadeInOut(float duration, float timeInFullBlack, Action OnFadeStart = null, Action OnFadeMiddle = null, Action OnFadeEnded = null, Color color = default)
        {
            float fd = duration - timeInFullBlack;
            FadeInOut(fd*BaseInOutRation, fd*(1-BaseInOutRation), timeInFullBlack, OnFadeStart, OnFadeMiddle, OnFadeEnded, color);
        }
        
        public static void FadeInOut( float fadeInDuration,  float fadeOutDuration, float timeInFullBlack , Action OnFadeStart = null, Action OnFadeMiddle = null, Action OnFadeEnded = null, Color color = default)
        {
            if(_fading) return;

            if (color == default)
            {
                color = BaseColor;
            }

            FaderBehaviour behaviour = GameObject.Instantiate(Resources.Load<FaderBehaviour>(PrefabPath));

            Action StartAction = () =>
            {
                //_fadingIn = true;
                OnFadeStart?.Invoke();
            };
            Action MiddleAction = () =>
            {
                _fadingIn = false;
                _fadingOut = true;
                OnFadeMiddle?.Invoke();
            };
            Action EndAction = () =>
            {
                _fadingOut = false;
                _fading = false;
                OnFadeEnded?.Invoke();
            };
            behaviour.FullFade(fadeInDuration, fadeOutDuration, timeInFullBlack, StartAction, MiddleAction, EndAction, color);
        }

        #endregion

        #region In

        public static void FadeIn(Action OnFadeEnded = null, bool destroy = true, Color color = default)
        {
            FadeIn(BaseFullFadeDuration*BaseInOutRation, OnFadeEnded, destroy, color);
        }
        
        public static void FadeIn(float duration, Action OnFadeEnded = null, bool destroy = true, Color color = default)
        {
            if(_fading) return;
            
            if (color == default)
            {
                color = BaseColor;
            }

            FaderBehaviour behaviour = GameObject.Instantiate(Resources.Load<FaderBehaviour>(PrefabPath));

            _fading = true;
            _fadingIn = true;
            Action EndAction = () =>
            {
                _fadingIn = false;
                _fading = false;
                OnFadeEnded?.Invoke();
            };
            behaviour.FadeIn(duration, EndAction, destroy, color);
        }

        #endregion

        #region Out

        public static void FadeOut(Action OnFadeEnded = null, bool destroy = true, Color color = default)
        {
            FadeOut(BaseFullFadeDuration*(1-BaseInOutRation), OnFadeEnded, destroy, color);
        }
        
        public static void FadeOut(float duration, Action OnFadeEnded = null, bool destroy = true, Color color = default)
        {
            if(_fading) return;
            
            if (color == default)
            {
                color = BaseColor;
            }

            FaderBehaviour behaviour = GameObject.Instantiate(Resources.Load<FaderBehaviour>(PrefabPath));

            _fading = true;
            _fadingOut = true;
            Action EndAction = () =>
            {
                _fadingOut = false;
                _fading = false;
                OnFadeEnded?.Invoke();
            };
            behaviour.FadeOut(duration, EndAction, destroy, color);
        }

        #endregion
    }
}
