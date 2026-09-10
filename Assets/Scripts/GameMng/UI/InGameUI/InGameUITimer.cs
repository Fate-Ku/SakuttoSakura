//
// InGameUITimer.cs
// 
// 2026/06/16 Created By Fate Ku
// 2026/09/09 Updated By Fate Ku
//

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUITimer
{
    private Slider m_TimerBar;
    private TextMeshProUGUI m_TimerText;

    private GameObject m_Petal;

    private RectTransform m_TimerBarRect;

    private float m_YOffset = 34f;
    private float m_XOffset = 50f;

    //-------------------
    //game info
    //-------------------
    private GameInfo m_GameInfo;
    public GameInfo GameInfo
    {
        get { return m_GameInfo; }
    }

    public InGameUITimer(TextMeshProUGUI timerText, Slider timerSlider,GameObject petal)
    {
        m_TimerText = timerText;
        m_TimerBar = timerSlider;

        m_Petal = petal;
    }

    public void Init()
    {
        GameObject gameInfo = GameObject.Find("GameInfo");

        if (gameInfo != null)
        {
            m_GameInfo = gameInfo.GetComponent<GameInfo>();
        }

        m_TimerBar.maxValue = m_GameInfo.GetPlayTime();

        m_TimerBarRect = m_TimerBar.GetComponent<RectTransform>(); // 2026/09/09 Updated By Fate Ku

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

        // 2026/09/09 Updated By Fate Ku
        float ratio = m_TimerBar.normalizedValue;

        Vector3[] corners = new Vector3[4];
        m_TimerBarRect.GetWorldCorners(corners);

        Vector3 left = corners[0];
        Vector3 right = corners[3];

        Vector3 targetPos = Vector3.Lerp(left, right, ratio);

        if (m_Petal != null)
        {
            Vector3 petalPos = targetPos;
            petalPos.y += m_YOffset;
            petalPos.x += m_XOffset;

            m_Petal.transform.position = petalPos;
        }

        if (m_TimerText != null)
        {
            Vector3 textPos = targetPos;
            textPos.y += m_YOffset;
            textPos.x += m_XOffset;

            m_TimerText.transform.position = textPos;
        }
        // 2026/09/09 Updated By Fate Ku
    }

}