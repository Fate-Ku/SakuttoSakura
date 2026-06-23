//
// InGameUITimer.cs
// 
// 2026/06/16 Created By Fate Ku
//

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUITimer
{
    private Slider m_TimerBar;
    private TextMeshProUGUI m_TimerText;

    //-------------------
    //game info
    //-------------------
    private GameInfo m_GameInfo;
    public GameInfo GameInfo
    {
        get { return m_GameInfo; }
    }

    public InGameUITimer(TextMeshProUGUI timerText, Slider timerSlider)
    {
        m_TimerText = timerText;
        m_TimerBar = timerSlider;
    }

    public void Init()
    {

        //TimerBar = GetComponent<Slider>();
        GameObject gameInfo = GameObject.Find("GameInfo");
        if (gameInfo != null)
        {
            m_GameInfo = gameInfo.GetComponent<GameInfo>();
        }

        m_TimerBar.maxValue = m_GameInfo.GetPlayTime();
        Debug.Log("maxTimer" + m_TimerBar.maxValue);
    }

    public void Update()
    {
        if (m_TimerText != null)
        {
            float timer = GameMng.Instance.GetGameTime();

            m_TimerText.text = ((int)timer).ToString();
            //Debug.Log("timer" + timer);

            UpdateTimerBar(timer);

        }
    }

    public void Term()
    {
        m_TimerText = null;
    }

    private void UpdateTimerBar(float timer)
    {
        m_TimerBar.value = timer;

        //Debug.Log("timer" + m_TimerBar.value);
    }

}