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
    //blocks
    //-------------------
    private BlockController m_BlockController;

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
        //blocks
        //-------------------
        m_BlockController = new(m_GameInfo);
        SetNextBlock();

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
            m_BlockController.FallBlock(0);
            SetNextBlock();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_BlockController.FallBlock(1);
            SetNextBlock();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_BlockController.FallBlock(GameInfo.GetScale().x - 1);
            SetNextBlock();
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

        if (m_BlockObs.TryGetValue(type, out var blockOb))
        {
            res = new FlowerBlock(blockOb);
        }
        else
        {
            Debug.Log("BlockOb don't find");
        }
        
        return res;
    }

    private void SetNextBlock()
    {
        IBlock block = null;

        int id = Random.Range(0, 7);
        block = CreateBlock((BlockType)id);
        Debug.Log("type of next block " + id.ToString());

        m_BlockController.SetNextBlock(block);

    }
}
