//
// BlockCombineState.cs
// 
// 2026/06/07 Created By Man-Yi, Yeh
// 2026/06/10 Updated By Man-Yi, Yeh
// 2026/06/11 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class BlockCombineState : IBlockState
{
    public BlockCombineState(IBlock block, BlockStateController controller) 
        : base(block, controller)
    {
        StateType = BlockStateType.Combine;
        StateName = "BlockCombineState";
    }

    public override void StateBegin()
    {
        //adjust size
        Vector3 scale = m_Block.BlockOb.transform.localScale;
        scale.x = 0.45f;
        scale.y = 0.45f;

        m_Block.BlockOb.transform.localScale = scale;

        //test
        m_Block.blockTest.trigger = m_Trigger;
    }

    public override void BeCombinedCheck(IBlock nearBlock, CombineSetsController controller)
    {
        m_Block.CombineStartegy.BeCombinedCheck(nearBlock, m_Block, controller);
    }

}
