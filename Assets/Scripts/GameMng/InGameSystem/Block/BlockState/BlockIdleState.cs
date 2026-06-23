//
// BlockIdleState.cs
// 
// 2026/06/03 Created By Man-Yi, Yeh
// 2026/06/04 Updated By Man-Yi, Yeh
// 2026/06/06 Updated By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
// 2026/06/10 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class BlockIdleState : IBlockState
{
    public BlockIdleState(IBlock block, BlockStateController controller) 
        : base(block, controller)
    {
        StateType = BlockStateType.Idle;
        StateName = "BlockIdleState";
    }

    public override void StateBegin()
    {
        Debug.Log("test block start idle: " + m_Block.Type.ToString());
    }

    public override void StateUpdate()
    {
        if (m_Block.IsGoFallDown())
        {
            m_Controller.SetState(new BlockFallState(m_Block, m_Controller));
            return;
        } 
    }

    public override void DoCombineCheck(CombineSetsController controller)
    {
        m_Block.CombineStartegy.DoCombineCheck(m_Block, controller);
    }

    public override void BeCombinedCheck(IBlock nearBlock, CombineSetsController controller)
    {
        m_Block.CombineStartegy.BeCombinedCheck(nearBlock, m_Block, controller);
    }

    public override void NearDestroy(IBlock destroyBlock)
    {
        m_Block.NearCombineStrategy.NearDestroy(m_Block, destroyBlock);
    }

    public override void BeDestroyed()
    {
        m_Block.GoDestroy();
    }
}
