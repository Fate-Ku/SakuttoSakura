//
// BlockDestroyState.cs
// 
// 2026/06/07 Created By Man-Yi, Yeh
// 2026/06/08 Updated By Man-Yi, Yeh
// 2026/06/10 Updated By Man-Yi, Yeh
// 2026/06/25 Updated By Man-Yi, Yeh
// 2026/06/30 Updated By Man-Yi, Yeh
// 

using System.Threading;
using UnityEngine;

public class BlockDestroyState : IBlockState
{
    private float timer = 0.8f;
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

    }

    public override void StateEnd()
    {
        m_Block.SetAnimation("Destroy", false);
    }

    public override void StateUpdate()
    {
        //test
        m_Block.blockTest.trigger = m_Trigger;
        if (m_Trigger)
        {
            //end destroy
            m_Block.BlockOb.SetActive(false);
            m_Block.DestroyStrategy.DestroyEnd(m_Block);
            return;
        }

        //test
        timer-= Time.deltaTime;
        if (timer <= 0)
        {
            //end destroy
            m_Block.DestroyStrategy.DestroyEnd(m_Block);
            return;
        }
}
}
