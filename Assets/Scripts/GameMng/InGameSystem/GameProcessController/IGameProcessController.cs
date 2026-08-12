//
// GameProcessController.cs
// 
// 2026/07/13 Created By Man-Yi, Yeh
// 2026/07/16 Updated By Man-Yi, Yeh
// 2026/07/28 Updated By Man-Yi, Yeh
// 2026/08/12 Updated By Man-Yi, Yeh
// 

using System;
using System.Collections.Generic;
using UnityEngine;

public class IGameProcessController
{
    protected InGameSystem m_InGameSystem;

    protected float m_GameTimer;
    public float GameTimer
    {
        get { return m_GameTimer; }
        set { m_GameTimer = value; }
    }

    protected Dictionary<int, ProcessData> m_ProcessDatas = new();
    protected ProcessData m_NowProcessData;

    protected Dictionary<int, FloorData[]> m_EventDatas = new();
    protected FloorData[] m_NowEventData;

    protected Dictionary<int, BGMData[]> m_BGMDatas = new();

    protected int m_Level;
    public int Level
    {
        get { return m_Level; }
    }
    protected int m_PreLevelSakuraNum;

    protected float m_EventTimer;
    protected bool m_IsInEvent = false;
    protected int m_NowFloor;
    protected IBlock m_TmpBlock;
    
    public IGameProcessController(InGameSystem inGameSystem, 
        string processDataPath, string eventDataPath, string bgmDataPath,
        int startLevel = 1)
    {
        m_InGameSystem = inGameSystem;
        m_GameTimer = m_InGameSystem.GameInfo.GetPlayTime();
        m_Level = startLevel;
        m_PreLevelSakuraNum = 0;
        
        InitProcessDatas(processDataPath);
        InitEventDatas(eventDataPath);
        InitBGMDatas(bgmDataPath);

        m_InGameSystem.GameInfo.nowLevel = m_Level;
    }

    public void AddGameTime(float time)
    {
        m_GameTimer += time;
    }

    public void LevelUpStart()
    {
        m_Level += 1;
        ProcessData data = GetProcessData(m_Level);
        if (data != null)
        {
            m_NowProcessData = data;
        }
        FloorData[] eventData = GetEventData(m_Level);
        if (eventData != null)
        {
            m_NowEventData = eventData;
        }
        BGMData[] bgmDatas = GetBGMData(m_Level);
        if (bgmDatas != null)
        {
            foreach (var bgmData in bgmDatas)
            {
                BGMMng.Instance.SetNextBGM(bgmData.type, bgmData.loop);
            }
        }

        m_GameTimer += m_InGameSystem.GameInfo.GetLevelUpAddGameTime();
        CheckEventStart();

        m_InGameSystem.GameInfo.nowLevel = m_Level;

    }

    public bool IsLevelUpEnd()
    {
        return !m_IsInEvent;
    }

    //-------------------
    //virtual
    //-------------------
    public virtual void OperateControl() 
    {
        m_InGameSystem.CanOperate = !m_InGameSystem.IsSettingNextBlock();
    }

    public virtual BlockType GetNowBlockType() 
    {
        BlockType res = BlockType.None;

        int underBdd = 7 - m_NowProcessData.typeQty;
        if (underBdd < 0)
        {
            underBdd = 0;
        }
        if (underBdd >= 7)
        {
            underBdd = 6;
        }
        int id = UnityEngine.Random.Range(underBdd, 7);
        res = (BlockType)id;

        return res;
    }

    public virtual void TimeControl() 
    {
        m_GameTimer -= Time.deltaTime;
        if (m_GameTimer <= 0)
        {
            m_GameTimer = 0;
        }
    }

    public virtual void EventControl() 
    {
        if (m_IsInEvent) 
        {
            InEventUpdate();
        }
        else
        {
            m_EventTimer += Time.deltaTime;
            if (m_EventTimer >= m_NowProcessData.eventInterval)
            {
                CheckEventStart();
            }
        }
    }

    public virtual bool CheckLevelUp()
    {
        bool res = false;

        if (GameMng.Instance.GetBlockDestroyNum(BlockType.Sakura) - m_PreLevelSakuraNum >=
            m_NowProcessData.levelUpSakuraNum)
        {
            m_PreLevelSakuraNum = GameMng.Instance.GetBlockDestroyNum(BlockType.Sakura);
            res = true;
        }

        return res;
    }

    //-------------------
    //basic
    //-------------------
    protected void InEventUpdate()
    {
        if (m_TmpBlock == null ||
            !m_TmpBlock.IsStateType(BlockStateType.Rise))
        {
            m_NowFloor += 1;
            if (m_NowFloor > m_NowEventData.Length)
            {
                EventEnd();
                Debug.Log("end event");
            }
            else
            {
                MakeNowFloor();
                Debug.Log("make now floor: " + m_NowFloor.ToString());
            }
        }
    }

