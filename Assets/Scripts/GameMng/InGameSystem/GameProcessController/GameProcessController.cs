//
// GameProcessController.cs
// 
// 2026/06/29 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class GameProcessController
{
    private InGameSystem m_InGameSystem;

    private float m_GameTimer;
    public float GameTimer
    {
        get { return m_GameTimer; }
        set { m_GameTimer = value; }
    }

    private float m_EventTimer;
    private bool m_IsInEvent = false;
    private int m_NowFloor;
    private IBlock m_TmpBlock;

    private int m_Level;
    private int m_PreLevelSakuraNum;

    public GameProcessController(InGameSystem inGameSystem)
    {
        m_InGameSystem = inGameSystem;
        m_GameTimer = m_InGameSystem.GameInfo.GetPlayTime();
        m_Level = 1;
        m_PreLevelSakuraNum = 0;
        m_InGameSystem.GameInfo.nowLevel = m_Level;
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
            if (m_EventTimer >= m_InGameSystem.GameInfo.GetEventInterval())
            {
                CheckEventStart();
            }
        }
    }

    public bool CheckLevelUp()
    {
        bool res = false;

        if (GameMng.Instance.GetBlockDestroyNum(BlockType.Sakura) - m_PreLevelSakuraNum >=
            m_InGameSystem.GameInfo.GetLevelUpSakuraNum())
        {
            m_PreLevelSakuraNum = GameMng.Instance.GetBlockDestroyNum(BlockType.Sakura);
            res = true;
        }

        return res;
    }

    public void LevelUpStart()
    {
        m_Level += 1;
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
                int createRate = Random.Range(0, 100);
                if (createRate < 120 - (20 * m_NowFloor))
                {
                    //random type
                    int randomType = Random.Range(0, 5);
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
}
