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
    private AudioInfo[] audioInfos;
    private Dictionary<BGMType, AudioInfo> m_AudioInfos = new();

    private BGMType m_NowBGMType = BGMType.None;
    private List<BGMType> m_NextBGMTypes = new();
    public float m_Volume = 0.5f;

    void Start()
    {
        audioInfos = GetComponentsInChildren<AudioInfo>();

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
            m_NextBGMTypes.Count > 0)
        {
            if (m_NowBGMType != m_NextBGMTypes[0])
            {
                if (GetAudioInfo(m_NowBGMType).GetRemainingTime() < 1f)
                {
                    Debug.Log("BGM: Play Next");
                    PlayNext();
                }
            }
        }

        if (m_NextBGMTypes.Count > 0)
        {
            if (m_NextBGMTypes[0] != BGMType.None ||
                m_NextBGMTypes[0] != m_NowBGMType)
            {
                // Handle the next BGM type
                if (GetAudioInfo(m_NextBGMTypes[0]).IsPlaying())
                {
                    m_NowBGMType = m_NextBGMTypes[0];
                    m_NextBGMTypes.RemoveAt(0);
                }
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

        int index = m_NextBGMTypes.Count - 1;
        if (index >= 0 && m_NextBGMTypes[index] == type)
        {
            return;
        }


        GetAudioInfo(m_NowBGMType).SetLoop(false);
        foreach (var nextType in m_NextBGMTypes)
        {
            GetAudioInfo(nextType).SetLoop(false);
        }
        GetAudioInfo(type).SetLoop(loop);
        m_NextBGMTypes.Add(type);

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
        if (m_NextBGMTypes[0] != BGMType.None &&
            m_NextBGMTypes[0] != m_NowBGMType)
        {
            double startTime = AudioSettings.dspTime +
                               GetAudioInfo(m_NowBGMType).GetRemainingTime();
            
            GetAudioInfo(m_NextBGMTypes[0])?.SetVolume(m_Volume);
            GetAudioInfo(m_NextBGMTypes[0])?.PlayScheduled(startTime);
        }
    }
}
