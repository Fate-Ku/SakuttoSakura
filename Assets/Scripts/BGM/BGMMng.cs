//
// BGMMng.cs
// 
// 2026/08/04 Created By Man-Yi, Yeh
//

using UnityEngine;

public enum BGMType
{
    None = -1,

    Intro,
    A1Loop,
    A1Final,
    BLoop,
    BFinal,
    CLoop,
    CFinal,
    Bridge,
    A2Loop,
    Outro,

    Ingame,

    Count
}

public class BGMMng
{
    private static BGMMng m_Instance;
    public static BGMMng Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new BGMMng();
            }
            return m_Instance;
        }
    }
    private BGMMng() { }

    private AudioSet m_AudioSet;
    private float m_Volume = 0.5f;

    public void SetAudioSet()
    {
        Debug.Log("Setting AudioSet in BGMMng");
        GameObject audioSet = GameObject.Find("AudioSet");
        if (audioSet != null)
        {
            m_AudioSet = audioSet.GetComponent<AudioSet>();
            if (m_AudioSet == null)
            {
                Debug.LogError("AudioSet component not found on the AudioSet GameObject.");
            }
            else
            {
                m_AudioSet.SetVolume(m_Volume);
            }
        }
        else
        {
            Debug.LogError("AudioSet GameObject not found in the scene.");
        }
    }

    public void SetBGM(BGMType bgmType, bool loop = false)
    {
        if (m_AudioSet != null)
        {
            m_AudioSet.SetNowAudio(bgmType, loop);
        }
    }

    public void SetNextBGM(BGMType bgmType, bool loop = false)
    {
        Debug.Log($"Setting next BGM: {bgmType}, loop: {loop}");
        if (m_AudioSet != null)
        {
            m_AudioSet.SetNextAudio(bgmType, loop);
        }
    }

    public void PauseBGM()
    {
        if (m_AudioSet != null)
        {
            m_AudioSet.Pause();
        }
    }

    public void ResumeBGM()
    {
        if (m_AudioSet != null)
        {
            m_AudioSet.Resume();
        }
    }

    //volume: 0.0f ~ 1.0f
    public void SetBGMVolume(float volume)
    {
        m_Volume = Mathf.Clamp01(volume);
        if (m_AudioSet != null)
        {
            m_AudioSet.SetVolume(m_Volume);
        }
    }
}
