//
// IBlock.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
// 2026/06/02 Updated By Man-Yi, Yeh
// 2026/06/04 Updated By Man-Yi, Yeh
// 2026/06/06 Updated By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
// 

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public enum BlockNearPos
{
    Above,
    Below,
    Left,
    Right,

    Count
}

public abstract class IBlock
{
    //-------------------
    //game object
    //-------------------
    private GameObject m_BlockOb;
    public GameObject BlockOb
    {
        get { return m_BlockOb; }
    }


    //-------------------
    //oner
    //-------------------
    //oner block node 
    private BlockNode m_BlockNode;
    public BlockNode BlockNode
    {
        get { return m_BlockNode; }
        set { m_BlockNode = value; }
    }

    //oner combine set
    private CombineSet m_CombineSet = null;
    public CombineSet CombineSet
    {
        get {  return m_CombineSet; }
        set { m_CombineSet = value; }
    }

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
    protected NormalCombineCheck m_CombineCheckStartegy;
    public NormalCombineCheck CombineCheckStartegy
    {
        get { return m_CombineCheckStartegy; }
    }

    protected IDestroyStrategy m_DestroyStrategy;
    public IDestroyStrategy DestroyStrategy
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

    //do combine check
    public void DoCombineCheck(CombineSetsController controller)
    {
        m_BlockStateController.DoCombineCheck(controller);
    }

    //be combined check
    public void BeCombinedCheck(IBlock block, CombineSetsController controller)
    {
        m_BlockStateController.BeCombinedCheck(block, controller);
    }

    //near destroy
    public void NearDestroy()
    {
        m_BlockStateController.NearDestroy();
    }

    //be destroyed
    public void BeDestroyed()
    {
        m_BlockStateController.BeDestroyed();
    }


    //-------------------
    //change state
    //-------------------
    //go combine state
    public void GoCombine()
    {
        if (m_BlockStateController.GetStateName() != "BlockCombineState")
        {
            m_BlockStateController.SetState(
                new BlockCombineState(this, m_BlockStateController));
        }
    }

    //go destroy state
    public void GoDestroy()
    {
        if (m_BlockStateController.GetStateName() != "BlockDestroyState")
        {
            m_BlockStateController.SetState(
                new BlockDestroyState(this, m_BlockStateController));
        }
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
    public void BlockDestroy()
    {
        //game object destroy
        Object.Destroy(m_BlockOb);

        //remove from node 
        m_BlockNode?.RemoveBlock();
    }

    public void SetCreateBlock(IBlock block)
    {

        m_DestroyStrategy.SetCreateBlock(block);
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


    //-------------------
    //get node
    //-------------------
    public BlockNode GetNearNode(BlockNearPos pos)
    {
        BlockNode blockNode = null;

        switch (pos) 
        {
            case BlockNearPos.Above:
                blockNode = m_BlockNode.GetAboveNode();
                break;

            case BlockNearPos.Below:
                blockNode = m_BlockNode.GetBelowNode();
                break;

            case BlockNearPos.Left:
                blockNode = m_BlockNode.GetLeftNode();
                break;

            case BlockNearPos.Right:
                blockNode = m_BlockNode.GetRightNode();
                break;

            default:
                break;
        }

        return blockNode;
    }

    //-------------------
    //go node
    //-------------------
    public void GoBelowNode()
    {
        m_BlockNode.BlockGoBelowNode();
    }

    //-------------------
    //set active
    //-------------------
    public void SetActive(bool active)
    {
        m_BlockOb.SetActive(active);
    }
}
