//
// InGameUI.cs
// 
// 2026/05/31 Created By Fate Ku
// 2026/06/02 Updated By Fate Ku
// 2026/06/06 Added InGameUIBackground By Fate Ku
// 2026/06/14 Added By Fate Ku
// 2026/06/22 Added By Fate Ku
// 2026/06/23 Updated By Fate Ku
//

using UnityEngine;

public class InGameUI : UISystem
{
    private ScoreInfo m_ScoreInfo;
 
    public ScoreInfo ScoreInfo
    {
        get { return m_ScoreInfo; }
    }
    private InGameUIButton m_ButtonSystem;
    private InGameUIBackground m_Background;
    private InGameUIScore m_ScoreUI;
    private InGameUITimer m_Timer;

    public InGameUI(GameMng gameMng)
        : base(gameMng)
    {
        m_ButtonSystem = new InGameUIButton();
        m_Background = new InGameUIBackground();
    }

    public override void Init()
    {
        m_ButtonSystem.Init();
        m_Background.Init();


        GameObject scoreInfo = GameObject.Find("ScoreInfo");
        if (scoreInfo != null)
        {
            m_ScoreInfo = scoreInfo.GetComponent<ScoreInfo>();
        }
        m_ScoreUI = new InGameUIScore(m_ScoreInfo.GetScoreText(),m_ScoreInfo.GetComboText()
            , m_ScoreInfo.GetMoveableComboText(),m_ScoreInfo.GetSakuraText());
        m_ScoreUI.Init();

        m_Timer = new InGameUITimer(m_ScoreInfo.GetTimeText(),m_ScoreInfo.GetTimerSlider());
        m_Timer.Init();

        //Debug.Log("InGameUI Init");
    }

    public override void Update()
    {
        m_ButtonSystem.Update();
        m_ScoreUI.Update();
        m_Timer.Update();
        //Debug.Log("InGameUI Update");
    }

    public override void Term()
    {
        m_ButtonSystem.Term();
        m_Background.Term();
        m_ScoreUI.Term();
        m_Timer.Term();
        //Debug.Log("InGameUI Term");
    }
}
