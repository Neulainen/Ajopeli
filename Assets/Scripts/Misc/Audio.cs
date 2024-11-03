using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Audio
{
    public string clipName;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume=1;
    [Range(0.1f, 5f)]
    public float pitch=1;
    public bool loop;
    [HideInInspector]
    public AudioSource audioSource;
  
}
