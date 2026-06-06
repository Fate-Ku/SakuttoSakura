//
// InGameUI.cs
// 
// 2026/05/31 Created By Fate Ku
// 2026/06/02 Updated By Fate Ku
// 2026/06/06 Added InGameUIBackground By Fate Ku
//
using UnityEngine;
using UnityEngine.UIElements;

public class InGameUI : UISystem
{
    private InGameUIButton m_ButtonSystem;
    private InGameUIBackground m_Background;

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
        m_Background.Term();
        Debug.Log("InGameUI Term");
    }
}
