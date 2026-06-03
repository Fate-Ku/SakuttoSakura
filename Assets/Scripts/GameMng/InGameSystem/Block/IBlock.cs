//
// IBlock.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
// 2026/06/02 Updated By Man-Yi, Yeh
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
    private BlockNode m_OnerNode;
    public BlockNode OnerNode
    {
        set { m_OnerNode = value; }
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

    //-------------------
    //controller
    //-------------------
    //state
    private BlockStateController m_BlockStateController = new();
    //startegys
    private IBlockStrategy m_CombineCheckStartegy;
    private IBlockStrategy m_NearDestroyStrategy;
    private IBlockStrategy m_DestroyStrategy;

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

    //update
    public void Update()
    {
        m_BlockStateController.BlockUpdate();
    }

    //check is go fall
    public void GoFallCheck()
    {
        m_BlockStateController.GoFallCheck();
    }

    //is falling
    public bool IsFalling()
    {
        return m_BlockStateController.IsFalling();
    }

    //fall info
    //public FallInfo GetFallInfo()

    //check combine
    public void CombineCheck()
    {
        m_BlockStateController.CombineCheck();
    }

    //check is go destroy
    public void GoDestroyCheck()
    {
        m_BlockStateController.GoDestroyCheck();
    }

    //near destroy
    public void NearDestroy()
    {
        m_BlockStateController.NearDestroy();
    }


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

    public void Test(bool active)
    {
        m_BlockOb.SetActive(active);
    }

    public void BlockDestroy()
    {
        Object.Destroy(m_BlockOb);
    }

   
}
