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
        scale.x *= 0.8f;
        scale.y *= 0.8f;

        m_Block.BlockOb.transform.localScale = scale;
    }
}
