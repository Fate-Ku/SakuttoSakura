//
// BlockController.cs
// 
// 2026/06/02 Created By Man-Yi, Yeh
// 2026/06/03 Updated By Man-Yi, Yeh
// 2026/06/04 Updated By Man-Yi, Yeh
// 2026/06/06 Updated By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
// 2026/06/08 Updated By Man-Yi, Yeh
// 2026/06/10 Updated By Man-Yi, Yeh
// 2026/06/11 Updated By Man-Yi, Yeh
// 2026/06/16 Updated By Man-Yi, Yeh
// 2026/06/17 Updated By Man-Yi, Yeh
// 2026/06/18 Updated By Man-Yi, Yeh
// 2026/06/22 Updated By Man-Yi, Yeh
// 2026/06/23 Updated By Man-Yi, Yeh
// 

using System.Collections.Generic;
using UnityEngine;


public enum BlockNearPos
{
    Above,
    Below,
    Left,
    Right,

    Count
}

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
                Vector2 pos = referPos + new Vector2(size * i, size * j);

                BlockNode node = new(this, id, pos);
                m_Nodes.TryAdd(id, node);
            }
        }

        //NextNode
        m_NextNodeID = new(-1, -1);
        Vector2 nextPos = gameInfo.GetNextBlockPos();
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
            //rows: -1, 0 ~ m_RowNum - 1, m_RowNum
            //update start from under
            for (int j = -1; j <= m_RowNum ; ++j)
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
            //rows: 0 ~ m_RowNum - 1
            //update start from under
            for (int j = 0; j < m_RowNum; ++j)
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

    public bool IsFullBlocks()
    {
        bool res = true;

        if (!IsAllBlocksIdle())
        {
            //if not all block idle
            //false
            res = false;
        }
        else
        {
            //cols: 0 ~ m_ColNum - 1
            for (int i = 0; i < m_ColNum; ++i)
            {
                bool goBreak = false;

                //rows: 0 ~ m_RowNum - 1
                //update start from under
                for (int j = 0; j < m_RowNum; ++j)
                {
                    if (IsNodeEmpty(new Vector2Int(i, j)))
                    {
                        //if has one empty node
                        //false
                        res = false;
                        goBreak = true;
                        break;
                    }
                }

                if (goBreak)
                {
                    break;
                }
            }
        }

        return res;
    }

    public bool IsAllBlocksIdle()
    {
        bool res = true;

        //cols: 0 ~ m_ColNum - 1
        for (int i = 0; i < m_ColNum; ++i)
        {
            bool goBreak = false;

            //rows: 0 ~ m_RowNum - 1
            //update start from under
            for (int j = 0; j < m_RowNum; ++j)
            {
                BlockNode blockNode = GetNode(new Vector2Int(i, j));
                if (blockNode.IsState(BlockNodeState.Occupied))
                {
                    if (!blockNode.Block.IsStateType(BlockStateType.Idle))
                    {
                        //if has one not idle block
                        //false
                        res = false;
                        goBreak = true;
                        break;
                    }
                }
            }

            if (goBreak)
            {
                break;
            }
        }

        return res;
    }

    //-------------------
    //game for rise
    //-------------------
    public void RiseBlock(IBlock block,int col)
    {
        if (CanRise(col))
        {
            int idY = 0;
            //check from buttom
            //find first empty node
            for (int i = 0; i < m_RowNum; ++i)
            {
                Vector2Int upperID = new(col, i);
                if (IsNodeEmpty(upperID))
                {
                    //upper empty, break
                    idY = i;
                    break;
                }
            }
            //all blocks under the empty node
            //rise from top
            for (int i = idY - 1; i >= 0 ; --i)
            {
                Vector2Int upperID = new(col, i);
                IBlock upperBlock = GetNode(upperID).Block;
                //start rise
                StartRise(upperBlock, upperID);
            }

            //id: target node's id
            //start pos: node that under target node
            Vector2Int id = new(col, -1);
            GetNode(id).SetBlock(block);
            block.SetPos(GetNodePos(id));
            StartRise(block, id);
        }
    }

    public bool CanRise(int col)
    {
        bool res = false;

        //check from buttom
        for (int i = 0; i < m_RowNum; ++i)
        {
            Vector2Int upperID = new(col, i);
            if (IsNodeEmpty(upperID))
            {
                if (i < m_RowNum - 1)
                {
                    res = true;
                }
                else
                {
                    res = !GetNode(upperID).IsMoving();
                }

                break;
            }
            else
            {
                IBlock block = GetNode(upperID).Block;
                if (!block.IsStateType(BlockStateType.Idle))
                {
                    break;
                }
            }
        }

        return res;
    }

    //id = target id
    private void StartRise(IBlock block, Vector2Int nowID)
    {
        block.StartRise(GetNodePos(nowID + new Vector2Int(0, 1)));
        block.GoNearNode(BlockNearPos.Above);

        BlockNode belowBlockNode = GetNode(nowID);
        if (belowBlockNode != null)
        {
            if (!belowBlockNode.IsState(BlockNodeState.Occupied))
            {
                belowBlockNode.State = BlockNodeState.VerticalMoving;
            }
        }
    }

    //id = target id
    public void EndRise(Vector2Int id)
    {
        Vector2Int belowID = id + new Vector2Int(0, -1);
        BlockNode belowBlockNode = GetNode(belowID);
        if (belowBlockNode != null)
        {
            if (!belowBlockNode.IsState(BlockNodeState.Occupied))
            {
                belowBlockNode.State = BlockNodeState.Empty;
            }
        }
    }


    //-------------------
    //game for fall
    //-------------------
    public void FallBlock(int col)
    {
        if (CanFallDown(col))
        {
            Vector2Int id = new(col, m_RowNum);

            GetNode(m_NextNodeID).Block.SetPos(GetNodePos(id));
            GetNode(m_NextNodeID).BlockChangeNode(id);

            m_InGameSystem.SetNextBlock();
        }
    }

    private bool CanFallDown(int col)
    {
        bool res = false;

        Vector2Int id = new(col, m_RowNum);
        Vector2Int underID = new(col, m_RowNum - 1);
        if (IsNodeEmpty(id))
        {
            res = GetNode(underID).CanVerticalMoveTo();
        }

        return res;
    }

    //id = start id
    public void StartFall(Vector2Int id, FallDirection direction)
    {
        switch (direction)
        {
            case FallDirection.Down:
                StartFallDown(id);
                break;

            case FallDirection.Left:
                StartFallLeft(id);
                break;

            case FallDirection.Right:
                StartFallRight(id);
                break;

            default:
                break;
        }
    }

    private void StartFallDown(Vector2Int id)
    {
        Vector2Int belowID = id + new Vector2Int(0, -1);
        BlockNode belowBlockNode = GetNode(belowID);
        if (belowBlockNode != null)
        {
            if (!belowBlockNode.IsState(BlockNodeState.Occupied))
            {
                belowBlockNode.State = BlockNodeState.VerticalMoving;
            }
        }
    }

    private void StartFallLeft(Vector2Int id)
    {
        Vector2Int leftID = id + new Vector2Int(-1, 0);
        BlockNode leftBlockNode = GetNode(leftID);
        if (leftBlockNode != null)
        {
            if (!leftBlockNode.IsState(BlockNodeState.Occupied))
            {
                leftBlockNode.State = BlockNodeState.HorizontalMoving;
            }
        }
    }

    private void StartFallRight(Vector2Int id)
    {
        Vector2Int rightID = id + new Vector2Int(-1, 0);
        BlockNode rightBlockNode = GetNode(rightID);
        if (rightBlockNode != null)
        {
            if (!rightBlockNode.IsState(BlockNodeState.Occupied))
            {
                rightBlockNode.State = BlockNodeState.HorizontalMoving;
            }
        }
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
            res = !blockNode.IsState(BlockNodeState.Occupied);
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
    public BlockNode GetNearNode(BlockNearPos nearPos, Vector2Int id)
    {
        BlockNode blockNode = null;

        switch (nearPos)
        {
            case BlockNearPos.Above:
                blockNode = GetAboveNode(id);
                break;

            case BlockNearPos.Below:
                blockNode = GetBelowNode(id);
                break;

            case BlockNearPos.Left:
                blockNode = GetLeftNode(id);
                break;

            case BlockNearPos.Right:
                blockNode = GetRightNode(id);
                break;

            default: 
                break;

        }

        return blockNode;
    }

    private BlockNode GetAboveNode(Vector2Int id)
    {
        BlockNode blockNode = null;

        //if isn't cover
        if (id.y + 1 < m_RowNum)
        {
            //get above node
            blockNode = GetNode(new Vector2Int(id.x, id.y + 1));
        }

        return blockNode;
    }

    private BlockNode GetBelowNode(Vector2Int id)
    {
        BlockNode blockNode = null;

        //if isn't bottom
        if (id.y - 1 >= 0)
        {
            //get under node
            blockNode = GetNode(new Vector2Int(id.x, id.y - 1));
        }

        return blockNode;
    }

    private BlockNode GetLeftNode(Vector2Int id)
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

    private BlockNode GetRightNode(Vector2Int id)
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

}
