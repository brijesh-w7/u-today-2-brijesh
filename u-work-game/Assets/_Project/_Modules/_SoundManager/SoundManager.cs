using System;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : SingletonMono<SoundManager>
{
    [SerializeField] AudioClips clips;
    public AudioClips Clips { get => clips; }

    bool isSoundEnabled = true;
    AudioSource _bgMusicSource;
    AudioSource commonAudioSource;

    public void UpdateSoundState()
    {
        isSoundEnabled = Prefs.SoundEnabled;
    }

    protected override void Awake()
    {
        base.Awake();
        UpdateSoundState();

    }

    private void Start()
    {
        PlayBackgroundMusic();
    }
    public void Play(AudioClip clip)
    {
        LogsManager.Log(ClassMethodName, clip != null ? clip.name : "null");
        if (!isSoundEnabled || clip == null)
        {
            return;
        }
        GetCommonAudioSource().PlayOneShot(clip);

    }

    public void PlayBackgroundMusic()
    {
        if (!Prefs.MusicEnabled)
        {
            return;
        }

        if (_bgMusicSource == null)
        {
            _bgMusicSource = gameObject.AddComponent<AudioSource>();
        }

        _bgMusicSource.clip = clips.backGroundMusic;
        _bgMusicSource.loop = true;
        _bgMusicSource.volume = 0.2f;
        _bgMusicSource.Play();
    }
    public void StopBackgroundMusic()
    {

        if (_bgMusicSource != null)
        {
            _bgMusicSource.Stop();
        }
    }



    AudioSource GetCommonAudioSource()
    {

        if (commonAudioSource == null)
        {
            commonAudioSource = gameObject.AddComponent<AudioSource>();
        }
        return commonAudioSource;
    }
}
