//
// IBlockFallController.cs
// 
// 2026/06/04 Created By Man-Yi, Yeh
// 

using UnityEngine;

public struct FallInfo
{
    public bool IsFalling;
    public float Speed;
}

public class IBlockFallController
{
    private IBlock m_Block;

    private FallInfo m_FallInfo;
    public FallInfo FallInfo
    {
        get { return m_FallInfo; }
    }

    public IBlockFallController(IBlock block)
    {
        m_Block = block;
    }

    //return is continue fall
    public virtual void FallUpdate()
    {

    }

    public bool IsFalling()
    {
        return m_FallInfo.IsFalling;
    }

    public void SetFalling(bool isFalling)
    {
        m_FallInfo.IsFalling = isFalling;
    }
}
