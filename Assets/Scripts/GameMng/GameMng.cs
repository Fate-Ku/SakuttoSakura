//
// GameMng.cs
// 
// 2026/05/21 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class GameMng
{
    public enum PhaseType
    {
        SkillSelect,
    }

    //singleton
    private static GameMng m_Instance;
    public static GameMng Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new GameMng();
            }
            return m_Instance;
        }
    }
    private GameMng() { }

    //-------------------
    //basic info
    //-------------------
    //is game end
    private bool m_IsGameEnd;
    //phase
    private Phase m_NowPhase;



    //-------------------
    //game system
    //-------------------
    //skill data
    private SkillDataSystem m_SkillDataSystem;
    


    public void Init()
    {
        m_IsGameEnd = false;

        m_SkillDataSystem = new SkillDataSystem(this);
    }

    public void Term()
    {
        if (m_NowPhase != null)
        {
            m_NowPhase.Term();
        }
    }

    public void Update()
    {
        if (m_NowPhase != null)
        {
            m_NowPhase.Update();
        }
    }

    public bool IsGameEnd()
    {
        return m_IsGameEnd;
    }

    public void SetPhase(PhaseType phaseType)
    {
        if (m_NowPhase != null)
        {
            m_NowPhase.Term();
        }

        Phase phase = null;
        switch (phaseType) 
        {
            case PhaseType.SkillSelect:
                phase = new SkillSelectPhase(this);
                break;

            default:
                Debug.Log("Don't have the phase");
                break;
        }
        if (phase != null)
        {
            phase.Init();
        }

        m_NowPhase = phase;
    }

}
