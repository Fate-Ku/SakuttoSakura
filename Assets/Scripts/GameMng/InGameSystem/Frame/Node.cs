//
// Node.cs
// 
// 2026/06/02 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class Node
{
    private IBlock m_Block;
    public IBlock Block
    {
        get { return m_Block; }
    }

    //fixed info
    private Frame m_Frame;  //oner
    private Vector2Int m_ID;
    private Vector2 m_Pos;
    public Vector2 Pos
    {
        get { return m_Pos; }
    }

    public Node(Frame frame, Vector2Int id, Vector2 pos)
    {
        m_Frame = frame;
        m_ID = id;
        m_Pos = pos;
    }

    public void SetBlock(IBlock block)
    {
        m_Block = block;
        m_Block.OnerNode = this;
    }
}
