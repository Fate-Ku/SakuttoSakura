//
// GameProcessController.cs
// 
// 2026/06/29 Created By Man-Yi, Yeh
// 2026/06/30 Updated By Man-Yi, Yeh
// 2026/07/06 Updated By Man-Yi, Yeh
// 2026/07/07 Updated By Man-Yi, Yeh
// 

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProcessData
{
    public int levelUpSakuraNum;
    public int typeQty;
    public float eventInterval;
}

[Serializable]
public class LevelProcessData
{
    public int level;
    public ProcessData data;
}

[Serializable]
public class LevelProcessDataList
{
    public LevelProcessData[] list;
}

public class GameProcessController
{
    private InGameSystem m_InGameSystem;

    private float m_GameTimer;
    public float GameTimer
    {
        get { return m_GameTimer; }
        set { m_GameTimer = value; }
    }

    private Dictionary<int, ProcessData> m_ProcessDatas = new();
    private ProcessData m_NowProcessData;

    private int m_Level;
    public int Level
    {
        get { return m_Level; }
    }
    private int m_PreLevelSakuraNum;

    private float m_EventTimer;
    private bool m_IsInEvent = false;
    private int m_NowFloor;
    private IBlock m_TmpBlock;

    public GameProcessController(InGameSystem inGameSystem)
    {
        m_InGameSystem = inGameSystem;
        m_GameTimer = m_InGameSystem.GameInfo.GetPlayTime();
        m_Level = 1;
        m_PreLevelSakuraNum = 0;
        m_InGameSystem.GameInfo.nowLevel = m_Level;

        //-------------------
        //ProcessData
        //-------------------
        string jsonFilePath = "Data/ProcessData";
        TextAsset jsonTextAsset = Resources.Load<TextAsset>(jsonFilePath);
        Debug.Log("ProcessData: " + jsonTextAsset);
        LevelProcessDataList dataSet =
            JsonUtility.FromJson<LevelProcessDataList>(jsonTextAsset.text);

        foreach (LevelProcessData data in dataSet.list)
        {
            bool isAdded = m_ProcessDatas.TryAdd(data.level, data.data);
            if (!isAdded)
            {
                Debug.Log("LevelData TryAdd failed for Level:" + data.level.ToString());
            }

            if (data.level == 1)
            {
                m_NowProcessData = data.data;
            }
        }
        
    }

    public void OperateControl()
    {
        m_InGameSystem.CanOperate = !m_InGameSystem.IsSettingNextBlock();
    }

    public BlockType GetNowBlockType()
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

    public void AddGameTime(float time)
    {
        m_GameTimer += time;
    }

    public void TimeControl()
    {
        m_GameTimer -= Time.deltaTime;
        if (m_GameTimer <= 0)
        {
            m_GameTimer = 0;
        }
    }

    public void EventControl()
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

    public bool CheckLevelUp()
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

    public void LevelUpStart()
    {
        m_Level += 1;
        ProcessData data = GetProcessData(m_Level);
        if (data != null)
        {
            m_NowProcessData = data;
        }
        
        m_GameTimer += m_InGameSystem.GameInfo.GetLevelUpAddGameTime();
        CheckEventStart();

        m_InGameSystem.GameInfo.nowLevel = m_Level;
    
    }

    public bool IsLevelUpEnd()
    {
        return !m_IsInEvent;
    }

    private void InEventUpdate()
    {
        if (m_TmpBlock == null ||
            !m_TmpBlock.IsStateType(BlockStateType.Rise))
        {
            m_NowFloor += 1;
            if (m_NowFloor > m_InGameSystem.GameInfo.GetFloorNum()) 
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

    private void CheckEventStart()
    {
        int blockNum = m_InGameSystem.GetNumOfBlock();
        Vector2Int scale = m_InGameSystem.GameInfo.GetScale();
        
        if (blockNum <= scale.x * scale.y * 1/2)
        {
            EventStart();
        }
        else
        {
            EventEnd();
        }
    }

    private void EventStart()
    {
        m_IsInEvent = true;
        m_NowFloor = 0;
        m_TmpBlock = null;
    }

    private void EventEnd()
    {
        m_IsInEvent = false;
        m_EventTimer = 0;
    }

    private void MakeNowFloor()
    {
        int col = m_InGameSystem.GameInfo.GetScale().x;

        //in rise block range
        for (int i = m_NowFloor - 1; i < col - m_NowFloor + 1; ++i)
        {
            //if col can rise
            if (m_InGameSystem.CanRise(i))
            {
                //random create
                int createRate = UnityEngine.Random.Range(0, 100);
                if (createRate < 120 - (20 * m_NowFloor))
                {
                    //random type
                    int randomType = UnityEngine.Random.Range(0, 5);
                    BlockType type = BlockType.SoftRock;
                    if (randomType == 0)
                    {
                        type = BlockType.HardRock;
                    }
                    else if (randomType == 1)
                    {
                        type = BlockType.TimeItem;
                    }
                    //create block and rise
                    m_TmpBlock = m_InGameSystem.CreateBlock(type);
                    m_InGameSystem.RiseBlock(m_TmpBlock, i);
                }
            } 
        }
    }

    private ProcessData GetProcessData(int level)
    {
        ProcessData res = null;

        if (m_ProcessDatas.TryGetValue(level, out var data))
        {
            res = data;
        }

        return res;
    }
}
