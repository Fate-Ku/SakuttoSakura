//
// InGameSystem.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
// 2026/05/30 Updated By Man-Yi, Yeh
// 2026/05/31 Updated By Man-Yi, Yeh
// 

using System.Collections.Generic;
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
    //Infomation
    //-------------------
    //game info
    private GameInfo m_GameInfo;
    public GameInfo GameInfo
    {
        get { return m_GameInfo; }
    }
    //GameObject of blocks
    private Dictionary<BlockType, GameObject> m_Blocks = new();

    //-------------------
    //frame
    //-------------------
    private IBlock testBlock;

    //-------------------
    //combine sets
    //-------------------



    public override void Init()
    {
        Debug.Log("InGameSystem Init");
        
        GameObject gameInfo = GameObject.Find("GameInfo");
        if (gameInfo != null)
        {
            m_GameInfo = gameInfo.GetComponent<GameInfo>();
        }

        for (int i = 0; i < (int)BlockType.Count; i++)
        {
            bool isAdded = m_Blocks.TryAdd((BlockType)i, m_GameInfo.GetBlock((BlockType)i));
            if (!isAdded) 
            {
                Debug.Log("TryAdd failed for GameObject:" + ((BlockType)i).ToString());
            }
        }

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
            testBlock?.TestDestroy();
            testBlock = CreateBlock((BlockType)1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            testBlock?.TestDestroy();
            testBlock = CreateBlock((BlockType)2);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T");
            testBlock?.Test(true);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F");
            testBlock?.Test(false);
        }
    }

    private IBlock CreateBlock(BlockType type)
    {
        IBlock res = null;

        res = new FlowerBlock(m_Blocks[type]);

        return res;
    }
}
