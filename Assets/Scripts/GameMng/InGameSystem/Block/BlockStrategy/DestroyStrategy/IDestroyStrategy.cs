//
// IDestroyStrategy.cs
// 
// 2026/06/06 Created By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
// 2026/06/08 Updated By Man-Yi, Yeh
//


using UnityEngine;

public abstract class IDestroyStrategy
{
    //block that be created after destory
    protected IBlock m_CreateBlock;

    public abstract void DestroyStart(IBlock onerBlock);
    public abstract void DestroyEnd(IBlock onerBlock);
    public abstract void DestroyUpdate(IBlock onerBlock);

    public void SetCreateBlock(IBlock block)
    {
        m_CreateBlock = block;
        block?.SetActive(false);
    }

    protected void CreateBlockAfterDestroy(IBlock onerBlock)
    {
        if (m_CreateBlock != null)
        {
            BlockNode blockNode = onerBlock.BlockNode;
            blockNode.RemoveBlock();
            blockNode.SetBlock(m_CreateBlock);

            m_CreateBlock.SetActive(true);
            m_CreateBlock.SetPos(blockNode.Pos);
        }
    }
}
