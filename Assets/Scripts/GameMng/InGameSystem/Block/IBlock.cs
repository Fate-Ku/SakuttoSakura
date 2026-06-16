//
// IBlock.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
// 2026/06/02 Updated By Man-Yi, Yeh
// 2026/06/04 Updated By Man-Yi, Yeh
// 2026/06/06 Updated By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
// 2026/06/08 Updated By Man-Yi, Yeh
// 2026/06/10 Updated By Man-Yi, Yeh
// 2026/06/11 Updated By Man-Yi, Yeh
// 2026/06/16 Updated By Man-Yi, Yeh
// 

using UnityEngine;

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
    }

    //size
    private float m_Size;
    public float Size
    {
        get { return m_Size; }
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

    //rise controller
    protected BlockRiseController m_RiseController;
    public BlockRiseController RiseController
    {
        get { return m_RiseController; }
    }

    //fall controller
    protected IBlockFallController m_FallController;
    public IBlockFallController FallController
    {
        get { return m_FallController; }
    }

    //startegys
    protected ICombineStrategy m_CombineStartegy;
    public ICombineStrategy CombineStartegy
    {
        get { return m_CombineStartegy; }
    }

    protected IDestroyStrategy m_DestroyStrategy;
    public IDestroyStrategy DestroyStrategy
    {
        get { return m_DestroyStrategy; }
    }


    protected INearCombineStrategy m_NearCombineStrategy;
    public INearCombineStrategy NearCombineStrategy
    {
        get { return m_NearCombineStrategy; }
    }

    
    public IBlock(
        GameObject block, BlockType type,  float size, 
        bool isCreate = false) 
    {
        m_BlockOb = Object.Instantiate(block);
        m_BlockOb.transform.localScale = new Vector3(size, size, 1);

        m_Type = type;
        m_Size = size;

        m_RiseController = new(this, 2);

        if (isCreate)
        {
            m_BlockStateController.SetState(new BlockCreateState(this, m_BlockStateController));
        }
        else
        {
            m_BlockStateController.SetState(new BlockIdleState(this, m_BlockStateController));
        }
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
        m_BlockStateController.StateUpdate();
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
    public void NearDestroy(IBlock destroyBlock)
    {
        m_BlockStateController.NearDestroy(destroyBlock);
    }

    //be destroyed
    public void BeDestroyed()
    {
        m_BlockStateController.BeDestroyed();
    }


    //-------------------
    //state
    //-------------------
    //go rise
    private void GoRise()
    {
        if (m_BlockStateController.GetStateType() != BlockStateType.Rise)
        {
            m_BlockStateController.SetState(
                new BlockRiseState(this, m_BlockStateController));
        }
    }

    //go combine state
    public void GoCombine()
    {
        if (m_BlockStateController.GetStateType() != BlockStateType.Combine)
        {
            m_BlockStateController.SetState(
                new BlockCombineState(this, m_BlockStateController));
        }
    }

    //go destroy state
    public void GoDestroy()
    {
        if (m_BlockStateController.GetStateType() != BlockStateType.Destroy)
        {
            m_BlockStateController.SetState(
                new BlockDestroyState(this, m_BlockStateController));
        }
    }

    //-------------------
    //rise
    //-------------------
    public void StartRise(Vector2 pos)
    {
        m_RiseController.StartRise(pos);
        GoRise();
    }

    public bool IsGoRise()
    {
        bool res = false;

        if (GetNearNode(BlockNearPos.Below) != null) 
        {
            
        }

        return res;
    }

    //-------------------
    //fall
    //-------------------
    //is go fall
    public bool IsGoFall()
    {
        return m_FallController.IsGoFall();
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
    //combine
    //-------------------
    //end combine
    public void EndCombine()
    {
        m_CombineStartegy.EndCombine(this);
    }

    //-------------------
    //update method
    //-------------------
    public void RemoveCombineSet()
    {
        m_CombineSet?.Remove();
    }

    public void SetCreateBlock(IBlock block)
    {

        m_DestroyStrategy.SetCreateBlock(block);
    }

    public void BlockDestroy()
    {
        //game object destroy
        Object.Destroy(m_BlockOb);

        //remove from node 
        m_BlockNode?.RemoveBlock();
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

    public bool IsStateType(BlockStateType type)
    {
        return m_BlockStateController.GetStateType() == type;
    }

    //-------------------
    //get node
    //-------------------
    public BlockNode GetNearNode(BlockNearPos nearPos)
    {
        return m_BlockNode.GetNearNode(nearPos);
    }

    //-------------------
    //go node
    //-------------------
    public void GoNearNode(BlockNearPos nearPos)
    {
        m_BlockNode.BlockGoNearNode(nearPos);
    }

    //-------------------
    //set active
    //-------------------
    public void SetActive(bool active)
    {
        m_BlockOb.SetActive(active);
    }
}
