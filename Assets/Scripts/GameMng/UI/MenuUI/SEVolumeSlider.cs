//
// SEVolumeSlider.cs
//
// 2026/09/13 Created By Fate Ku
//

using UnityEngine;
using UnityEngine.UI;

public class SEVolumeSlider : MonoBehaviour
{
    [SerializeField]private Slider m_SESlider;
    [SerializeField]private Slider m_BGMSlider;

    private void Start()
    {
        // init
        m_SESlider.value = 1.0f;
        m_BGMSlider.value = 0.5f;

        // change
        m_SESlider.onValueChanged.AddListener(OnSEVolumeChanged);
        m_BGMSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
    }

    public void OnSEVolumeChanged(float value)
    {
        Debug.Log("SE Volume : " + value);
        SEMng.Instance.SetVolume(value);
    }

    public void OnBGMVolumeChanged(float value)
    {
        Debug.Log("BGM Volume : " + value);
        BGMMng.Instance.SetBGMVolume(value);
    }
}