    protected void CheckEventStart()
    {
        int blockNum = m_InGameSystem.GetNumOfBlock();
        Vector2Int scale = m_InGameSystem.GameInfo.GetScale();

        if (blockNum <= scale.x * scale.y * 1 / 2)
        {
            EventStart();
        }
        else
        {
            EventEnd();
        }
    }

    protected void EventStart()
    {
        m_IsInEvent = true;
        m_NowFloor = 0;
        m_TmpBlock = null;
    }

    protected void EventEnd()
    {
        m_IsInEvent = false;
        m_EventTimer = 0;
    }

    protected void MakeNowFloor()
    {
        int col = m_InGameSystem.GameInfo.GetScale().x;

        //in rise block range
        for (int i = m_NowFloor - 1; i < col - m_NowFloor + 1; ++i)
        {
            //if col can rise
            if (m_InGameSystem.CanRise(i))
            {
                //random create
                float createRate = m_NowEventData[m_NowFloor - 1].createRate;
                if (RandomBool.Value(createRate))
                {
                    //random type
                    Dictionary<BlockType, float> data = new();
                    foreach (var pair in m_NowEventData[m_NowFloor - 1].typeRateDatas)
                    {
                        bool isAdded = data.TryAdd(pair.type, pair.rate);
                        if (!isAdded)
                        {
                            Debug.LogError($"Failed to add type rate for {pair.type}");
                        }
                    }
                    BlockType type = RandomRes<BlockType>.Value(data);

                    //create block and rise
                    m_TmpBlock = m_InGameSystem.CreateBlock(type);
                    m_InGameSystem.RiseBlock(m_TmpBlock, i);
                }
            }
        }
    }

    protected ProcessData GetProcessData(int level)
    {
        ProcessData res = null;

        if (m_ProcessDatas.TryGetValue(level, out var data))
        {
            res = data;
        }

        return res;
    }

    protected FloorData[] GetEventData(int level)
    {
        FloorData[] res = null;
        if (m_EventDatas.TryGetValue(level, out var data))
        {
            res = data;
        }
        return res;
    }

    protected BGMData[] GetBGMData(int level)
    {
        BGMData[] res = null;
        if (m_BGMDatas.TryGetValue(level, out var data))
        {
            res = data;
        }
        return res;
    }

    //-------------------
    //init
    //-------------------
    private void InitProcessDatas(string jsonFilePath)
    {
        TextAsset jsonTextAsset = Resources.Load<TextAsset>(jsonFilePath);
        LevelProcessDataList dataSet =
            JsonUtility.FromJson<LevelProcessDataList>(jsonTextAsset.text);
        foreach (LevelProcessData data in dataSet.list)
        {
            bool isAdded = m_ProcessDatas.TryAdd(data.level, data.data);
            if (!isAdded)
            {
                Debug.LogError($"Failed to add ProcessData for level {data.level}");
            }
        }

        for (int i = m_Level; i >= 1; i -= 1)
        {
            ProcessData data = GetProcessData(i);
            if (data != null)
            {
                m_NowProcessData = data;
                break;
            }
        }
    }

    private void InitEventDatas(string jsonFilePath)
    {
        TextAsset jsonTextAsset = Resources.Load<TextAsset>(jsonFilePath);
        LevelFloorDataList dataSet =
            JsonUtility.FromJson<LevelFloorDataList>(jsonTextAsset.text);
        foreach (LevelFloorData data in dataSet.list)
        {
            bool isAdded = m_EventDatas.TryAdd(data.level, data.datas);
            if (!isAdded)
            {
                Debug.LogError($"Failed to add EventData for level {data.level}");
            }
        }

        for (int i = m_Level; i >= 1; i -= 1)
        {
            FloorData[] data = GetEventData(i);
            if (data != null)
            {
                m_NowEventData = data;
                break;
            }
        }
    }

    private void InitBGMDatas(string jsonFilePath)
    {
        TextAsset jsonTextAsset = Resources.Load<TextAsset>(jsonFilePath);
        NextBGMDataList dataSet =
            JsonUtility.FromJson<NextBGMDataList>(jsonTextAsset.text);
        foreach (NextBGMData data in dataSet.list)
        {
            bool isAdded = m_BGMDatas.TryAdd(data.level, data.datas);
            if (!isAdded)
            {
                Debug.LogError($"Failed to add BGMData for level {data.level}");
            }
        }

        for (int i = m_Level; i >= 1; i -= 1)
        {
            BGMData[] bgmDatas = GetBGMData(i);
            if (bgmDatas != null)
            {
                BGMMng.Instance.SetBGM(bgmDatas[0].type, bgmDatas[0].loop);
                for(int j = 1; j < bgmDatas.Length; ++j)
                {
                    BGMMng.Instance.SetNextBGM(bgmDatas[j].type, bgmDatas[j].loop);
                }
                break;
            }
        }
    }

}
