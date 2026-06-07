//
// BlockIdleState.cs
// 
// 2026/06/03 Created By Man-Yi, Yeh
// 2026/06/04 Updated By Man-Yi, Yeh
// 2026/06/06 Updated By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class BlockIdleState : IBlockState
{
    public BlockIdleState(IBlock block, BlockStateController controller) 
        : base(block, controller)
    {
        StateName = "BlockIdleState";
    }

    public override void StateUpdate()
    {
        if (m_Block.IsGoFall())
        {
            m_Controller.SetState(new BlockFallState(m_Block, m_Controller));
        }
    }

    public override void StateCombineCheck(CombineSetsController controller)
    {
        m_Block.CombineCheckStartegy.Do(m_Block, controller);
    }

    public override void NearDestroy()
    {
        m_Block.NearDestroyStrategy.Do(m_Block);
    }

    public override void BeDestroyed()
    {
        m_Block.GoDestroy();
    }
}
