//
// InGameSystem.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
// 2026/05/30 Updated By Man-Yi, Yeh
// 2026/05/31 Updated By Man-Yi, Yeh
// 2026/06/02 Updated By Man-Yi, Yeh
// 2026/06/06 Updated By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
// 2026/06/08 Updated By Man-Yi, Yeh
// 2026/06/09 Updated By Man-Yi, Yeh
// 

using System.Collections.Generic;
using TMPro;
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
    //blocks
    private BlocksController m_BlocksController;
    //combine sets
    private CombineSetsController m_CombineSetsController;

    //-------------------
    //operate
    //-------------------
    private bool m_CanOperate;
    private float m_OperateTimer;

    //-------------------
    //time
    //-------------------
    private float m_GameTimer;
    public float GameTimer
    {
        get { return m_GameTimer; }
    }

    //-------------------
    //test
    //-------------------
    private TextMeshProUGUI m_TestInGameStateText;
    private TextMeshProUGUI m_TestTimeText;


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
        m_BlocksController = new(this, m_GameInfo);
        SetNextBlock();
        //combine sets
        m_CombineSetsController = new(
            this,
            m_GameInfo.GetCombineTime(), 
            m_GameInfo.GetCombineSize());

        //-------------------
        //operate
        //-------------------
        m_CanOperate = true;

        //-------------------
        //time
        //-------------------
        m_GameTimer = m_GameInfo.GetGameTime();

        //-------------------
        //test
        //-------------------
        m_TestInGameStateText = m_GameInfo.GetTestInGameStateText();
        m_TestTimeText = m_GameInfo.GetTestTimeText();

    }

    public override void Update()
    {
        ControlOperate();

        //combine update
        m_CombineSetsController.Update();
        m_BlocksController.CombineCheck(m_CombineSetsController);
        //block update
        m_BlocksController.Update();
        
        TestOprate();

        //game end check
        TimeControl();
        BlockFullCheck();
    }

    //-------------------
    //column button callBack
    //-------------------
    public void ColumnOnClick(int id)
    {
        if (m_CanOperate)
        {
            SetCantControl();
            m_BlocksController.FallBlock(id);
        }
    }

    //-------------------
    //method of blocks
    //-------------------
    public IBlock CreateBlock(BlockType type)
    {
        IBlock res = null;

        if (m_BlockObs.TryGetValue(type, out var blockOb))
        {
            float size = GameInfo.GetSize();
            res = new FlowerBlock(type, blockOb, size);
        }
        else
        {
            Debug.Log("BlockOb don't find");
        }
        
        return res;
    }

    public void SetNextBlock()
    {
        IBlock block;

        int qty = m_GameInfo.GetBlockTypeQty();
        int id = Random.Range(7 - qty, 7);
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
    //method of game end
    //-------------------
    private void TimeControl()
    {
        m_GameTimer -= Time.deltaTime;
        if (m_GameTimer <= 0)
        {
            m_GameTimer = 0;
            m_IsGameEnd = true;
        }

        m_TestTimeText.text = ((int)m_GameTimer).ToString();
    }

    private void BlockFullCheck()
    {
        if (m_BlocksController.IsFull())
        {
            m_IsGameEnd = true;
        }
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
