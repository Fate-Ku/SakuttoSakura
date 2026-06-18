//
// BlockRiseController.cs
// 
// 2026/06/16 Created By Man-Yi, Yeh
// 

using UnityEngine;


public class BlockRiseController
{
    //block
    protected IBlock m_Block;

    private bool m_IsRising;
    public bool IsRising
    {
        get { return m_IsRising; }
    }

    private Vector2 m_TargetPos;
    private float m_Speed;

    public BlockRiseController(IBlock block, float speed)
    {
        m_Block = block;
        m_Speed = speed;
    }

    public void RiseUpdate()
    {
        float moveY = m_Speed * Time.deltaTime;
        float newY = m_Block.Pos.y + moveY;
        float targetY = m_TargetPos.y;

        if (newY >= targetY)
        {
            //finish rise
            m_IsRising = false;
            m_Block.EndRise();
            
            //move to targetY
            m_Block.SetPos(new Vector2(m_Block.Pos.x, targetY));

        }
        else
        {
            //move to newY
            m_Block.SetPos(new Vector2(m_Block.Pos.x, newY));
        }
    }

    public void StartRise(Vector2 targetPos) 
    {
        m_IsRising = true;
        m_TargetPos = targetPos;
    }
}
