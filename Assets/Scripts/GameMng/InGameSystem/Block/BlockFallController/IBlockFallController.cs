//
// IBlockFallController.cs
// 
// 2026/06/04 Created By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
// 2026/06/17 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public enum FallDirection
{
    Down,
    Left,
    Right,
}

public struct FallInfo
{
    public bool IsFalling;
    public Vector2 TargetPos;
    public FallDirection Direction;
    public float Speed;
}

public class IBlockFallController
{
    //block
    protected IBlock m_Block;
    //fall info
    protected FallInfo m_FallInfo;
    public FallInfo FallInfo
    {
        get { return m_FallInfo; }
    }

    protected bool m_IsEndFall = false;
    public bool IsEndFall
    {
        get { return m_IsEndFall; }
        set { m_IsEndFall = value; }
    }

    public IBlockFallController(IBlock block)
    {
        m_Block = block;
    }


    //fall
    public virtual void FallUpdate()
    {

    }

    //is go fall
    public bool IsGoFall()
    {
        bool res = false;

        BlockNode belowNode = m_Block.GetNearNode(BlockNearPos.Below);
        if (belowNode != null)
        {
            IBlock belowBlock = belowNode.Block;
            if (belowBlock == null)
            {
                res = true;
            }
            else
            {
                if (belowBlock.IsStateType(BlockStateType.Fall) ||
                    belowBlock.IsStateType(BlockStateType.Rise))
                {
                    res = true;
                }
            }
        }

        return res;
    }

    public bool IsFalling()
    {
        return m_FallInfo.IsFalling;
    }

    public void SetFalling(bool isFalling)
    {
        m_FallInfo.IsFalling = isFalling;
    }

    public void SetTargetPos(Vector2 targetPos)
    {
        m_FallInfo.TargetPos = targetPos;
    }

    public void SetSpeed(float speed)
    {
        m_FallInfo.Speed = speed;
    }
}
