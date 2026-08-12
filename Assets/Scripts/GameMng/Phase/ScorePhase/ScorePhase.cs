//
// ScorePhase.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
// 2026/05/31 Updated By Man-Yi, Yeh
// 2026/06/15 Updated By Man-Yi, Yeh
// 2026/06/16 Updated By Man-Yi, Yeh
// 2026/08/03 Updated By Man-Yi, Yeh
//

using UnityEngine;

public class ScorePhase : Phase
{
    private ScoreInfo m_ScoreInfo;
    public ScoreInfo ScoreInfo
    {
        get { return m_ScoreInfo; }
    }

    private InGameUIScore m_ScoreUI;

    public ScorePhase(GameMng gameMng, bool isTGS) 
        : base(gameMng, isTGS)
    {
    }


    public override void Init()
    {
        GameObject scoreInfo = GameObject.Find("ScoreInfo");
        if (scoreInfo != null)
        {
            m_ScoreInfo = scoreInfo.GetComponent<ScoreInfo>();
        }
        m_ScoreUI = new InGameUIScore(m_ScoreInfo.GetScoreText(), m_ScoreInfo.GetComboText()
            , m_ScoreInfo.GetMoveableComboText(), m_ScoreInfo.GetSakuraText(), m_ScoreInfo.GetLevelText(),
          m_ScoreInfo.GetNiceTry(), m_ScoreInfo.GetGoodJob(), m_ScoreInfo.GetWelldone(),
          m_ScoreInfo.GetBronzeStamp(), m_ScoreInfo.GetSilverStamp(), m_ScoreInfo.GetGoldStamp());
        m_ScoreUI.Init();

    }

    public override void Term()
    {
        m_GameMng.InGameTerm();
        m_ScoreUI.Term();
    }

    public override void Update()
    {
        //m_ScoreUI.Update();
    }

    
}
