//
// BlockIdleState.cs
// 
// 2026/06/03 Created By Man-Yi, Yeh
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
        if (Input.GetKeyDown(KeyCode.T))
        {
            m_Block.Test(true);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            m_Block.Test(false);
        }
    }
}
