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
// 2026/06/24 Updated By Fate Ku
// 2026/06/25 Updated By Fate Ku
// 2026/06/30 Updated By Man-Yi, Yeh
// 2026/07/02 Updated By Fate Ku
// 2026/07/03 Updated By Man-Yi, Yeh
// 2026/07/06 Updated By Fate Ku
// 2026/07/07 Updated By Man-Yi, Yeh
// 2026/07/07 Updated By Fate Ku
// 2026/07/09 Updated By Man-Yi, Yeh
// 2026/07/10 Updated By Fate Ku
// 2026/07/11 Updated By Fate Ku
// 2026/07/12 Updated By Fate Ku
// 2026/07/13 Updated By Fate Ku
// 2026/07/13 Updated By Man-Yi, Yeh
// 2026/07/17 Updated By Fate Ku
// 

using System.Collections.Generic;
using UnityEngine;

public class GameMng
{
    public enum PhaseType
    {
        SkillSelect,
        InGame,
        Score,
        Tutorial,
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

    // 2026/06/25 Updated By Fate Ku
    //effect info
    private EffectInfo m_EffectInfo;
    
    public EffectInfo EffectInfo
    {
        get { return m_EffectInfo; }
    }

    //score info
    private ScoreInfo m_ScoreInfo;

    public ScoreInfo ScoreInfo
    {
        get { return m_ScoreInfo; }
    }

    //blockPos info
    private BlockPosInfo m_BlockPosInfo;

    public BlockPosInfo BlockPosInfo
    {
        get { return m_BlockPosInfo; }
    }

    //-------------------
    //game system
    //-------------------
    //in game system
    private InGameSystem m_InGameSystem;

    //skill data
    private SkillDataSystem m_SkillDataSystem;

    //score system
    private ScoreSystem m_ScoreSystem;
    public ScoreSystem ScoreSystem => m_ScoreSystem;

    //game log system
    private GameLogSystem m_GameLogSystem;

    //effect system
    private EffectSystem m_EffectSystem;

    //UI game state
    private InGameUIState m_UIState;

    //UI game background
    private InGameUIBackground m_Background;

    // UI touch button
    private InGameUIButton m_ButtonSystem;

