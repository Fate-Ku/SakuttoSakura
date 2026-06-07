//
// IDestroyStrategy.cs
// 
// 2026/06/06 Created By Man-Yi, Yeh
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

    public abstract void DestroyStart(IBlock block);
    public abstract void DestroyEnd(IBlock block);
    public abstract void DestroyUpdate(IBlock block);
}
