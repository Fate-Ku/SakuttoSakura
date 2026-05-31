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
        GameMng.Instance.InGameSystemInit();
        //m_InGameUI.Init();
    }

    public override void Term()
    {
        GameMng.Instance.InGameSystemTerm();
        //m_InGameUI.Term();
    }

    public override void Update()
    {
        GameMng.Instance.InGameSystemUpdate();
        //m_InGameUI.Update();
    }


}
