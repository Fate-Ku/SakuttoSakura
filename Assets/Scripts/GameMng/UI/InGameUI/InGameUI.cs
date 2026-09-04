//
// InGameUI.cs
// 
// 2026/05/31 Created By Fate Ku
// 2026/06/02 Updated By Fate Ku
// 2026/06/06 Added InGameUIBackground By Fate Ku
// 2026/06/14 Added By Fate Ku
// 2026/06/22 Added By Fate Ku
// 2026/06/23 Updated By Fate Ku
// 2026/06/30 Updated By Fate Ku
// 2026/07/10 Updated By Fate Ku
// 2026/07/17 Updated By Fate Ku
// 2026/08/03 Updated By Fate Ku
// 2026/08/24 Updated By Fate Ku
// 2026/09/04 Updated By Fate Ku
//

using UnityEngine;

public class InGameUI : UISystem
{
    private ScoreInfo m_ScoreInfo;

    public ScoreInfo ScoreInfo
    {
        get { return m_ScoreInfo; }
    }
    //private InGameUIButton m_ButtonSystem;
    //private InGameUIBackground m_Background;
    private InGameUIScore m_ScoreUI;
    private InGameUITimer m_Timer;

    // 2026/09/04 Added By Fate Ku
    // Sakura Info
    private SakuraInfo m_SakuraInfo;

    public SakuraInfo SakuraInfo
    { get { return m_SakuraInfo; } }
    // 2026/09/04 Added By Fate Ku

    public InGameUI(GameMng gameMng)
        : base(gameMng)
    {
        //m_ButtonSystem = new InGameUIButton();
        //m_Background = new InGameUIBackground();
    }

    public override void Init()
    {

        //m_ButtonSystem.Init();
        //m_Background.Init();

        // 2026/09/04 Added By Fate Ku
        // Sakura Info
        GameObject sakuraInfo = GameObject.Find("SakuraInfo");

        if (sakuraInfo != null)
        {
            m_SakuraInfo = sakuraInfo.GetComponent<SakuraInfo>();
        }
        // 2026/09/04 Added By Fate Ku


        GameObject scoreInfo = GameObject.Find("ScoreInfo");
        if (scoreInfo != null)
        {
            m_ScoreInfo = scoreInfo.GetComponent<ScoreInfo>();
        }
        
        m_ScoreUI = new InGameUIScore(m_ScoreInfo.GetScoreText(), m_ScoreInfo.GetComboText()
            , m_ScoreInfo.GetMoveableComboText(), m_ScoreInfo.GetSakuraText(), m_ScoreInfo.GetLevelText(),
          m_ScoreInfo.GetNiceTry(), m_ScoreInfo.GetGoodJob(), m_ScoreInfo.GetWelldone(),
          m_ScoreInfo.GetBronzeStamp(), m_ScoreInfo.GetSilverStamp(), m_ScoreInfo.GetGoldStamp(),
          m_ScoreInfo.GetSakuraRenderer(),m_ScoreInfo.GetSakuraTarget(),
          // 2026/09/04 Added By Fate Ku
          m_SakuraInfo.GetSakura1(), m_SakuraInfo.GetSakura2(), m_SakuraInfo.GetSakura3(),
            m_SakuraInfo.GetSakura5(), m_SakuraInfo.GetSakura7(), m_SakuraInfo.GetSakura9(),
            m_SakuraInfo.GetSakura11(), m_SakuraInfo.GetSakura14(), m_SakuraInfo.GetSakura17(),
            m_SakuraInfo.GetSakura20(), m_SakuraInfo.GetSakura23(), m_SakuraInfo.GetSakura26(),
            m_SakuraInfo.GetSakura29(), m_SakuraInfo.GetSakura32(), m_SakuraInfo.GetSakura35(),
            m_SakuraInfo.GetSakura38(), m_SakuraInfo.GetSakura41(), m_SakuraInfo.GetSakura44(),
            m_SakuraInfo.GetSakura47(), m_SakuraInfo.GetSakura50(), m_SakuraInfo.GetSakura53(),
            m_SakuraInfo.GetSakura56(), m_SakuraInfo.GetSakura60());
        // 2026/09/04 Added By Fate Ku
        m_ScoreUI.Init();

        m_Timer = new InGameUITimer(m_ScoreInfo.GetTimeText(), m_ScoreInfo.GetTimerSlider());
        m_Timer.Init();

        //Debug.Log("InGameUI Init");
    }

    public override void Update()
    {
        //m_ButtonSystem.Update();
        m_ScoreUI.Update();
        m_Timer.Update();
        //Debug.Log("InGameUI Update");
    }

    public override void Term()
    {
        //m_ButtonSystem.Term();
        //m_Background.Term();
        m_ScoreUI.Term();
        m_Timer.Term();
        //Debug.Log("InGameUI Term");
    }
}
