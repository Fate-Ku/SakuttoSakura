//
// BlockController.cs
// 
// 2026/06/02 Created By Man-Yi, Yeh
// 

using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BlockController
{
    //nodes
    private Dictionary<Vector2Int, BlockNode> m_Nodes = new();
    private Vector2Int m_NextNodeID;

    public BlockController(GameInfo gameInfo)
    {
        int colNum = gameInfo.GetScale().x;
        int rowNum = gameInfo.GetScale().y;
        Vector2 referPos = gameInfo.GetReferPos();
        float size = gameInfo.GetSize();

        //Nodes
        //cols: 0 ~ colNum - 1
        for (int i = 0; i < colNum; ++i)
        {
            //rows: -1, 0 ~ rowNum - 1, rowNum
            for (int j = -1; j <= rowNum; ++j)
            {
                Vector2Int id = new(i, j);
                Vector2 pos = referPos + new Vector2(size * i, -size * j);

                BlockNode node = new(this, id, pos);
                m_Nodes.TryAdd(id, node);
            }
        }

        //NextNode
        m_NextNodeID = new(-1, -1);
        Vector2 nextPos = referPos + new Vector2(size * (colNum - 1), -size * -2);
        BlockNode nextNode = new(this, m_NextNodeID, nextPos);
        m_Nodes.TryAdd(m_NextNodeID, nextNode);

    }

    public void SetNextBlock(IBlock block)
    {
        BlockNode blockNode = GetNode(m_NextNodeID);
        blockNode.SetBlock(block);
        block.SetPos(blockNode.Pos);
    }

    public void FallBlock(int col)
    {
        Vector2Int id = new(col, 0);

        if (IsNodeEmpty(id))
        {
            GetNode(m_NextNodeID).Block.SetPos(GetNodePos(id));
            GetNode(m_NextNodeID).BlockChangeNode(id);
        }
    }

    public BlockNode GetNode(Vector2Int id)
    {
        BlockNode blockNode = null;

        if (m_Nodes.TryGetValue(id, out var node))
        {
            blockNode = node;
        }
        return blockNode;
    }

    public bool IsNodeEmpty(Vector2Int id)
    {
        bool res = false;

        BlockNode blockNode = GetNode(id);
        res = (blockNode.Block == null);

        return res;
    }
    private void AddBlock(Vector2Int id, IBlock block)
    {
        BlockNode blockNode = GetNode(id);
        blockNode.SetBlock(block);
    }

    private void RemoveBlock(Vector2Int id)
    {
        BlockNode blockNode = GetNode(id);
        blockNode.RemoveBlock();
    }

    public Vector2 GetNodePos(Vector2Int id)
    {
        Vector2 res = new(0, 0);

        if (m_Nodes.TryGetValue(id, out var node)) 
        {
            res = node.Pos;
        }

        return res;
    }

    public void Test(Vector2Int id,bool active)
    {
        if (m_Nodes.TryGetValue(id, out var node))
        {
            node.Block?.Test(active);
        }
        else
        {
            Debug.Log("out frame");
        }
    }
}
