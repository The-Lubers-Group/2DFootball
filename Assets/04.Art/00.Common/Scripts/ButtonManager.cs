using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default;
    [SerializeField] private Sprite _pressed;
    [SerializeField] private AudioClip _compressClip;
    [SerializeField] private AudioClip _uncompressClip;
    [SerializeField] private AudioSource _source;


    public void OnPointDown(PointerEventData eventData)
    {
        _img.sprite = _pressed;
        _source.PlayOneShot(_compressClip);
    }

    public void OnPointUp(PointerEventData eventData) 
    {
        _img.sprite = _default;
        _source.PlayOneShot(_uncompressClip);
    }
}
