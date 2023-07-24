using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    // Background
    public AudioClip[] clips;
    public AudioSource musicBG;

    // SongFX
    public AudioClip[] clipsFX;
    public AudioSource songsFX;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;    
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!musicBG.isPlaying)
        {
            musicBG.clip = GetRandom();
            musicBG.Play();
        }
    }

    //Pegue uma música aleatória e toque
    AudioClip GetRandom()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    //Cria um evento para tocar uma efeito sonoro
    public void SongsFXPlay(int index)
    {
        songsFX.clip = clipsFX[index];
        songsFX.Play();
    }

}
