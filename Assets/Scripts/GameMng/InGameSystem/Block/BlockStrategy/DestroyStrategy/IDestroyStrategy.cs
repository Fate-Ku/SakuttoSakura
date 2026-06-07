//
// IDestroyStrategy.cs
// 
// 2026/06/06 Created By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
//


using UnityEngine;

public abstract class IDestroyStrategy
{
    //block that be created after destory
    protected IBlock m_CreateBlock;
    public IBlock CreateBlock
    {
        set { m_CreateBlock = value; }
    }

    public abstract void DestroyStart(IBlock onerBlock);
    public abstract void DestroyEnd(IBlock onerBlock);
    public abstract void DestroyUpdate(IBlock onerBlock);
}
