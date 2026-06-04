//
// BlockNode.cs
// 
// 2026/06/02 Created By Man-Yi, Yeh
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

    public void SetBlock(IBlock block)
    {
        m_Block = block;
        m_Block.OnerNode = this;
    }

    public void RemoveBlock()
    {
        m_Block.OnerNode = null;
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

    public BlockNode GetUnderNode()
    {
        BlockNode blockNode = null;



        return blockNode;
    }
}
