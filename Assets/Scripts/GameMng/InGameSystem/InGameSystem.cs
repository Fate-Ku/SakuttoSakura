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
// 2026/06/10 Updated By Man-Yi, Yeh
// 

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum BlockType
{
    None = -1,

    //flower
    Tsubaki,
    Kaede,
    Himawari,
    Clover,
    Asagao,
    Kikyou,
    Sakura,

    //rock
    SoftRock,
    HardRock,

    //item
    TimeItem,

    //count
    Count
}

public class InGameSystem : IGameSystem
{
    //-------------------
    //game end
    //-------------------
    private bool m_IsGameEnd = false;
    public bool IsGameEnd
    {
        get { return m_IsGameEnd; }
        set { m_IsGameEnd = value; }
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
    //play
    //-------------------
    private bool m_IsPause = false;
    public bool IsPause
    {
        set { m_IsPause = value; }
    }

    private bool m_IsPlaying = false;
    public bool IsPlaying
    {
        set { m_IsPlaying = value; }
    }

    private bool m_CanOperate = false;
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
    //controller
    //-------------------
    //state
    private InGameStateController m_InGameSystemStateController = new();


    //-------------------
    //test
    //-------------------
    private TextMeshProUGUI m_TestInGameStateText;
    public TextMeshProUGUI TestInGameStateText
    {
        get { return m_TestInGameStateText; }
    }
    private TextMeshProUGUI m_TestTimeText;

    public InGameSystem(GameMng gameMng)
        : base(gameMng)
    { 
    }


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
        //time
        //-------------------
        m_GameTimer = m_GameInfo.GetPlayTime();

        //-------------------
        //controller
        //-------------------
        m_InGameSystemStateController.SetState(
            new InGameSystemStartState(this, m_InGameSystemStateController));


        //-------------------
        //test
        //-------------------
        m_TestInGameStateText = m_GameInfo.GetTestInGameStateText();
        m_TestTimeText = m_GameInfo.GetTestTimeText();

    }

    public override void Update()
    {
        if (!m_IsPause)
        {
            m_InGameSystemStateController.StateUpdate();
        }

        TestOperate();

    }

    //-------------------
    //method of update
    //-------------------
    //game basic update
    public void GameRun()
    {
        //-------------------
        //game basic update
        //-------------------
        //combine update
        m_BlocksController.CombineCheck(m_CombineSetsController);
        m_CombineSetsController.Update();
        //block update
        m_BlocksController.Update();
    }

    public void TimeControl()
    {
        m_GameTimer -= Time.deltaTime;
        if (m_GameTimer <= 0)
        {
            m_GameTimer = 0;
        }

        m_TestTimeText.text = ((int)m_GameTimer).ToString();
    }

    //-------------------
    //method of play
    //-------------------
    //start play
    public void StartPlay()
    {
        m_IsPlaying = true;
        m_CanOperate = true;
    }

    public void OperateControl()
    {
        if (m_IsPause)
        {
            return;
        }
        if (!m_IsPlaying)
        {
            return;
        }

        if (!m_CanOperate)
        {
            m_OperateTimer -= Time.deltaTime;
            if (m_OperateTimer <= 0)
            {
                m_CanOperate = true;
            }
        }
    }

    //-------------------
    //method for call back
    //-------------------
    public void ColumnOnClick(int id)
    {
        if (m_IsPause)
        {
            return;
        }
        if (!m_IsPlaying)
        {
            return;
        }

        if (m_CanOperate)
        {
            SetCantControl();
            m_BlocksController.FallBlock(id);
        }
    }

    public void ReversePause()
    {
        m_IsPause = !m_IsPause;
    }

    public void AddGameTime(float time)
    {
        m_GameTimer += time;
    }

    //-------------------
    //method of blocks
    //-------------------
    public IBlock CreateBlock(BlockType type, bool isCreate = false)
    {
        IBlock res = null;

        if (m_BlockObs.TryGetValue(type, out var blockOb))
        {
            float size = GameInfo.GetSize();

            if ((int)type >= 0 && (int)type < 7)
            {
                res = new FlowerBlock(blockOb, type, size, isCreate);
            }
            else if (type == BlockType.SoftRock)
            {
                res = new SoftRockBlock(blockOb, size, isCreate);
            }
            else if (type == BlockType.HardRock)
            {
                res = new HardRockBlock(this, blockOb, size, isCreate);
            }
            else if (type == BlockType.TimeItem)
            {
                res = new TimeItemBlock(blockOb, size, isCreate);
            }
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
        BlockType type;

        int pattern = Random.Range(0, 10);
        if (pattern == 0)
        {
            type = BlockType.TimeItem;
            Debug.Log("type of next block " + BlockType.TimeItem.ToString());
        }
        else if (pattern == 1)
        {
            type = BlockType.SoftRock;
        }
        else if (pattern == 2)
        {
            type = BlockType.HardRock;
        }
        else
        {
            int qty = m_GameInfo.GetBlockTypeQty();
            int id = Random.Range(7 - qty, 7);
            type = (BlockType)id;
            
        }

        block = CreateBlock(type);
        Debug.Log("type of next block " + type.ToString());

        m_BlocksController.SetNextBlock(block);
    }

    public bool IsFullBlocks()
    {
        return m_BlocksController.IsFullBlocks();
    }

    public bool IsAllBlocksIdle()
    {
        return m_BlocksController.IsAllBlocksIdle();
    }

    //-------------------
    //method of operate
    //-------------------
    private void SetCantControl()
    {
        m_CanOperate = false;
        m_OperateTimer = GameInfo.GetNextOperateTime();
    }


    //-------------------
    //test
    //-------------------
    private void TestOperate()
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

        //pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Test Pause");

            ReversePause();
        }

        //rise
        if (Input.GetKeyDown(KeyCode.R))
        {
            m_BlocksController.RiseBlock(CreateBlock(BlockType.SoftRock), 0);
        }
    }
}
