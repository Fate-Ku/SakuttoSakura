//
// BlockIdleState.cs
// 
// 2026/06/03 Created By Man-Yi, Yeh
// 2026/06/04 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class BlockIdleState : IBlockState
{
    public BlockIdleState(IBlock block, BlockStateController controller) 
        : base(block, controller)
    {
        this.StateName = "BlockIdleState";
    }

    public override void StateUpdate()
    {
        if (m_Block.IsGoFall())
        {
            m_Controller.SetState(new BlockFallState(m_Block, m_Controller));
        }
    }
}
