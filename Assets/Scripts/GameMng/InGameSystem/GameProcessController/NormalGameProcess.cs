//
// GameProcessController.cs
// 
// 2026/06/29 Created By Man-Yi, Yeh
// 2026/06/30 Updated By Man-Yi, Yeh
// 2026/07/06 Updated By Man-Yi, Yeh
// 2026/07/07 Updated By Man-Yi, Yeh
// 2026/07/13 Updated By Man-Yi, Yeh
// 

using System;
using System.Collections.Generic;
using UnityEngine;

public class NormalGameProcess : IGameProcessController
{
    public NormalGameProcess(InGameSystem inGameSystem)
        : base(inGameSystem, "Data/ProcessData")
    {
    }

    public override void OperateControl()
    {
        m_InGameSystem.CanOperate = !m_InGameSystem.IsSettingNextBlock();
    }

    public override BlockType GetNowBlockType()
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

    public override void TimeControl()
    {
        m_GameTimer -= Time.deltaTime;
        if (m_GameTimer <= 0)
        {
            m_GameTimer = 0;
        }
    }

    public override void EventControl()
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

    public override bool CheckLevelUp()
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
}
