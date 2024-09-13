using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    public AudioSource audioSourceSFX;
    public AudioSource audioSourceBGM;
    public AudioSource audioSourceEnemy;
    public AudioSource audioSourceTurret;

    public AudioType[] AudioTypes;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MuteAudioSource(AudioConfig.AudioSourceList sourceName, bool value)
    {
        switch (sourceName)
        {
            case AudioConfig.AudioSourceList.audioSourceSFX:
                audioSourceSFX.mute = value;
                break;

            case AudioConfig.AudioSourceList.audioSourceBGM:
                audioSourceBGM.mute = value;
                break;

            case AudioConfig.AudioSourceList.audioSourceEnemy:
                audioSourceEnemy.mute = value;
                break;

            case AudioConfig.AudioSourceList.audioSourceTurret:
                audioSourceTurret.mute = value;
                break;

        }
    }

    public void PlaySFX(AudioConfig.AudioNames audioName)
    {
        AudioClip clip = GetAudioClip(audioName);
        if (clip != null)
        {
            audioSourceSFX.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("Audio Clip not found for " + audioName);
        }
    }

    public void PlayBGM(AudioConfig.AudioNames audioName)
    {
        AudioClip clip = GetAudioClip(audioName);
        if (clip != null)
        {
            audioSourceBGM.clip = clip;
            audioSourceBGM.Play();
        }
        else
        {
            Debug.LogError("Audio Clip not found for " + audioName);
        }
    }

    public void PlayEnemySFX(AudioConfig.AudioNames audioName)
    {
        AudioClip clip = GetAudioClip(audioName);
        if (clip != null)
        {
            audioSourceEnemy.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("Audio Clip not found for " + audioName);
        }
    }

    public void PlayTurretSFX(AudioConfig.AudioNames audioName)
    {
        AudioClip clip = GetAudioClip(audioName);
        if (clip != null)
        {
            audioSourceTurret.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("Audio Clip not found for " + audioName);
        }
    }

    public AudioClip GetAudioClip(AudioConfig.AudioNames audio_name)
    {
        AudioType audioItem = Array.Find(AudioTypes, item => item.audioName == audio_name);
        if (audioItem != null)
        {
            return audioItem.audioClip;
        }
        return null;
    }

}
