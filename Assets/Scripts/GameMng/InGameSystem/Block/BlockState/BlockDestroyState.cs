//
// BlockDestroyState.cs
// 
// 2026/06/07 Created By Man-Yi, Yeh
// 2026/06/08 Updated By Man-Yi, Yeh
// 2026/06/10 Updated By Man-Yi, Yeh
// 2026/06/25 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class BlockDestroyState : IBlockState
{
    public BlockDestroyState(IBlock block, BlockStateController controller) 
        : base(block, controller)
    {
        StateType = BlockStateType.Destroy;
        StateName = "BlockDestroyState";  
    }

    public override void StateBegin()
    {
        //remove combine set
        m_Block.RemoveCombineSet();

        m_Block.DestroyStrategy.DestroyStart(m_Block);
        m_Block.SetAnimation("Destroy", true);

        //test
        m_Block.blockTest.trigger = m_Trigger;
    }

    public override void StateEnd()
    {
        m_Block.SetAnimation("Destroy", false);
    }

    public override void StateUpdate()
    {
        m_Block.DestroyStrategy.DestroyUpdate(m_Block);
    }
}
