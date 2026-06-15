//
// InGameUI.cs
// 
// 2026/05/31 Created By Fate Ku
// 2026/06/02 Updated By Fate Ku
// 2026/06/06 Added InGameUIBackground By Fate Ku
// 2026/06/14 Added By Fate Ku
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
        m_ScoreUI = new InGameUIScore(m_ScoreInfo.GetScoreText());
        m_ScoreUI.Init();

        Debug.Log("InGameUI Init");
    }

    public override void Update()
    {
        m_ButtonSystem.Update();
        m_ScoreUI.Update();
        Debug.Log("InGameUI Update");
    }

    public override void Term()
    {
        m_ButtonSystem.Term();
        m_Background.Term();
        m_ScoreUI.Term();
        Debug.Log("InGameUI Term");
    }
}
