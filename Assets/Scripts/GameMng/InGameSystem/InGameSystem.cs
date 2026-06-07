//
// InGameSystem.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
// 2026/05/30 Updated By Man-Yi, Yeh
// 2026/05/31 Updated By Man-Yi, Yeh
// 2026/06/02 Updated By Man-Yi, Yeh
// 2026/06/06 Updated By Man-Yi, Yeh
// 

using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

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
    //blocks
    private BlocksController m_BlocksController;
    //combine sets
    private CombineSetsController m_CombineSetsController;

    //-------------------
    //operate
    //-------------------
    private bool m_CanOperate;
    private float m_OperateTimer;


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
        //blocks
        m_BlocksController = new(m_GameInfo);
        SetNextBlock();
        //combine sets
        m_CombineSetsController = new(
            m_GameInfo.GetCombineTime(), 
            m_GameInfo.GetCombineSize());

        //-------------------
        //operate
        //-------------------
        m_CanOperate = true;

    }

    public override void Term()
    {
        Debug.Log("InGameSystem Term");

        m_GameInfo = null;
    }

    public override void Update()
    {
        ControlOperate();

        m_BlocksController.Update();
        m_BlocksController.CombineCheck(m_CombineSetsController);

        m_CombineSetsController.Update();



        TestOprate();
    }

    //-------------------
    //column button callBack
    //-------------------
    public void ColumnOnClick(int id)
    {
        if (m_CanOperate)
        {
            SetCantControl();
            if (m_BlocksController.FallBlock(id))
            {
                SetNextBlock();
            }
        }
    }

    //-------------------
    //method of blocks
    //-------------------
    private IBlock CreateBlock(BlockType type)
    {
        IBlock res = null;

        if (m_BlockObs.TryGetValue(type, out var blockOb))
        {
            float size = GameInfo.GetSize();
            res = new FlowerBlock(blockOb, size)
            {
                Type = type
            };
        }
        else
        {
            Debug.Log("BlockOb don't find");
        }
        
        return res;
    }

    private void SetNextBlock()
    {
        IBlock block;

        int id = Random.Range(4, 7);
        block = CreateBlock((BlockType)id);
        Debug.Log("type of next block " + id.ToString());

        m_BlocksController.SetNextBlock(block);
    }

    //-------------------
    //method of operate
    //-------------------
    private void ControlOperate()
    {
        if (!m_CanOperate)
        {
            m_OperateTimer -= Time.deltaTime;
            if (m_OperateTimer <= 0)
            {
                m_CanOperate = true;
            }
        }
    }

    private void SetCantControl()
    {
        m_CanOperate = false;
        m_OperateTimer = GameInfo.GetNextOperateTime();
    }

    //-------------------
    //test
    //-------------------
    private void TestOprate()
    {
        //test
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ColumnOnClick(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ColumnOnClick(1);

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ColumnOnClick(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ColumnOnClick(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ColumnOnClick(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ColumnOnClick(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ColumnOnClick(6);
        }

        //game end
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Test End Game");

            m_IsGameEnd = true;
        }
    }
}
