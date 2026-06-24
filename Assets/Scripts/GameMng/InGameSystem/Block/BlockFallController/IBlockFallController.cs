//
// IBlockFallController.cs
// 
// 2026/06/04 Created By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
// 2026/06/17 Updated By Man-Yi, Yeh
// 2026/06/18 Updated By Man-Yi, Yeh
// 2026/06/22 Updated By Man-Yi, Yeh
// 2026/06/23 Updated By Man-Yi, Yeh
// 2026/06/24 Updated By Man-Yi, Yeh
// 


using System;
using System.Collections.Generic;
using UnityEngine;


public enum FallDirection
{
    Down,
    Left,
    Right,
}

public  abstract class IBlockFallController
{
    //block
    protected IBlock m_Block;
    //fall strategys
    protected List<IFallStrategy> m_FallStrategys = new();
    protected int m_NowFallStrategyID;
    protected float m_BasicSpeed;
    public float BasicSpeed
    {
        get { return m_BasicSpeed; }
    }

    protected bool m_IsEndFall = false;
    public bool IsEndFall
    {
        get { return m_IsEndFall; }
        set { m_IsEndFall = value; }
    }

    protected bool m_GoNextFall = false;
    public bool GoNextFall
    {
        set { m_GoNextFall = value; }
    }

    public IBlockFallController(IBlock block)
    {
        m_Block = block;
    }

    //fall init
    public void FallInit()
    {
        m_IsEndFall = false;
        m_NowFallStrategyID = 0;
        StartFall();
    }

    //fall update
    public void FallUpdate()
    {
        //update
        m_FallStrategys[m_NowFallStrategyID].UpdateFall(m_Block, this);

        //check go next
        if (m_GoNextFall)
        {
            
            //check can next fall
            if (CanNextFall())
            {
                //if can
                //start fall
                StartFall();
            }
            else
            {
                //if can't
                //end fall
                EndFall();
            }
        }
    }


    //-------------------
    //method of game
    //-------------------
    public bool IsGoFall(FallDirection direction)
    {
        bool res = false;

        switch (direction)
        {
            case FallDirection.Down:
                res = IsGoFallDown();
                break;

            case FallDirection.Left:
                res = IsGoFallLeft();
                break;

            case FallDirection.Right:
                res = IsGoFallRight();
                break;

            default:
                break;
        }


        return res;
    }


    public bool IsFalling(FallDirection direction)
    {
        bool res = false;

        if (m_FallStrategys.Count > 0)
        {
            res = m_FallStrategys[m_NowFallStrategyID].Direction == direction;
        }

        return res;
    }

    public float GetFallSpeed()
    {
        float res = 0;

        if (m_FallStrategys.Count > 0)
        {
            res = m_FallStrategys[m_NowFallStrategyID].Speed;
        }

        return res;
    }

    //is go fall down
    private bool IsGoFallDown()
    {
        bool res = false;

        BlockNode belowNode = m_Block.GetNearNode(BlockNearPos.Below);
        if (belowNode != null)
        {
            res = belowNode.CanVerticalMoveTo();
        }

        return res;
    }

    //is go fall left
    private bool IsGoFallLeft()
    {
        bool res = false;

        BlockNode leftNode = m_Block.GetNearNode(BlockNearPos.Left);
        if (leftNode != null)
        {
            res = leftNode.CanLeftMoveTo();
        }

        return res;
    }

    //is go fall right
    private bool IsGoFallRight()
    {
        bool res = false;

        BlockNode rightNode = m_Block.GetNearNode(BlockNearPos.Right);
        if (rightNode != null)
        {
            res = rightNode.CanRightMoveTo();
        }

        return res;
    }

    //-------------------
    //basic method
    //-------------------
    protected void GoNextFallID()
    {
        m_NowFallStrategyID += 1;
        if (m_NowFallStrategyID >= m_FallStrategys.Count)
        {
            m_NowFallStrategyID = 0;
        }
    }

    private void StartFall()
    {
        m_GoNextFall = false;
        m_FallStrategys[m_NowFallStrategyID].StartFall(m_Block);
    }

    private void EndFall()
    {
        ResetlFallController();
        m_Block.FallController.IsEndFall = true;
    }

    //-------------------
    //method of update
    //-------------------
    protected virtual bool CanNextFall()
    {
        bool res = false;
        return res;
    }

    public virtual void ResetlFallController() { }

}
