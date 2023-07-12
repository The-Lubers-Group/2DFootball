using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LubyLib.Sound
{
    [RequireComponent(typeof(Button))]
    public class UISFXOnClick : MonoBehaviour
    {
        public UISFXType type;

        public AudioClip overrideClip;

        // Use this for initialization
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(PlayEffect);
        }

        void PlayEffect()
        {
            if(overrideClip == null)
                UISFXManager.PlayEffect(type);
            else
                UISFXManager.PlayCustomUIEffect(overrideClip);
        }
    }
}
