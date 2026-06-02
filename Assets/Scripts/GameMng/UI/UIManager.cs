//
// UIManager.cs
// 
// 2026/05/31 Created By Fate Ku
// 2026/06/01 Updated By Fate Ku
//
using UnityEngine;

public class UIManager : IGameSystem
{
    private IGameSystem m_CurrentUI = null;

    public UIManager(GameMng gameMng)
        : base(gameMng)
    {
    }

    public UIManager GetSelf()
    {
        return this;
    }

    public override void Init()
    {

        Debug.Log("UIManager Init");
    }

    public override void Term()
    {
        if (m_CurrentUI != null)
        {
            m_CurrentUI.Term();
            m_CurrentUI = null;
        }
        Debug.Log("UIManager Term");
    }

    public override void Update()
    {
        if (m_CurrentUI != null)
        {
            m_CurrentUI.Update();
        }
    }

    public void SetUI(GameMng.PhaseType phaseType)
    {
        // clean old UI
        if (m_CurrentUI != null)
        {
            m_CurrentUI.Term();
            m_CurrentUI = null;
        }

        // Create UI by PhaseType
        switch (phaseType)
        {
            case GameMng.PhaseType.SkillSelect:
                m_CurrentUI = new SkillSelectUI(m_GameMng);
                break;

            case GameMng.PhaseType.InGame:
                m_CurrentUI = new InGameUI(m_GameMng);
                break;

            case GameMng.PhaseType.Score:
                m_CurrentUI = new ScoreUI(m_GameMng);
                break;

            default:
                Debug.LogWarning("UIManager.SetUI: unknown PhaseType " + phaseType);
                break;
        }

        // Init new UI
        if (m_CurrentUI != null)
        {
            m_CurrentUI.Init();
        }
    }

    //-------------------------------------
    //In Game Click Button
    //-------------------------------------
    private int selectedIndex = -1; // default:-1 (no choose)

    public void SetSelectedIndex(int index)
    {
        selectedIndex = index;
        Debug.Log("UI MGR Set Selected button Index = " + index);
    }

    public int GetSelectedIndex()
    {
        return selectedIndex;
    }

}
