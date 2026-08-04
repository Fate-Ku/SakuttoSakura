//
// BGMController.cs
// old version for BGM management, replaced by BGMMng.cs
// 
// 2026/08/04 Created By Man-Yi, Yeh
//

using System;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;
    private BGMType m_NowBGMType = BGMType.None;
    private BGMType m_NextBGMTYpe = BGMType.None;
    public float m_Volume = 1.0f;

    private void Awake()
    {
        //don't destroy
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // Check if the next BGM type is different from the current one
        if (m_NowBGMType != BGMType.None &&
            m_NowBGMType != m_NextBGMTYpe)
        {
            if (audioSources[(int)m_NowBGMType].clip.length - audioSources[(int)m_NowBGMType].time
                < 0.2f)
            {
                Debug.Log("BGM: Play Next");
                PlayNext();
            }
        }
    }

    public void SetBGM(BGMType bgmType)
    {
        if (bgmType == m_NowBGMType)
        {
            return;
        }

        // Stop the current BGM
        if (m_NowBGMType != BGMType.None)
        {
            audioSources[(int)m_NowBGMType].Stop();
        }
        // Play the new BGM
        if (bgmType != BGMType.None)
        {
            audioSources[(int)bgmType].volume = m_Volume;
            audioSources[(int)bgmType].Play();
        }
        m_NowBGMType = bgmType;
    }

    public void SetNextBGM(BGMType bgmType)
    {
        m_NextBGMTYpe = bgmType;
    }

    public void PauseBGM()
    {
        if (m_NowBGMType != BGMType.None)
        {
            audioSources[(int)m_NowBGMType].Pause();
        }   
    }

    public void ResumeBGM()
    {
        if (m_NowBGMType != BGMType.None)
        {
            audioSources[(int)m_NowBGMType].UnPause();
        }
    }

    public void SetBGMVolume(float volume)
    {
        m_Volume = Mathf.Clamp01(volume);
        if (m_NowBGMType != BGMType.None)
        {
            audioSources[(int)m_NowBGMType].volume = m_Volume;
        }
    }

    private void PlayNext()
    {
        if (m_NextBGMTYpe != BGMType.None)
        {
            double startTime = AudioSettings.dspTime +
                               audioSources[(int)m_NowBGMType].clip.length - audioSources[(int)m_NowBGMType].time;
            audioSources[(int)m_NextBGMTYpe].volume = m_Volume;
            audioSources[(int)m_NextBGMTYpe].PlayScheduled(startTime);
        }
        m_NowBGMType = m_NextBGMTYpe;
    }
}
