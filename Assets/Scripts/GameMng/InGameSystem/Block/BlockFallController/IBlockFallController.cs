//
// IBlockFallController.cs
// 
// 2026/06/04 Created By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
// 2026/06/17 Updated By Man-Yi, Yeh
// 2026/06/18 Updated By Man-Yi, Yeh
// 2026/06/22 Updated By Man-Yi, Yeh
// 2026/06/23 Updated By Man-Yi, Yeh
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
    protected List<IFallStrategy> m_Falls = new();
    protected int m_NowFallID;
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

    public IBlockFallController(IBlock block, float speed)
    {
        m_Block = block;
        m_BasicSpeed = speed;

        m_Falls.Add(new DownFall(speed));
    }

    //fall init
    public void FallInit()
    {
        m_NowFallID = 0;
        StartFall();
    }

    //fall update
    public void FallUpdate()
    {
        //update
        m_Falls[m_NowFallID].UpdateFall(m_Block, this);

        //check go next
        if (m_GoNextFall)
        {
            //set next ID
            GoNextFallID();
            if (m_Falls[m_NowFallID].CanFall(m_Block))
            {
                //if can fall
                //start
                StartFall();
            }
            else
            {
                //if can't fall
                //end
                EndFall();
            }
        }
    }


    //-------------------
    //method of game
    //-------------------
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

        if (m_Falls.Count > 0)
        {
            res = m_Falls[m_NowFallID].Direction == direction;
        }

        return res;
    }

    public float GetFallSpeed()
    {
        float res = 0;

        if (m_Falls.Count > 0)
        {
            res = m_Falls[m_NowFallID].Speed;
        }

        return res;
    }

    //-------------------
    //basic method
    //-------------------
    private void GoNextFallID()
    {
        if (m_NowFallID < m_Falls.Count - 1)
        {
            m_NowFallID += 1;
        }
        else
        {
            m_NowFallID = 0;
        }
    }

    private void StartFall()
    {
        m_IsEndFall = false;
        m_GoNextFall = false;
        m_Falls[m_NowFallID].StartFall(m_Block);
    }

    public void EndFall(bool resetFallController = true)
    {
        IsEndFall = true;
        if (resetFallController)
        {
            ResetlFallController();
        }
    }

    protected virtual void ResetlFallController() { }

}
