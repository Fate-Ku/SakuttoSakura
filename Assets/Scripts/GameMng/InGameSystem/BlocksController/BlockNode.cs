//
// BlockNode.cs
// 
// 2026/06/02 Created By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
// 2026/06/08 Updated By Man-Yi, Yeh
// 2026/06/10 Updated By Man-Yi, Yeh
// 2026/06/18 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public enum BlockNodeState
{
    Empty,
    Occupied,
    VerticalMoving,
    HorizontalMoving
}

public class BlockNode
{
    private IBlock m_Block = null;
    public IBlock Block
    {
        get { return m_Block; }
    }

    private BlockNodeState m_State = BlockNodeState.Empty;
    public BlockNodeState State
    {
        set { m_State = value; }
    }

    //fixed info
    private BlocksController m_Controller;  //oner
    private Vector2Int m_ID;
    private Vector2 m_Pos;
    public Vector2 Pos
    {
        get { return m_Pos; }
    }

    public BlockNode(BlocksController controller, Vector2Int id, Vector2 pos)
    {
        m_Controller = controller;
        m_ID = id;
        m_Pos = pos;
    }

    //-------------------
    //game
    //-------------------
    public void EndRise()
    {
        m_Controller.EndRise(m_ID);
    }

    public void StartFall()
    {
        m_Controller.StartFall(m_ID);
    }

    //-------------------
    //basic
    //-------------------
    public bool IsEmpty()
    {
        return m_Block == null;
    }

    public bool CanVerticalMoveTo()
    {
        bool res;
        if (m_Block == null) 
        {
            res = m_State == BlockNodeState.Empty ||
                  m_State == BlockNodeState.VerticalMoving;
        }
        else
        {
            res = m_Block.IsStateType(BlockStateType.Fall) ||
                  m_Block.IsStateType(BlockStateType.Rise);
        }
       
        return res;
    }


public bool CanHorizontalMoveTo()
    {
        bool res =
            (m_State == BlockNodeState.Empty ||
             m_State == BlockNodeState.HorizontalMoving);
        return res;
    }

    public void SetBlock(IBlock block)
    {
        m_Block = block;
        m_Block.BlockNode = this;
        m_State = BlockNodeState.Occupied;
    }

    public void RemoveBlock()
    {
        m_Block.BlockNode = null;
        m_Block = null;
        m_State = BlockNodeState.Empty;
    }

    public void BlockChangeNode(Vector2Int id)
    {
        if (m_Controller.IsNodeEmpty(id))
        {
            m_Controller.GetNode(id).SetBlock(m_Block);
            m_Block = null;
            m_State = BlockNodeState.Empty;
        }
    }

    //-------------------
    //get node
    //-------------------
    public BlockNode GetNearNode(BlockNearPos nearPos)
    {
        return m_Controller.GetNearNode(nearPos, m_ID);
    }

    //-------------------
    //go node
    //-------------------
    public void BlockGoNearNode(BlockNearPos nearPos)
    {
        Vector2Int id = new(m_ID.x, m_ID.y);

        switch (nearPos)
        {
            case BlockNearPos.Above:
                id += new Vector2Int(0, -1);
                break;

            case BlockNearPos.Below:
                id += new Vector2Int(0, 1);
                break;

            case BlockNearPos.Left:
                id += new Vector2Int(-1, 0);
                break;

            case BlockNearPos.Right:
                id += new Vector2Int(1, 0);
                break;

            default:
                break;
        }
        Debug.Log("test near id: " + id.ToString());

        BlockChangeNode(id);
    }

    


}
