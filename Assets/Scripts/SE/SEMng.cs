//
// SEMng.cs
// 
// 2026/09/09 Created By Man-Yi, Yeh
// 


using UnityEngine;

public class SEMng
{
    private static SEMng m_Instance;
    public static SEMng Instance
    {
        get {
            if (m_Instance == null) {
                m_Instance = new SEMng();
            }
            return m_Instance;
        }
    }
    private SEMng() { }

    private SESet m_SEController;
    private float m_Volume = 1.0f;

    public void Init(SESet controller) 
    {
        m_SEController = controller;
    }

    public void SetVolume(float volume)
    {
        m_Volume = Mathf.Clamp01(volume);
    }

    public void PlayUISE(UISEType type)
    {
        if (m_SEController != null)
        {
            m_SEController.PlayUISE(type, m_Volume);
        }
    }

    public void PlayGameSE(GameSEType type)
    {
        if (m_SEController != null)
        {
            m_SEController.PlayGameSE(type, m_Volume);
        }
    }

    public void PlayComboSE(int combo)
    {
        if (m_SEController != null)
        {
            m_SEController.PlayComboSE(combo, m_Volume);
        }
    }
}
