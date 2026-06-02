//
// Frame.cs
// 
// 2026/06/02 Created By Man-Yi, Yeh
// 

using UnityEngine;

using System;
using System.Collections.Generic;

public class Frame
{
    //nodes
    private Dictionary<Vector2Int, Node> m_Nodes = new();

    public Frame(GameInfo gameInfo)
    {
        int colNum = gameInfo.GetScale().x;
        int rowNum = gameInfo.GetScale().y;
        Vector2 referPos = gameInfo.GetReferPos();
        float size = gameInfo.GetSize();

        //cols: 0 ~ colNum - 1
        for (int i = 0; i < colNum; ++i)
        {
            //rows: -1, 0 ~ rowNum - 1, rowNum
            for (int j = -1; j <= rowNum; ++j)
            {
                Vector2Int id = new(i, j);
                Vector2 pos = referPos + new Vector2(size * i, -size * j);

                Node node = new(this, id, pos);
                m_Nodes.TryAdd(id, node);
            }
        }
    }

    public void AddBlock(Vector2Int id, IBlock block)
    {
        m_Nodes[id].SetBlock(block);
    }

    public Vector2 GetBlockPos(Vector2Int id)
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