    public void Init()
    {
        m_SkillDataSystem = new SkillDataSystem(this);

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

            case PhaseType.Tutorial:
                phase = new TutorialPhase(this);
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
    //2026/06/30 Updated By Man-Yi, Yeh
    //-------------------
    //method of InGameSystem
    //-------------------
    public void InGameInit(InGameType inGameType = InGameType.Classic)
    {
        GameObject scoreInfo = GameObject.Find("ScoreInfo");
        if (scoreInfo != null)
        {
            m_ScoreInfo = scoreInfo.GetComponent<ScoreInfo>();
        }

        //renew
        m_UIState = new InGameUIState(m_ScoreInfo.GetInGameStateText());
        m_InGameSystem = new InGameSystem(this, inGameType);
        m_Background = new InGameUIBackground();


        // 2026/07/16 Updated By Fate Ku
        // block info . path preview
        GameObject blockPosInfo = GameObject.Find("BlockPosInfo");

        if (blockPosInfo != null)
        {
            m_BlockPosInfo = blockPosInfo.GetComponent<BlockPosInfo>();
        }

        Dictionary<BlockType, Material> prevMats = new Dictionary<BlockType, Material>();
        prevMats[BlockType.Kaede] = m_BlockPosInfo.GetMatKaede();
        prevMats[BlockType.Himawari] = m_BlockPosInfo.GetMatHimawari();
        prevMats[BlockType.Clover] = m_BlockPosInfo.GetMatClover();
        prevMats[BlockType.Asagao] = m_BlockPosInfo.GetMatAsagao();
        prevMats[BlockType.Kikyou] = m_BlockPosInfo.GetMatKikyou();
        prevMats[BlockType.Sakura] = m_BlockPosInfo.GetMatSakura();

        m_ButtonSystem = new InGameUIButton(prevMats);
        // 2026/07/16 Updated By Fate Ku

        m_ScoreSystem = new ScoreSystem(this);
        m_GameLogSystem = new GameLogSystem(this);


        // 2026/06/25 Updated By Fate Ku
        GameObject effectInfo = GameObject.Find("EffectInfo");

        if (effectInfo != null)
        {
            m_EffectInfo = effectInfo.GetComponent<EffectInfo>();
        }

        Dictionary<BlockType, Material> mats = new Dictionary<BlockType, Material>();
        mats[BlockType.Kaede] = m_EffectInfo.GetMatKaede();
        mats[BlockType.Himawari] = m_EffectInfo.GetMatHimawari();
        mats[BlockType.Clover] = m_EffectInfo.GetMatClover();
        mats[BlockType.Asagao] = m_EffectInfo.GetMatAsagao();
        mats[BlockType.Kikyou] = m_EffectInfo.GetMatKikyou();
        mats[BlockType.Sakura] = m_EffectInfo.GetMatSakura();
        mats[BlockType.None] = m_EffectInfo.GetMatTsubaki();
        
        m_EffectSystem = new EffectSystem(this, m_EffectInfo.GetEffectPrefab(), mats,
            m_EffectInfo.GetSakuraImagePrefab(),m_EffectInfo.GetSakuraTarget(),
            m_EffectInfo.GetSakuraFlyPrefab());
        // 2026/06/25 Updated By Fate Ku

        //init
        m_UIState?.Init();
        m_InGameSystem?.Init();
        m_Background.Init();
        m_ButtonSystem.Init();
        m_ScoreSystem?.Init();
        m_GameLogSystem?.Init();
        m_EffectSystem?.Init();
    }

    public void InGameTerm()
    {
        m_UIState?.Term();
        m_InGameSystem?.Term();
        m_Background?.Term();
        m_ButtonSystem?.Term();
        m_ScoreSystem?.Term();
        m_GameLogSystem?.Term();
        m_EffectSystem?.Term();

        m_InGameSystem = null;
    }

    public void InGameUpdate()
    {
        m_UIState?.Update();
        m_InGameSystem?.Update();
        m_ButtonSystem?.Update();
        m_ScoreSystem?.Update();
        m_GameLogSystem?.Update();
        m_EffectSystem?.Update();
    }

    public bool IsInGameEnd()
    {
        bool res = false;

        if (m_InGameSystem != null)
        {
            res = m_InGameSystem.IsGameEnd;
        }

        return res;
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
    //2026/06/25 Updated By Man-Yi, Yeh
    //2026/06/29 Updated By Man-Yi, Yeh
    //2026/06/30 Updated By Man-Yi, Yeh
    //-------------------
    //inGame
    //-------------------
    public InGameSystemStateType GetInGameSystemStateType()
    {
        InGameSystemStateType res = InGameSystemStateType.None;

        if (m_InGameSystem != null)
        {
            res = m_InGameSystem.GetInGameSystemStateType();
        }

        return res;
    }

    public float GetGameTime()
    {
        float res = 0;
        if (m_InGameSystem != null)
        {
            res = m_InGameSystem.GetGameTime();
        }
        return res;
    }

    public void AddGameTime(float time)
    {
        m_InGameSystem?.AddGameTime(time);
    }

    public int GetGameLevel()
    {
        int res = 0;

        if (m_InGameSystem != null)
        {
            res = m_InGameSystem.GetGameLevel();
        }        

        return res;
    }
    public void CallInGameSystemStateTrigger()
    {
        m_InGameSystem?.CallStateTrigger();
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

    //2026/06/23 Updated By Fate Ku
    //-------------------
    //combo
    //-------------------
    public int GetTotalCombo() //now
    {
        int res = 0;

        if (m_InGameSystem != null)
        {
            res = m_ScoreSystem.GetNowCombo();
        }
        return res;

    }
    //max bombo
    public int GetMaxCombo()
    {
        int res = 0;

        if (m_InGameSystem != null)
        {
            res = m_ScoreSystem.GetMaxCombo();
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
        Debug.Log("record block destroy: " + type.ToString());
        m_GameLogSystem?.RecordBlockDestroy(type);
    }


    //2026/06/16 Updated By Man-Yi, Yeh
    //2026/07/07 Updated By Man-Yi, Yeh
    //-------------------
    //effect
    //-------------------

    public int SetCombineEffect(BlockType type,List<Vector2> pos)
    {
        int id = -1;

        Debug.Log(
            "set combine effect type: " + type.ToString() +
            ", qty: " + pos.Count.ToString());
        id = m_EffectSystem.SetCombineEffect(type, pos);

        return id;
    }

    public void OffCombineEffect(int effectID)
    {
        m_EffectSystem?.OffCombineEffect(effectID);
    }

    public int SetDestroyEffect(BlockType type, Vector2Int blockID)
    {
        int res = -1;

        Debug.Log(
            "set destroy effect type: " + type.ToString());
        res = m_EffectSystem.SetDestroyEffect(type, blockID);

        return res;
    }

    public void OffDestroyEffect(int effectID)
    {
        m_EffectSystem?.OffDestroyEffect(effectID);
    }

    public void RecordCombineDestroyInfo(BlockType type, int num, Vector2Int blockID)
    {
        Debug.Log("record combine destroy info: " + type.ToString() + " " + num);

        m_ScoreSystem?.SetDestroyInfo(type, num, blockID);
    }

    // effect
    public EffectSystem GetEffectSystem()
    {
        return m_EffectSystem;
    }

    //2026/07/03 Updated By Man-Yi, Yeh
    //-------------------
    //UI
    //-------------------
    public void ShowStateUI(InGameSystemStateType type)
    {
        m_UIState.ShowStateUI(type);
    }

    public void EndStateUI(InGameSystemStateType type)
    {
        m_UIState.EndStateUI(type);
    }

    //2026/07/07 Updated By Man-Yi, Yeh
    //-------------------
    //path effect
    //-------------------
    public void SetNextBlockPath(BlockType type, List<FallDirection> path)
    {
        m_ButtonSystem.SetNextBlockPath(type, path);
    }

    public void SetCanOperate(bool canOperate)
    {
        m_ButtonSystem.SetCanOperate(canOperate);

    }

    //2026/07/11 Updated By Fate Ku
    //-------------------
    //get bg position for sakura fly use
    //-------------------
    public Vector3 GetBgVirtualCubePosition(int row, int col)
    {
        return m_Background.GetBgVirtualCubePosition(row, col);
    }

    //2026/07/13 Updated By Fate Ku
    //-------------------
    //get real bg position 
    //-------------------
    public Vector3 GetBgCubePosition(int row, int col)
    {
        return m_Background.GetBgCubePosition(row, col);
    }

    //2026/07/16 Updated By Fate Ku
    //-------------------
    //can operate
    //-------------------
    public bool GetCanOperate()
    {
        return m_InGameSystem.GetCanOperate();
    }

    //2026/07/17 Updated By Fate Ku
    public int GetMaxLevel()
    {
        return m_UIState.GetMaxLevel();
    }


}
