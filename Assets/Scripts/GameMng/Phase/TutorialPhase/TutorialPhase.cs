//
// TutorialPhase.cs
// 
// 2026/07/13 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class TutorialPhase : Phase
{
    public TutorialPhase(GameMng gameMng, bool isTGS) 
        : base(gameMng, isTGS)
    {
    }

    //-------------------
    //UI
    //-------------------
    UISystem m_InGameUI;  //call m_GameMng.InGameClickColumn(id);


    public override void Init()
    {
        m_GameMng.InGameInit(m_IsTGS, InGameType.Tutorial);

        m_InGameUI = new InGameUI(m_GameMng);
        m_InGameUI.Init();

    }

    public override void Term()
    {
        m_InGameUI?.Term();

    }

    public override void Update()
    {
        m_GameMng.InGameUpdate();

        m_InGameUI?.Update();

        if (m_GameMng.IsInGameEnd())
        {
            m_GameMng.SetNextScene("MenuScene");
        }
    }
}
