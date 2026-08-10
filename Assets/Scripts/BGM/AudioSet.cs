//
// AudioSet.cs
// 
// 2026/08/04 Created By Man-Yi, Yeh
//

using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioSet : MonoBehaviour
{
    [SerializeField] private AudioInfo[] audioInfos;
    private Dictionary<BGMType, AudioInfo> m_AudioInfos = new();

    private BGMType m_NowBGMType = BGMType.None;
    private BGMType m_NextBGMTYpe = BGMType.None;
    public float m_Volume = 0.5f;

    void Start()
    {
        foreach (var audioInfo in audioInfos)
        {
            bool isAdded = m_AudioInfos.TryAdd(audioInfo.type, audioInfo);
            if (!isAdded)
            {
                Debug.LogError($"AudioInfo with type {audioInfo.type} already exists.");
            }
        }
    }

    void Update()
    {
        // Check if the next BGM type is different from the current one
        if (m_NowBGMType != BGMType.None &&
            m_NowBGMType != m_NextBGMTYpe)
        {
            if (GetAudioInfo(m_NowBGMType).GetRemainingTime() < 0.1f)
            {
                Debug.Log("BGM: Play Next");
                PlayNext();
            }
        }

        if (m_NextBGMTYpe != BGMType.None &&
            m_NextBGMTYpe != m_NowBGMType)
        {
            if (GetAudioInfo(m_NextBGMTYpe).IsPlaying())
            {
                m_NowBGMType = m_NextBGMTYpe;
            }
        }
    }

    public void SetNowAudio(BGMType type, bool loop = false)
    {
        if (type == m_NowBGMType)
        {
            return;
        }
        if (GetAudioInfo(type) == null)
        {
            Debug.LogError($"AudioInfo with type {type} not found.");
            return;
        }

        // Stop the current BGM
        if (m_NowBGMType != BGMType.None)
        {
            GetAudioInfo(m_NowBGMType)?.Stop();
        }
        // Play the new BGM
        if (type != BGMType.None)
        {
            Debug.Log($"BGM: Play {type}");
            GetAudioInfo(type)?.SetVolume(m_Volume);
            GetAudioInfo(type)?.SetLoop(loop);
            GetAudioInfo(type)?.Play();
        }
        m_NowBGMType = type;
    }

    public void SetNextAudio(BGMType type, bool loop = false)
    {
        if (GetAudioInfo(type) == null)
        {
            Debug.LogError($"AudioInfo with type {type} not found.");
            return;
        }
        m_NextBGMTYpe = type;
        GetAudioInfo(type)?.SetLoop(loop);
    }

    public void Pause()
    {
        GetAudioInfo(m_NowBGMType)?.Pause();
    }

    public void Resume()
    {
        GetAudioInfo(m_NowBGMType)?.Resume();
    }

    public void SetVolume(float volume)
    {
        m_Volume = Mathf.Clamp01(volume);
        GetAudioInfo(m_NowBGMType)?.SetVolume(m_Volume);
    }


    //-------------------
    //basic
    //-------------------
    private AudioInfo GetAudioInfo(BGMType type)
    {
        if (m_AudioInfos.TryGetValue(type, out var audioInfo))
        {
            return audioInfo;
        }
        else
        {
            Debug.LogError($"AudioInfo with type {type} not found.");
            return null;
        }
    }

    private void PlayNext()
    {
        if (m_NextBGMTYpe != BGMType.None &&
            m_NextBGMTYpe != m_NowBGMType)
        {
            double startTime = AudioSettings.dspTime +
                               GetAudioInfo(m_NowBGMType).GetRemainingTime();
            
            GetAudioInfo(m_NextBGMTYpe)?.SetVolume(m_Volume);
            GetAudioInfo(m_NextBGMTYpe)?.PlayScheduled(startTime);
        }
    }
}
