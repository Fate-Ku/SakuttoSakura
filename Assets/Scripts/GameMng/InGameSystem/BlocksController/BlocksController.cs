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
// 2026/06/24 Updated By Man-Yi, Yeh
// 2026/06/25 Updated By Man-Yi, Yeh
// 2026/06/26 Updated By Man-Yi, Yeh
// 2026/06/29 Updated By Man-Yi, Yeh
// 2026/07/13 Updated By Man-Yi, Yeh
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
    private Vector2Int m_NextNextNodeID;

    //gameinfo
    private int m_ColNum;
    private int m_RowNum;
    private float m_Size;
    private float m_NextSize;
    private float m_NextNextSize;
    private float m_NextOperateTime;

    //set next block
    private bool m_IsSettingNextBlock = false;
    public bool IsSettingNextBlock
    {
        get { return m_IsSettingNextBlock;  }
    }
    private float m_SetNextBlockTimer = 0;


    public BlocksController(InGameSystem inGameSystemfloat, GameInfo gameInfo)
    {
        m_InGameSystem = inGameSystemfloat;

        m_ColNum = gameInfo.GetScale().x;
        m_RowNum = gameInfo.GetScale().y;
        m_Size = gameInfo.GetSize();
        m_NextSize = gameInfo.GetNextBlockSize();
        m_NextNextSize = gameInfo.GetNextNextBlockSize();
        m_NextOperateTime = gameInfo.GetNextOperateTime();

        Vector2 referPos = gameInfo.GetReferPos();


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
                Vector2 pos = referPos + new Vector2(m_Size * i, m_Size * j);

                BlockNode node = new(this, id, pos);
                m_Nodes.TryAdd(id, node);
            }
        }

        //NextNode
        m_NextNodeID = new(-1, -1);
        Vector2 nextPos = gameInfo.GetNextBlockPos();
        BlockNode nextNode = new(this, m_NextNodeID, nextPos);
        m_Nodes.TryAdd(m_NextNodeID, nextNode);
        //NextNextBlock
        m_NextNextNodeID = new(-2, -2);
        Vector2 nextNextPos = gameInfo.GetNextNextBlockPos();
        BlockNode nextNextNode = new(this, m_NextNextNodeID, nextNextPos);
        m_Nodes.TryAdd(m_NextNextNodeID, nextNextNode);

    }

    public void Init(IBlock nextBlock, IBlock nextNextBlock)
    {
        //next
        BlockNode nextNode = GetNode(m_NextNodeID);
        nextNode.SetBlock(nextBlock);
        nextNode?.Block?.SetPos(new Vector3(nextNode.Pos.x, nextNode.Pos.y, 0));
        nextNode?.Block?.SetSize(m_NextSize);
        //next mext
        BlockNode nextNextNode = GetNode(m_NextNextNodeID);
        nextNextNode.SetBlock(nextNextBlock);
        nextNextNode?.Block?.SetPos(new Vector3(nextNextNode.Pos.x, nextNextNode.Pos.y, 1));
        nextNextNode?.Block?.SetSize(m_NextNextSize);
    }

    public void Update()
    {
        UpdateBlocks();
        UpdateSetNextBlock();
    }

    private void UpdateBlocks()
    {
        List<IBlock> checkedBlocks = new();
        //rows: -1, 0 ~ m_RowNum - 1, m_RowNum
        for (int j = -1; j <= m_RowNum; ++j)
        {
            //cols: 0 ~ m_ColNum - 1
            //update start from under
            for (int i = 0; i < m_ColNum; ++i)
            {
                BlockNode blockNode = GetNode(new Vector2Int(i, j));
                IBlock block = blockNode?.Block;

                if (block != null)
                {
                    //if isn't checked
                    if (!checkedBlocks.Contains(block))
                    {
                        //if fall right,do set update
                        if (block.IsFalling(FallDirection.Right))
                        {
                            //make check set
                            List<IBlock> checkSet = new()
                            {
                                block
                            };
                            for (int k = i + 1; k <= m_ColNum; ++k)
                            {
                                BlockNode checkNode = GetNode(new Vector2Int(k, j));
                                IBlock checkBlock = checkNode?.Block;

                                if (checkBlock == null)
                                {
                                    break;
                                }
                                else
                                {
                                    checkSet.Add(checkBlock);
                                    if (!checkBlock.IsFalling(FallDirection.Right))
                                    {
                                        break;
                                    }
                                }
                            }
                            //check from right
                            for (int k = 0; k < checkSet.Count; ++k)
                            {
                                checkSet[checkSet.Count - 1 - k].Update();
                                checkedBlocks.Add(checkSet[k]);
                            }
                        }
                        //if not fall right, update
                        else
                        {
                            block.Update();
                            checkedBlocks.Add(block);
                        }
                    }
                }
            }
        }
    }

    private void UpdateSetNextBlock()
    {
        if (m_IsSettingNextBlock)
        {
            m_SetNextBlockTimer += Time.deltaTime;
            if (m_SetNextBlockTimer >= m_NextOperateTime)
            {
                m_IsSettingNextBlock = false;

                BlockNode nextNode = GetNode(m_NextNodeID);
                Vector2 nextPos = GetNodePos(m_NextNodeID);
                nextNode?.Block?.SetPos(new Vector3(nextPos.x, nextPos.y, 0));
                nextNode?.Block?.SetSize(m_NextSize);

                BlockNode nextNextNode = GetNode(m_NextNextNodeID);
                nextNextNode?.Block?.SetActive(true);
            }
            else
            {
                BlockNode nextNode = GetNode(m_NextNodeID);
                Vector2 nextPos = GetNodePos(m_NextNodeID);
                Vector2 nextNextPos = GetNodePos(m_NextNextNodeID);
                Vector2 nowPos = nextNextPos + (nextPos - nextNextPos) * (m_SetNextBlockTimer / m_NextOperateTime);
                float nowSize = m_NextNextSize + (m_NextSize - m_NextNextSize) * (m_SetNextBlockTimer / m_NextOperateTime);

                nextNode?.Block?.SetPos(nowPos);
                nextNode?.Block?.SetSize(nowSize);
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
    public void SetPreviewBlocks(IBlock nowNextNextBlock)
    {
        m_IsSettingNextBlock = true;
        m_SetNextBlockTimer = 0;

        BlockNode nextNode = GetNode(m_NextNodeID);
        BlockNode nextNextNode = GetNode(m_NextNextNodeID);
        if (nextNode != null && nextNextNode != null)
        {
            //now next
            nextNextNode.BlockChangeNode(m_NextNodeID);
            m_InGameSystem.SetNextBlockPath(nextNode.Block.Type);

            //new next next
            nextNextNode.SetBlock(nowNextNextBlock);
            nowNextNextBlock.SetPos(new Vector3(nextNextNode.Pos.x, nextNextNode.Pos.y, 1));
            nowNextNextBlock.SetSize(m_NextNextSize);
            nowNextNextBlock.SetActive(false);
        }
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

            //rows: -1, 0 ~ m_RowNum - 1, m_RowNum
            //update start from under
            for (int j = -1; j < m_RowNum + 1; ++j)
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

    public int GetNumOfBlock()
    {
        int res = 0;

        //cols: 0 ~ m_ColNum - 1
        for (int i = 0; i < m_ColNum; ++i)
        {
            //rows: 0 ~ m_RowNum - 1
            //update start from under
            for (int j = 0; j < m_RowNum; ++j)
            {
                BlockNode blockNode = GetNode(new Vector2Int(i, j));
                if (blockNode != null)
                {
                    if (blockNode.IsState(BlockNodeState.Occupied))
                    {
                        res += 1;
                    }
                }
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
                    res = GetNode(upperID).CanVerticalMoveTo();
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

            BlockNode blockNode = GetNode(m_NextNodeID);
            if (blockNode != null)
            {
                IBlock block = blockNode.Block;
                blockNode.BlockChangeNode(id);

                block.SetPos(GetNodePos(id));
                block.GoState(BlockStateType.Fall);

                block.SetSize(m_Size);
            }

            m_InGameSystem.SetPreviewBlocks();
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
        Vector2Int rightID = id + new Vector2Int(1, 0);
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
