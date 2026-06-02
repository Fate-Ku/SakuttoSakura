//
// InGameUI.cs
// 
// 2026/05/31 Created By Fate Ku
// 2026/06/02 Updated By Fate Ku
//
using UnityEngine;

public class InGameUI : UISystem
{
    private InGameUIButton m_ButtonSystem;

    public InGameUI(GameMng gameMng)
        : base(gameMng)
    {

    }

    public override void Init()
    {

        m_ButtonSystem.Init();
        Debug.Log("InGameUI Init");

    }

    public override void Update()
    {
        m_ButtonSystem.Update();
        Debug.Log("InGameUI Update");
    }

    public override void Term()
    {
        m_ButtonSystem.Term();
        Debug.Log("InGameUI Term");
    }
}
