using System;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public Audio[] sounds, music;
    int musicTrackID;
    bool mixStopRun;
    void Awake()
    {
        musicTrackID = UnityEngine.Random.Range(0, music.Length - 1);
        foreach (Audio a in sounds)
        {
            a.audioSource = gameObject.AddComponent<AudioSource>();
            a.audioSource.clip = a.clip;
            a.audioSource.volume = a.volume;
            a.audioSource.pitch = a.pitch;
            a.audioSource.loop = a.loop;
        }
        foreach (Audio a in music)
        {
            a.audioSource = gameObject.AddComponent<AudioSource>();
            a.audioSource.clip = a.clip;
            a.audioSource.volume = a.volume;
            a.audioSource.pitch = a.pitch;
            a.audioSource.loop = a.loop;
        }
        mixStopRun = false;
    }
    public void PlaySound(string clipName)
    {
        Audio a = Array.Find(sounds, Audio => Audio.clipName == clipName);
        a.audioSource.Play();
    }
    public void StopSound(string clipName)
    {
        Audio a = Array.Find(sounds, Audio => Audio.clipName == clipName);
        a.audioSource.Stop();
    }
    public void PitchSound(string clipName, float pitch)
    {
        Audio a = Array.Find(sounds, Audio => Audio.clipName == clipName);
        a.audioSource.pitch = pitch;
    }
    public void VolumeSound(string clipName, float volume)
    {
        Audio a = Array.Find(sounds, Audio => Audio.clipName == clipName);
        a.audioSource.volume = volume;
    }
    public void PlayMusicMix()
    {

        music[musicTrackID].audioSource.Play();
    }
    public void StopMusicMix()
    {
        if (!mixStopRun)
        {
            mixStopRun=true;
            PlaySound("TrackEnd");
        }
        music[musicTrackID].audioSource.Stop();
    }
}
