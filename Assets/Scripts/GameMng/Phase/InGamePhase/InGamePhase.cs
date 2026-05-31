//
// InGamePhase.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
// 2026/05/31 Updated By Man-Yi, Yeh
//

using UnityEngine;

public class InGamePhase : Phase
{
    public InGamePhase(GameMng gameMng) 
        : base(gameMng)
    {
    }

    //-------------------
    //UI
    //-------------------
    //if UI in Phase
    //UIManager m_InGameUI;

    public override void Init()
    {
        m_GameMng.InGameSystemInit();
        //m_InGameUI.Init();
    }

    public override void Term()
    {
        m_GameMng.InGameSystemTerm();
        //m_InGameUI.Term();
    }

    public override void Update()
    {
        m_GameMng.InGameSystemUpdate();
        //m_InGameUI.Update();

        if (m_GameMng.IsInGameEnd())
        {
            m_GameMng.SetNextScene("ScoreScene");
        }
    }


}
