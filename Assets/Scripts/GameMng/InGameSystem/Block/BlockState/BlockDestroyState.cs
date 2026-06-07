//
// BlockDestroyState.cs
// 
// 2026/06/07 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class BlockDestroyState : IBlockState
{
    

    public BlockDestroyState(IBlock block, BlockStateController controller) 
        : base(block, controller)
    {
        StateName = "BlockDestroyState";
        
    }

    public override void StateBegin()
    {
        m_Block.DestroyStrategy.DestroyStart(m_Block);
    }

    public override void StateUpdate()
    {
        m_Block.DestroyStrategy.DestroyUpdate(m_Block);
    }
}
