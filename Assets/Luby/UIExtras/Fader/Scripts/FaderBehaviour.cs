using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LubyLib.UIExtras.Fader
{
    public class FaderBehaviour : MonoBehaviour
    {

        [SerializeField] private Image _image;
        
        
        public void FullFade(float fadeInDuration, float fadeOutDuration, float timeInFullBlack, Action OnFadeStart, Action OnFadeMiddle, Action OnFadeEnded, Color color)
        {
            _image.color = color;
            OnFadeStart?.Invoke();

            StartCoroutine(FullFadeFadeCoroutine(fadeInDuration, fadeOutDuration, timeInFullBlack, OnFadeMiddle,
                OnFadeEnded));
            
        }

        public void FadeIn(float fadeInDuration, Action OnFadeEnded, bool destroy,  Color color)
        {
            _image.color = color;
            if(destroy)
            {
                StartCoroutine(Fade(fadeInDuration, 0, 1, () =>
                {
                    OnFadeEnded?.Invoke();
                    Destroy(gameObject);
                }));
            }
            else
            {
                StartCoroutine(Fade(fadeInDuration, 0, 1, OnFadeEnded));
            }
        }
        
        public void FadeOut(float fadeOutDuration, Action OnFadeEnded, bool destroy,  Color color)
        {
            _image.color = color;
            if(destroy)
            {
                StartCoroutine(Fade(fadeOutDuration, 1, 0, () =>
                {
                    OnFadeEnded?.Invoke();
                    Destroy(gameObject);
                }));
            }
            else
            {
                StartCoroutine(Fade(fadeOutDuration, 1, 0, OnFadeEnded));
            }
        }

        private IEnumerator Fade(float duration, float startValue,  float finalValue, Action OnComplete)
        {
            Color color = _image.color;
            color.a = startValue;
            _image.color = color;
            float perSecond = (finalValue - startValue) / duration;
            float timeElapsed = 0;
            do
            {
                yield return null;
                timeElapsed += Time.deltaTime;
                color.a = startValue + (timeElapsed * perSecond);
                _image.color = color;
            } while (timeElapsed < duration);
            OnComplete?.Invoke();
        }
        

        private IEnumerator FullFadeFadeCoroutine(float fadeInDuration,  float fadeOutDuration, float timeInFullBlack, Action OnFadeMiddle, Action OnFadeEnded )
        {
            yield return Fade(fadeInDuration, 0,1, OnFadeMiddle);
            
            if(timeInFullBlack > 0)
                yield return new WaitForSecondsRealtime(timeInFullBlack);
            
            yield return Fade(fadeOutDuration,1,0, OnFadeEnded);
            
            Destroy(gameObject);
            
        }
    }
}
