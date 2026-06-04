//
// IBlock.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
// 2026/06/02 Updated By Man-Yi, Yeh
// 2026/06/04 Updated By Man-Yi, Yeh
// 

using Unity.VisualScripting;
using UnityEngine;

public abstract class IBlock
{
    //-------------------
    //game object
    //-------------------
    private GameObject m_BlockOb;


    //-------------------
    //oner
    //-------------------
    private BlockNode m_BlockNode;
    public BlockNode BlockNode
    {
        set { m_BlockNode = value; }
    }

    //oner combine set

    //-------------------
    //info
    //-------------------
    //type
    private BlockType m_Type;
    public BlockType Type
    {
        get { return m_Type; }
        set { m_Type = value; }
    }

    //is idle
    private bool m_IsIdle = false;
    public bool IsIdle
    {
        get { return m_IsIdle; }
        set {  m_IsIdle = value; }
    }

    //pos
    private Vector2 m_Pos;
    public Vector2 Pos
    {
        get { return m_Pos; }
    }

    //-------------------
    //controller
    //-------------------
    //state
    private BlockStateController m_BlockStateController = new();

    //fall controller
    protected IBlockFallController m_FallController;
    public IBlockFallController FallController
    {
        get { return m_FallController; }
    }

    //startegys
    protected IBlockStrategy m_CombineCheckStartegy;
    public IBlockStrategy CombineCheckStartegy
    {
        get { return m_CombineCheckStartegy; }
    }

    protected IBlockStrategy m_DestroyStrategy;
    public IBlockStrategy DestroyStrategy
    {
        get { return m_DestroyStrategy; }
    }


    protected IBlockStrategy m_NearDestroyStrategy;
    public IBlockStrategy NearDestroyStrategy
    {
        get { return m_NearDestroyStrategy; }
    }

    
    public IBlock(GameObject block, float size) 
    {
        m_BlockOb = Object.Instantiate(block);
        m_BlockOb.transform.localScale = new Vector3(size, size, 1);

        m_BlockStateController.SetState(new BlockIdleState(this, m_BlockStateController));
    }
    ~IBlock()
    {
        BlockDestroy();
    }

    //-------------------
    //update
    //-------------------
    //update
    public void Update()
    {
        m_BlockStateController.BlockUpdate();
    }

    //check combine
    public void CombineCheck()
    {
        m_BlockStateController.CombineCheck();
    }

    //check is go destroy
    public void DestroyCheck()
    {
        m_BlockStateController.DestroyCheck();
    }

    //near destroy
    public void NearDestroy()
    {
        m_BlockStateController.NearDestroy();
    }

    //-------------------
    //fall
    //-------------------
    //is go fall
    public bool IsGoFall()
    {
        return m_FallController.IsGoFall();
    }

    //is falling
    public bool IsFalling()
    {
        return m_FallController.IsFalling();
    }

    //fall info
    public FallInfo GetFallInfo()
    {
        return m_FallController.FallInfo;
    }

    //set fall target pos
    public void SetFallTargetPos(Vector2 targetPos)
    {
        m_FallController.SetTargetPos(targetPos);
    }

    //set fall speed
    public void SetSpeed(float speed)
    {
        m_FallController.SetSpeed(speed);
    }

    //-------------------
    //basic method
    //-------------------
    public void SetPos(Vector2 pos)
    {
        m_Pos = pos;
        if (m_BlockOb != null) 
        {
            m_BlockOb.transform.localPosition =
                new Vector3(
                    pos.x,
                    pos.y,
                    m_BlockOb.transform.localPosition.z);
        }
    }

    public void BlockDestroy()
    {
        Object.Destroy(m_BlockOb);
    }

    //-------------------
    //basic method of node
    //-------------------
    public BlockNode GetUnderNode()
    {
        return m_BlockNode.GetUnderNode();
    }

    public void GoUnderNode()
    {
        m_BlockNode.BlockGoUnderNode();
    }

    //-------------------
    //test
    //-------------------
    public void Test(bool active)
    {
        m_BlockOb.SetActive(active);
    }
}
