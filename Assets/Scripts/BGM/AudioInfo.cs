//
// AudioInfo.cs
// 
// 2026/08/04 Created By Man-Yi, Yeh
//

using UnityEngine;
using UnityEngine.Rendering;

public class AudioInfo : MonoBehaviour
{
    public BGMType type;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on " + gameObject.name);
        }
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }

    public void Play()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void Stop()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void Pause()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    public void Resume()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
    }

    public void SetLoop(bool loop)
    {
        audioSource.loop = loop;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public double GetRemainingTime()
    {
        return audioSource.clip.length - audioSource.time;
    }

    public void PlayScheduled(double time)
    {
        audioSource.PlayScheduled(time);
    }
}
