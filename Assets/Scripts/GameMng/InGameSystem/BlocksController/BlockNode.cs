//
// BlockNode.cs
// 
// 2026/06/02 Created By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
// 2026/06/08 Updated By Man-Yi, Yeh
// 2026/06/10 Updated By Man-Yi, Yeh
// 2026/06/18 Updated By Man-Yi, Yeh
// 2026/06/22 Updated By Man-Yi, Yeh
// 2026/07/28 Updated By Man-Yi, Yeh
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
    //for test
    public Vector2Int ID
    {
        get { return m_ID; }
    }

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

    public void StartFall(FallDirection direction)
    {
        m_Controller.StartFall(m_ID, direction);
    }

    //-------------------
    //basic
    //-------------------
    public bool IsState(BlockNodeState state)
    {
        return m_State == state;
    }

    public bool IsMoving()
    {
        bool res = m_State == BlockNodeState.VerticalMoving ||
                   m_State == BlockNodeState.HorizontalMoving;

        return res;
    }

    public bool CanVerticalMoveTo()
    {
        bool res;

        switch (m_State)
        {
            case BlockNodeState.HorizontalMoving:
                res = false;
                break;

            case BlockNodeState.Occupied:
                res = m_Block.IsFalling(FallDirection.Down) ||
                      m_Block.IsStateType(BlockStateType.Rise);
                break;

            default:
                res = true;
                break;
        }
       
        return res;
    }


    public bool CanLeftMoveTo()
    {
        bool res;

        switch (m_State)
        {
            case BlockNodeState.VerticalMoving:
                res = false;
                break;

            case BlockNodeState.Occupied:
                res = m_Block.IsFalling(FallDirection.Left);
                break;

            default:
                res = true;
                break;
        }
        
        return res;
    }

    public bool CanRightMoveTo()
    {
        bool res;


        switch (m_State)
        {
            case BlockNodeState.VerticalMoving:
                res = false;
                break;

            case BlockNodeState.Occupied:
                res = m_Block.IsFalling(FallDirection.Right);
                break;

            default:
                res = true;
                break;
        }

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
        Vector2Int tartgetID = new(m_ID.x, m_ID.y);

        //move
        switch (nearPos)
        {
            case BlockNearPos.Above:
                tartgetID += new Vector2Int(0, 1);
                break;

            case BlockNearPos.Below:
                tartgetID += new Vector2Int(0, -1);
                break;

            case BlockNearPos.Left:
                tartgetID += new Vector2Int(-1, 0);
                break;

            case BlockNearPos.Right:
                tartgetID += new Vector2Int(1, 0);
                break;

            default:
                break;
        }
        BlockChangeNode(tartgetID);

        //set node state
        switch (nearPos)
        {
            case BlockNearPos.Below:
                BlockNode above = GetNearNode(BlockNearPos.Above);
                if (above != null && above.IsState(BlockNodeState.Occupied))
                {
                    if (above.Block.IsFalling(FallDirection.Down))
                    {
                        m_State = BlockNodeState.VerticalMoving;
                    }
                }
                break;

            case BlockNearPos.Left:
                BlockNode right = GetNearNode(BlockNearPos.Above);
                if (right != null && right.IsState(BlockNodeState.Occupied))
                {
                    if (right.Block.IsFalling(FallDirection.Left))
                    {
                        m_State = BlockNodeState.HorizontalMoving;
                    }
                }
                break;

            case BlockNearPos.Right:
                BlockNode left = GetNearNode(BlockNearPos.Above);
                if (left != null && left.IsState(BlockNodeState.Occupied))
                {
                    if (left.Block.IsFalling(FallDirection.Right))
                    {
                        m_State = BlockNodeState.HorizontalMoving;
                    }
                }
                break;

            default:
                break;
        }

    }

}
