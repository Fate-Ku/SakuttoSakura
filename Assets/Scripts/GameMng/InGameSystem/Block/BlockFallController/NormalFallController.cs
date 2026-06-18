//
// NormalFallController.cs
// 
// 2026/06/04 Created By Man-Yi, Yeh
// 2026/06/16 Updated By Man-Yi, Yeh
// 2026/06/17 Updated By Man-Yi, Yeh
// 


using UnityEngine;

public class NormalFallController : IBlockFallController
{
    public NormalFallController(IBlock block, float speed)
        : base(block)
    {
        m_FallInfo.IsFalling = false;
        m_FallInfo.Direction = FallDirection.Down;
        m_FallInfo.Speed = speed;
    }

    public override void FallUpdate()
    {
        float moveY = m_FallInfo.Speed * Time.deltaTime;
        float newY = m_Block.Pos.y - moveY;
        float targetY = m_FallInfo.TargetPos.y;

        //check is under rising
        BlockNode underBlockNode = m_Block.GetNearNode(BlockNearPos.Below);
        if (underBlockNode != null)
        {
            if (!underBlockNode.IsEmpty())
            {
                IBlock underBlock = underBlockNode.Block;
                if (underBlock.IsStateType(BlockStateType.Rise))
                {
                    //if under rising
                    float riseBlockY = underBlock.Pos.y;
                    if (newY - riseBlockY < m_Block.Size)
                    {
                        //if collision
                        Vector2 pos = m_Block.BlockNode.Pos;
                        //end falling
                        SetFalling(false);
                        //start rise
                        m_Block.StartRise(pos);
                        return;
                    }
                }
            }
        }

        //check arrive
        if (newY <= targetY)
        {
            //if arrive
            //finish fall
            m_Block.GoNearNode(BlockNearPos.Below);
            //check for continue fall
            if (m_Block.IsGoFall())
            {
                //move to newY
                m_Block.SetPos(new Vector2(m_Block.Pos.x, newY));
                //set target pos as below node's pos
                Vector2 pos = m_Block.GetNearNode(BlockNearPos.Below).Pos;
                m_Block.SetFallTargetPos(pos);
            }
            else
            {
                //move to targetY
                m_Block.SetPos(new Vector2(m_Block.Pos.x, targetY));
                //end falling
                SetFalling(false);
                //end fall
                m_IsEndFall = true;
            }
        }
        else
        {
            //move to newY
            m_Block.SetPos(new Vector2(m_Block.Pos.x, newY));
        }
    }
}
