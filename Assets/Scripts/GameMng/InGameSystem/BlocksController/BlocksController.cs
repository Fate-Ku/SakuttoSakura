//
// BlockController.cs
// 
// 2026/06/02 Created By Man-Yi, Yeh
// 2026/06/03 Updated By Man-Yi, Yeh
// 2026/06/04 Updated By Man-Yi, Yeh
// 2026/06/06 Updated By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
// 2026/06/08 Updated By Man-Yi, Yeh
// 

using System;
using System.Collections.Generic;
using UnityEngine;

public class BlocksController
{
    //oner
    private InGameSystem m_InGameSystem;

    //nodes
    private Dictionary<Vector2Int, BlockNode> m_Nodes = new();
    private Vector2Int m_NextNodeID;

    //gameinfo
    private int m_ColNum;
    private int m_RowNum;

    public BlocksController(InGameSystem inGameSystemfloat, GameInfo gameInfo)
    {
        m_InGameSystem = inGameSystemfloat;

        m_ColNum = gameInfo.GetScale().x;
        m_RowNum = gameInfo.GetScale().y;
        Vector2 referPos = gameInfo.GetReferPos();
        float size = gameInfo.GetSize();

        //Nodes
        //left to right
        //cols: 0 ~ m_ColNum - 1
        for (int i = 0; i < m_ColNum; ++i)
        {
            //above to under
            //rows: -1, 0 ~ m_RowNum - 1, m_RowNum
            for (int j = -1; j <= m_RowNum; ++j)
            {
                Vector2Int id = new(i, j);
                Vector2 pos = referPos + new Vector2(size * i, -size * j);

                BlockNode node = new(this, id, pos);
                m_Nodes.TryAdd(id, node);
            }
        }

        //NextNode
        m_NextNodeID = new(-1, -1);
        Vector2 nextPos = referPos + new Vector2(size * (m_ColNum - 1), -size * -2);
        BlockNode nextNode = new(this, m_NextNodeID, nextPos);
        m_Nodes.TryAdd(m_NextNodeID, nextNode);

    }

    //-------------------
    //update
    //-------------------
    //update
    public void Update()
    {
        //cols: 0 ~ m_ColNum - 1
        for (int i = 0;i< m_ColNum; ++i)
        {
            //rows: m_RowNum, m_RowNum - 1 ~ 0, -1
            //update start from under
            for (int j = m_RowNum; j >= -1 ; --j)
            {
                BlockNode blockNode = GetNode(new Vector2Int(i, j));
                blockNode?.Block?.Update();
            }
        }
    }
    //combine check
    public void CombineCheck(CombineSetsController controller)
    {
        //cols: 0 ~ m_ColNum - 1
        for (int i = 0; i < m_ColNum; ++i)
        {
            //rows: m_RowNum, m_RowNum - 1 ~ 0, -1
            //update start from under
            for (int j = m_RowNum; j >= -1; --j)
            {
                BlockNode blockNode = GetNode(new Vector2Int(i, j));
                blockNode?.Block?.DoCombineCheck(controller);
            }
        }
    }


    //-------------------
    //game
    //-------------------
    public void SetNextBlock(IBlock block)
    {
        BlockNode blockNode = GetNode(m_NextNodeID);
        blockNode.SetBlock(block);
        block.SetPos(blockNode.Pos);
    }

    public void FallBlock(int col)
    {
        if (CanFall(col))
        {
            Vector2Int id = new(col, -1);

            GetNode(m_NextNodeID).Block.SetPos(GetNodePos(id));
            GetNode(m_NextNodeID).BlockChangeNode(id);

            m_InGameSystem.SetNextBlock();
        }
    }

    private bool CanFall(int col)
    {
        bool res = false;

        Vector2Int id = new(col, -1);
        Vector2Int underID= new(col, 0);
        if (IsNodeEmpty(id))
        {
            if (IsNodeEmpty(underID))
            {
                res = true;
            }
            else
            {
                res = GetNode(underID).Block.IsFalling();
            }
        }

        return res;
    }

    //-------------------
    //basic method of node
    //-------------------
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
        if (blockNode != null) 
        {
            res = blockNode.IsEmpty();
        }

        return res;
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

    //-------------------
    //get node
    //-------------------
    public BlockNode GetAboveNode(Vector2Int id)
    {
        BlockNode blockNode = null;

        //if isn't cover
        if (id.y - 1 >= 0)
        {
            //get above node
            blockNode = GetNode(new Vector2Int(id.x, id.y - 1));
        }

        return blockNode;
    }

    public BlockNode GetUnderNode(Vector2Int id)
    {
        BlockNode blockNode = null;

        //if isn't bottom
        if (id.y + 1 < m_RowNum)
        {
            //get under node
            blockNode = GetNode(new Vector2Int(id.x, id.y + 1));
        }

        return blockNode;
    }

    public BlockNode GetLeftNode(Vector2Int id)
    {
        BlockNode blockNode = null;

        //if isn't left wall
        if (id.x - 1 >= 0)
        {
            //get left node
            blockNode = GetNode(new Vector2Int(id.x - 1, id.y));
        }

        return blockNode;
    }

    public BlockNode GetRightNode(Vector2Int id)
    {
        BlockNode blockNode = null;

        //if isn't right wall
        if (id.x + 1 < m_ColNum)
        {
            //get right node
            blockNode = GetNode(new Vector2Int(id.x + 1, id.y));
        }

        return blockNode;
    }

    //-------------------
    //basic method of block
    //-------------------
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

    

}
