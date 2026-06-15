//
// InGamePhase.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
// 2026/05/31 Updated By Man-Yi, Yeh
// 2026/06/02 Updated By Fate Ku
// 2026/06/09 Updated By Man-Yi, Yeh
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
    UISystem m_InGameUI;  //call m_GameMng.InGameClickColumn(id);
  

    public override void Init()
    {
        m_GameMng.InGameInit();

        // 2026/06/02 Updated By Fate Ku
        m_InGameUI = new InGameUI(m_GameMng);
        m_InGameUI.Init();
        // 2026/06/02 Updated By Fate Ku
    }

    public override void Term()
    {
        m_GameMng.InGameTerm();

        // 2026/06/02 Updated By Fate Ku
        if (m_InGameUI != null)
        {
            m_InGameUI.Term();
        }
        // 2026/06/02 Updated By Fate Ku

    }

    public override void Update()
    {
        m_GameMng.InGameUpdate();

        // 2026/06/02 Updated By Fate Ku
        if (m_InGameUI != null)
            m_InGameUI.Update();
        // 2026/06/02 Updated By Fate Ku

        if (m_GameMng.IsInGameEnd())
        {
            m_GameMng.SetNextScene("ScoreScene");
        }
    }


}
