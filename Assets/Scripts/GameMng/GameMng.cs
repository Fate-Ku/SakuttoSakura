//
// GameMng.cs
// 
// 2026/05/21 Created By Man-Yi, Yeh
// 2026/05/26 Updated By Man-Yi, Yeh 
// 2026/05/31 Updated By Man-Yi, Yeh
// 2026/06/02 Updated By Man-Yi, Yeh
// 2026/06/09 Updated By Man-Yi, Yeh
// 2026/06/10 Updated By Man-Yi, Yeh
// 2026/06/11 Updated By Man-Yi, Yeh
// 2026/06/15 Updated By Fate Ku
// 2026/06/16 Updated By Man-Yi, Yeh
// 

using System;
using System.Collections.Generic;
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
    //is scene end
    private bool m_IsSceneEnd = false;
    public bool IsSceneEnd
    {
        get { return m_IsSceneEnd; }
        set { m_IsSceneEnd = value; }
    }

    private string m_NextSceneName;
    public string NextSceneName
    {
        get { return m_NextSceneName; }
        set { m_NextSceneName = value; }
    }

    //phase
    private Phase m_NowPhase;


    //-------------------
    //game system
    //-------------------
    //in game system
    private InGameSystem m_InGameSystem;

    //skill data
    private SkillDataSystem m_SkillDataSystem;

    //score system
    private ScoreSystem m_ScoreSystem;

    //game log system
    private GameLogSystem m_GameLogSystem;

    //effect system
    private EffectSystem m_EffectSystem;


    public void Init()
    {
        m_SkillDataSystem = new SkillDataSystem(this);
        m_EffectSystem = new EffectSystem(this);
    }

    public void Term()
    {
        m_NowPhase?.Term();
    }

    public void Update()
    {
        m_NowPhase?.Update();
    }

    public void SetNextScene(string nextSceneName)
    {
        m_IsSceneEnd = true;
        m_NextSceneName = nextSceneName;
    }

    //2026/05/31 Update By Man-Yi, Yeh 
    //-------------------
    //method of phase
    //-------------------
    public void SetPhase(PhaseType phaseType)
    {
        m_NowPhase?.Term();

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

    public void EndPhase()
    {
        m_NowPhase?.Term();
        m_NowPhase = null;
    }

    //2026/05/26 Update By Man-Yi, Yeh 
    //-------------------
    //method of InGameSystem
    //-------------------
    public void InGameInit()
    {
        //renew
        m_InGameSystem = new InGameSystem(this);
        m_ScoreSystem = new ScoreSystem(this);
        m_GameLogSystem = new GameLogSystem(this);

        //init
        m_InGameSystem?.Init();
        m_ScoreSystem?.Init();
        m_GameLogSystem?.Init();
        m_EffectSystem?.Init();
    }

    public void InGameTerm()
    {
        m_InGameSystem?.Term();
        m_ScoreSystem?.Term();
        m_GameLogSystem?.Term();
        m_EffectSystem?.Term();

        m_InGameSystem = null;
    }

    public void InGameUpdate()
    {
        m_InGameSystem?.Update();
        m_ScoreSystem?.Update();
        m_GameLogSystem?.Update();
        m_EffectSystem?.Update();
    }

    public bool IsInGameEnd()
    {
        return m_InGameSystem.IsGameEnd;
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

    public float GetSize()
    {
        float res = 0;
        if (m_InGameSystem != null)
        {
            if (m_InGameSystem.GameInfo != null)
            {
                res = m_InGameSystem.GameInfo.GetSize();
            }
        }

        return res;
    }

    //2026/05/26 Updated By Man-Yi, Yeh 
    //2026/06/09 Updated By Man-Yi, Yeh
    //-------------------
    //inGame operate
    //-------------------
    public void InGameColumnOnClick(int id)
    {
        m_InGameSystem?.ColumnOnClick(id);
    }

    public void InGameReversePause()
    {
        m_InGameSystem?.ReversePause();
    }


    //2026/06/09 Updated By Man-Yi, Yeh
    //-------------------
    //inGame
    //-------------------
    public float GetGameTime()
    {
        float res = 0;
        if (m_InGameSystem != null) 
        {
            res = m_InGameSystem.GameTimer;
        }
        return res;
    }

    public void AddGameTime(float time)
    {
        m_InGameSystem?.AddGameTime(time);
    }


    //2026/06/09 Updated By Man-Yi, Yeh
    //-------------------
    //score
    //-------------------
    public int GetScore()
    {
        int res = 0;

        if (m_InGameSystem != null)
        {
            res = m_ScoreSystem.GetScore();
        }
        return res;
    }


    //2026/06/09 Updated By Man-Yi, Yeh
    //-------------------
    //game log
    //-------------------
    public int GetBlockDestroyNum(BlockType type)
    {
        int res = 0;

        res = m_GameLogSystem.GetBlockDestroyNum(type);

        return res;
    }

    public void RecordBlockDestroy(BlockType type)
    {
        Debug.Log("record block destroy: "+ type.ToString());
        m_GameLogSystem?.RecordBlockDestroy(type);
    }


    //2026/06/16 Updated By Man-Yi, Yeh
    //-------------------
    //effect
    //-------------------
    public Effect SetCombineEffect(BlockType type,List<Vector2> pos)
    {
        Effect res = null;

        Debug.Log(
            "set combine effect type: " + type.ToString() + 
            ", qty: " + pos.Count.ToString());
        //res = m_EffectSystem.SetCombineEffect(type, pos);

        return res;
    }

    public Effect SetDestroyEffect(BlockType type, List<Vector2> pos)
    {
        Effect res = null;

        Debug.Log(
            "set destroy effect type: " + type.ToString() +
            ", qty: " + pos.Count.ToString());
        //res = m_EffectSystem.SetDestroyEffect(type, pos);

        return res;
    }


    public void RecordCombineDestroyInfo(BlockType type, int num, Vector2 pos)
    {
        Debug.Log("record combine destroy info: " + type.ToString() + " " + num);
        m_ScoreSystem?.SetDestroyInfo(type, num);
    }


}
