//
// IBlockFallController.cs
// 
// 2026/06/04 Created By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
// 2026/06/17 Updated By Man-Yi, Yeh
// 2026/06/18 Updated By Man-Yi, Yeh
// 2026/06/22 Updated By Man-Yi, Yeh
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

public class IBlockFallController
{
    //block
    protected IBlock m_Block;
    //fall strategys
    protected List<IFallStrategy> m_FallStrategys = new();
    protected int m_NowFallStartegyID;

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

    //fall init
    public void FallInit()
    {
        //foreach ()
    }

    //fall update
    public virtual void FallUpdate()
    {

    }

    //is go fall
    public bool IsGoFallDown()
    {
        bool res = false;

        BlockNode belowNode = m_Block.GetNearNode(BlockNearPos.Below);
        if (belowNode != null)
        {
            res = belowNode.CanVerticalMoveTo();
        }

        return res;
    }

    public bool IsFalling(FallDirection direction)
    {
        bool res = false;

        if (m_FallStrategys.Count > 0)
        {
            res = m_FallStrategys[m_NowFallStartegyID].Direction == direction;
        }

        return res;
    }

    public float GetFallSpeed()
    {
        float res = 0;

        if (m_FallStrategys.Count > 0)
        {
            res = m_FallStrategys[m_NowFallStartegyID].Speed;
        }

        return res;
    }
}
