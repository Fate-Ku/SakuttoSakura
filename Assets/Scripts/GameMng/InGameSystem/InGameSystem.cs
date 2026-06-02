//
// InGameSystem.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
// 2026/05/30 Updated By Man-Yi, Yeh
// 2026/05/31 Updated By Man-Yi, Yeh
// 2026/06/02 Updated By Man-Yi, Yeh
// 

using System.Collections.Generic;
using System.Numerics;

using UnityEngine;

public enum BlockType
{
    //flower
    Tsubaki,
    Kaede,
    Himawari,
    Clover,
    Asagao,
    Kikyou,
    Sakura,

    //rock
    Ishi,

    //item
    TimeItem,

    //count
    Count
}

public class InGameSystem : IGameSystem
{
    public InGameSystem(GameMng gameMng) 
        : base(gameMng)
    {
    }

    //-------------------
    //game end
    //-------------------
    private bool m_IsGameEnd;
    public bool IsGameEnd
    {
        get { return m_IsGameEnd; }
    }

    //-------------------
    //Info
    //-------------------
    //game info
    private GameInfo m_GameInfo;
    public GameInfo GameInfo
    {
        get { return m_GameInfo; }
    }
    //GameObject of blocks
    private Dictionary<BlockType, GameObject> m_BlockObs = new();

    //-------------------
    //frame
    //-------------------
    //private IBlock testBlock;
    private Frame m_Frame;

    //-------------------
    //combine sets
    //-------------------



    public override void Init()
    {
        Debug.Log("InGameSystem Init");

        //-------------------
        //game end
        //-------------------
        m_IsGameEnd = false;

        //-------------------
        //Info
        //-------------------
        //game info
        GameObject gameInfo = GameObject.Find("GameInfo");
        if (gameInfo != null)
        {
            m_GameInfo = gameInfo.GetComponent<GameInfo>();
        }
        //GameObject of blocks
        for (int i = 0; i < (int)BlockType.Count; i++)
        {
            bool isAdded = m_BlockObs.TryAdd((BlockType)i, m_GameInfo.GetBlock((BlockType)i));
            if (!isAdded) 
            {
                Debug.Log("TryAdd failed for GameObject:" + ((BlockType)i).ToString());
            }
        }

        //-------------------
        //frame
        //-------------------
        m_Frame = new(m_GameInfo);

    }

    public override void Term()
    {
        Debug.Log("InGameSystem Term");

        m_GameInfo = null;
    }

    public override void Update()
    {
        //Debug.Log("InGameSystem Update");

        //test
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Vector2Int id = new(0, 0);
            IBlock block = CreateBlock((BlockType)1);
            block?.SetPos(m_Frame.GetBlockPos(id));

            AddBlockIntoFrame(id, block);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Vector2Int id = new(GameInfo.GetScale().x - 1, GameInfo.GetScale().y - 1);
            IBlock block = CreateBlock((BlockType)2);
            block?.SetPos(m_Frame.GetBlockPos(id));

            AddBlockIntoFrame(id, block);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T");

            Vector2Int id = new(0, 0);
            m_Frame.Test(id, true);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F");

            Vector2Int id = new(0, 0);
            m_Frame.Test(id, false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Test End Game");

            m_IsGameEnd = true;
        }
    }

    private IBlock CreateBlock(BlockType type)
    {
        IBlock res = null;

        if (m_BlockObs.TryGetValue(type,out var blockOb))
        {
            res = new FlowerBlock(blockOb);
        }
        else
        {
            Debug.Log("BlockOb don't find");
        }
        
        return res;
    }

    private void AddBlockIntoFrame(Vector2Int id, IBlock block)
    {
       m_Frame.AddBlock(id, block);
    }
}
