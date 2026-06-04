//
// BlockIdleState.cs
// 
// 2026/06/04 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class BlockFallState : IBlockState
{
    public BlockFallState(IBlock block, BlockStateController controller) 
        : base(block, controller)
    {
        this.StateName = "BlockFallState";
    }

    public override void StateBegin()
    {
        Debug.Log("block start fall");

        //falling
        m_Block.FallController.SetFalling(true);
        //set target pos as under node's pos
        Vector2 pos = m_Block.GetUnderNode().Pos;
        m_Block.SetFallTargetPos(pos);
    }

    public override void StateUpdate()
    {
        m_Block.FallController.FallUpdate();
        if (!m_Block.FallController.IsFalling())
        {
            m_Controller.SetState(new BlockIdleState(m_Block, m_Controller));
        }
    }
}
