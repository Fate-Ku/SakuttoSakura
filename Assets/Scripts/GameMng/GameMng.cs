//
// GameMng.cs
// 
// 2026/05/21 Created By Man-Yi, Yeh
// 2026/05/26 Updated By Man-Yi, Yeh 
// 2026/05/31 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class GameMng
{
    public enum PhaseType
    {
        SkillSelect,
        InGame,
        Score,
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
    //in game system
    private InGameSystem m_InGameSystem;

    //skill data
    private SkillDataSystem m_SkillDataSystem;


    //-------------------
    //UI
    //-------------------
    //if UI in GameMng
    //UIManager m_UIManager;



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

            case PhaseType.InGame:
                phase = new InGamePhase(this);
                break;

            case PhaseType.Score:
                phase = new ScorePhase(this);
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


    //2026/05/26 Update By Man-Yi, Yeh 
    //-------------------
    //method of InGameSystem
    //-------------------
    public void InGameSystemInit()
    {
        //renew
        m_InGameSystem = new InGameSystem(this);

        //init
        m_InGameSystem.Init();
    }

    public void InGameSystemTerm()
    {
        m_InGameSystem.Term();
    }

    public void InGameSystemUpdate()
    {
        m_InGameSystem.Update();
    }

    //2026/05/30 Updated By Man-Yi, Yeh
    //-------------------
    //get game info
    //-------------------
    public Vector2Int GetGameScale()
    {
        Vector2Int res = new(0, 0);
        if (m_InGameSystem != null) 
        {
            if (m_InGameSystem.GameInfo != null)
            {
                res = m_InGameSystem.GameInfo.GetScale();
            }
        }
        
        return res;
    }

    public Vector2 GetGameReferPos()
    {
        Vector2 res = new(0, 0);

        if (m_InGameSystem != null)
        {
            if (m_InGameSystem.GameInfo != null)
            {
                res = m_InGameSystem.GameInfo.GetReferPos();
            }
        }

        return res;
    }

    //2026/05/26 Update By Man-Yi, Yeh 
    //-------------------
    //ingame click column
    //-------------------
    public void InGameClickColumn(int id)
    {
        Debug.Log("click col: " + id.ToString());
    }


}
