//
// IBlock.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
// 

using UnityEngine;

public abstract class IBlock
{
    //info
    //oner frame
    //oner combine set

    //startegys
    private IBlockStrategy m_UpdateStartegy;
    private IBlockStrategy m_CombineCheckStartegy;
    private IBlockStrategy m_NextDestroyStrategy;
    private IBlockStrategy m_DestroyStrategy;

    public IBlock() { }

    public virtual void Init() { }
    public virtual void Term() { }
    public virtual void Update() { }


}
