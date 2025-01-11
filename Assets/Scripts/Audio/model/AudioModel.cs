using System;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class AudioModel
{

    public string name;
    public AudioClip clip;
    public AudioSource audioSource;

    public bool PlayOnAwake = false;
    public float Volume = 1f;
    public bool isPitchRandomize;

    public float minPitch = 1f;
    public float maxPitch = 1f;
    



    public void InitializeAudioSource()
    {
        audioSource.clip = clip;
        audioSource.volume = Volume;
        audioSource.pitch = maxPitch;
        audioSource.playOnAwake = PlayOnAwake;
    }
    internal void Play()
    {
        try
        {
            if (audioSource.isPlaying)
                audioSource.Stop();

            if(isPitchRandomize)
                maxPitch=OnRandomizePitch();
            audioSource.pitch = maxPitch;
            audioSource.PlayOneShot(clip);
        }
        catch (System.Exception)
        {
            Debug.Log("Unable to play due to missing audio source?");
        }
    }

    public float OnRandomizePitch()
    {
        return UnityEngine.Random.Range(minPitch, maxPitch);
    }

    internal void Stop()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}