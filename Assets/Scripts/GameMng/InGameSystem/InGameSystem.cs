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
// 2026/06/22 Updated By Man-Yi, Yeh
// 2026/06/23 Updated By Man-Yi, Yeh
// 2026/06/25 Updated By Man-Yi, Yeh
// 2026/06/30 Updated By Man-Yi, Yeh
// 2026/07/03 Updated By Man-Yi, Yeh
// 2026/07/06 Updated By Man-Yi, Yeh
// 2026/07/07 Updated By Man-Yi, Yeh
// 


using UnityEngine;
using UnityEngine.InputSystem;

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

    //-------------------
    //game process controller
    //-------------------
    private GameProcessController m_GameProcessController;

    //-------------------
    //blocks
    //-------------------
    //block factory
    private BlockFactory m_BlockFactory;
    //blocks controller
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
    public bool CanOperate
    {
        set { m_CanOperate = value; }
    }

    //-------------------
    //controller
    //-------------------
    //state
    private InGameStateController m_InGameSystemStateController = new();


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

        //-------------------
        //game process controller
        //-------------------
        m_GameProcessController = new(this);

        //-------------------
        //blocks
        //-------------------
        //block factory
        m_BlockFactory = new(m_GameInfo);
        //blocks controller
        m_BlocksController = new(this, m_GameInfo);
        InitPreviewBlocks();
        //SetNextBlock();

        //combine sets
        m_CombineSetsController = new(
            this,
            m_GameInfo.GetCombineTime(), 
            m_GameInfo.GetCombineSize());

        //-------------------
        //controller
        //-------------------
        m_InGameSystemStateController.SetState(
            new InGameSystemStartState(this, m_InGameSystemStateController));

    }

    public override void Update()
    {
        if (!m_IsPause)
        {
            m_InGameSystemStateController.StateUpdate();
        }

        m_GameMng.SetCanOperate(!m_IsPause && m_IsPlaying && m_CanOperate);

    }

    public InGameSystemStateType GetInGameSystemStateType()
    {
        return m_InGameSystemStateController.GetStateType();
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

    public void OperateControl()
    {
        m_GameProcessController.OperateControl();
    }

    public void TimeControl()
    {
        m_GameProcessController.TimeControl();
    }

    public void EventControl()
    {
        m_GameProcessController.EventControl();
    }

    public bool CheckLevelUp()
    {
        return m_GameProcessController.CheckLevelUp();
    }

    public void LevelUpStart()
    {
        m_GameProcessController.LevelUpStart();
    }

    public bool IsLevelUpEnd()
    {
        return m_GameProcessController.IsLevelUpEnd();
    }

    //-------------------
    //method of play
    //-------------------
    //start play
    public void StartPlay()
    {
        m_IsPlaying = true;
    }

    public bool IsSettingNextBlock()
    {
        return m_BlocksController.IsSettingNextBlock;
    }

    public bool CanRise(int col)
    {
        return m_BlocksController.CanRise(col);
    }

    public void RiseBlock(IBlock block,int col)
    {
        m_BlocksController.RiseBlock(block, col);
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
            m_BlocksController.FallBlock(id);
        }
    }

    public void ReversePause()
    {
        m_IsPause = !m_IsPause;
    }

    public float GetGameTime()
    {
        return m_GameProcessController.GameTimer;
    }

    public void AddGameTime(float time)
    {
        m_GameProcessController.AddGameTime(time);
    }

    public int GetGameLevel()
    {
        return m_GameProcessController.Level;
    }

    public void CallStateTrigger()
    {
        m_InGameSystemStateController.CallTrigger();
    }

    //-------------------
    //method of blocks
    //-------------------
    public IBlock CreateBlock(BlockType type)
    {
        IBlock res = m_BlockFactory.GetBlock(type);

        return res;
    }

    private void InitPreviewBlocks()
    {
        //next
        IBlock nextBlock;
        BlockType nextType;
        nextType = m_GameProcessController.GetNowBlockType();
        nextBlock = CreateBlock(nextType);
        //next next
        IBlock nextNextBlock;
        BlockType nextNextType;
        nextNextType = m_GameProcessController.GetNowBlockType();
        nextNextBlock = CreateBlock(nextNextType);

        if (nextBlock != null && nextNextBlock != null)
        {
            m_BlocksController?.Init(nextBlock, nextNextBlock);
        }
    }

    public void SetPreviewBlocks()
    {
        IBlock block;
        BlockType type;

        type = m_GameProcessController.GetNowBlockType();

        block = CreateBlock(type);
        Debug.Log("type of next next block " + type.ToString());

        m_BlocksController.SetPreviewBlocks(block);
    }

    public void SetNextBlockPath(BlockType type)
    {
        m_GameMng.SetNextBlockPath(type, m_BlockFactory.GetBlockPath(type));
    }

    public bool IsFullBlocks()
    {
        return m_BlocksController.IsFullBlocks();
    }

    public bool IsAllBlocksIdle()
    {
        return m_BlocksController.IsAllBlocksIdle();
    }

    public int GetNumOfBlock()
    {
        return m_BlocksController.GetNumOfBlock();
    }

    //-------------------
    //test
    //-------------------
    private void TestOperate()
    {
        ////test
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    ColumnOnClick(0);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    ColumnOnClick(1);

        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    ColumnOnClick(2);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    ColumnOnClick(3);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    ColumnOnClick(4);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha6))
        //{
        //    ColumnOnClick(5);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha7))
        //{
        //    ColumnOnClick(6);
        //}

        ////game end
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    Debug.Log("Test End Game");

        //    m_IsGameEnd = true;
        //}

        ////pause
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    Debug.Log("Test Pause");

        //    ReversePause();
        //}

        ////rise
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    if (m_BlocksController.CanRise(0))
        //    {
        //        m_BlocksController.RiseBlock(CreateBlock(BlockType.SoftRock), 0);
        //    }
        //}

        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            if (m_BlocksController.CanRise(0))
            {
                m_BlocksController.RiseBlock(CreateBlock(BlockType.SoftRock), 0);
            }
        }
    }
}
