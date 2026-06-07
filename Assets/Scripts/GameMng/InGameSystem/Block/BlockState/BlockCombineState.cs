//
// BlockCombineState.cs
// 
// 2026/06/07 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class BlockCombineState : IBlockState
{
    public BlockCombineState(IBlock block, BlockStateController controller) 
        : base(block, controller)
    {
        StateName = "BlockCombineState";
    }

    public override void StateBegin()
    {
        //adjust size
        Vector3 scale = m_Block.BlockOb.transform.localScale;
        scale.x = 0.5f;
        scale.y = 0.5f;

        m_Block.BlockOb.transform.localScale = scale;
    }

    public override void BeCombinedCheck(IBlock block, CombineSetsController controller)
    {
        m_Block.CombineCheckStartegy.BeCombined(block, m_Block, controller);
    }

}
