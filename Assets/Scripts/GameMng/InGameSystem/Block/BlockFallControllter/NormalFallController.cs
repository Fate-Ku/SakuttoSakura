//
// NormalFallController.cs
// 
// 2026/06/04 Created By Man-Yi, Yeh
// 


using UnityEngine;

public class NormalFallController : IBlockFallController
{
    public NormalFallController(IBlock block) 
        : base(block)
    {
        m_FallInfo.Speed = 2.5f;
    }

    public override void FallUpdate()
    {
        float moveY = m_FallInfo.Speed * Time.deltaTime;
        float newY = m_Block.Pos.y - moveY;
        float targetY = m_FallInfo.TargetPos.y;

        if (newY <= targetY)
        {
            //finish fall
            m_Block.GoUnderNode();
            //check for continue fall
            if (m_Block.IsGoFall())
            {
                //move to newY
                m_Block.SetPos(new Vector2(m_Block.Pos.x, newY));
                //set target pos as under node's pos
                Vector2 pos = m_Block.GetUnderNode().Pos;
                m_Block.SetFallTargetPos(pos);
            }
            else
            {
                //move to targetY
                m_Block.SetPos(new Vector2(m_Block.Pos.x, targetY));
                //end fall
                SetFalling(false);
            }
        }
        else
        {
            //move to newY
            m_Block.SetPos(new Vector2(m_Block.Pos.x, newY));
        }
        
    }
}
