//
// InGameUI.cs
// 
// 2026/05/31 Created By Fate Ku
//
//
using UnityEngine;

public class InGameUI : IGameSystem
{
    private InGameUIButton m_ButtonSystem;

    public InGameUI(GameMng gameMng)
        : base(gameMng)
    {
    }

    public override void Init()
    {
        Debug.Log("InGameUI Init");

        m_ButtonSystem.Init();

    }

    public override void Update()
    {
        
    }

    public override void Term()
    {
        Debug.Log("InGameUI Term");

        m_ButtonSystem.Term();
    }
}
