//
// BlockNode.cs
// 
// 2026/06/02 Created By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
// 2026/06/08 Updated By Man-Yi, Yeh
// 2026/06/10 Updated By Man-Yi, Yeh
// 

using NUnit.Framework.Interfaces;
using UnityEngine;

public class BlockNode
{
    private IBlock m_Block = null;
    public IBlock Block
    {
        get { return m_Block; }
    }
    public bool IsEmpty()
    {
        return m_Block == null;
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
    //basic
    //-------------------
    public void SetBlock(IBlock block)
    {
        m_Block = block;
        m_Block.BlockNode = this;
    }

    public void RemoveBlock()
    {
        m_Block.BlockNode = null;
        m_Block = null;
    }

    public void BlockChangeNode(Vector2Int id)
    {
        if (m_Controller.IsNodeEmpty(id))
        {
            m_Controller.GetNode(id).SetBlock(m_Block);
            m_Block = null;
        }
    }


    //-------------------
    //get node
    //-------------------
    public BlockNode GetAboveNode()
    {
        return m_Controller.GetAboveNode(m_ID);
    }

    public BlockNode GetBelowNode()
    {
        return m_Controller.GetBelowNode(m_ID);
    }
    public BlockNode GetLeftNode()
    {
        return m_Controller.GetLeftNode(m_ID);
    }

    public BlockNode GetRightNode()
    {
        return m_Controller.GetRightNode(m_ID);
    }


    //-------------------
    //go node
    //-------------------
    public void BlockGoBelowNode()
    {
        Vector2Int id = new(m_ID.x, m_ID.y + 1);
        BlockChangeNode(id);
    }

    


}
