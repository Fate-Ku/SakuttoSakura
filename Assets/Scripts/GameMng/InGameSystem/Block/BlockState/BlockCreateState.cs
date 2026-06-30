//
// BlockCreateState.cs
// 
// 2026/06/10 Created By Man-Yi, Yeh
// 2026/06/25 Updated By Man-Yi, Yeh
// 2026/06/26 Updated By Man-Yi, Yeh
// 2026/06/30 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class BlockCreateState : IBlockState
{
    private float timer = 0.8f;
    //private float size;

    public BlockCreateState(IBlock block, BlockStateController controller) 
        : base(block, controller)
    {
        StateType = BlockStateType.Create;
        StateName = "BlockCreateState";

        //size = m_Block.BlockOb.transform.localScale.x;
    }

    public override void StateBegin()
    {
        m_Block.FallController.ResetlFallController();
        m_Block.SetAnimation("Create", true);
    }

    public override void StateEnd()
    {
        m_Block.SetAnimation("Create", false);

        //test
        m_Block.blockTest.trigger = m_Trigger;
    }

    public override void StateUpdate()
    {
        m_Block.blockTest.trigger = m_Trigger;
        if (m_Trigger)
        {
            //go idle
            m_Controller.SetState(new BlockIdleState(m_Block, m_Controller));
            return;
        }

        //test
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            //go idle
            m_Controller.SetState(new BlockIdleState(m_Block, m_Controller));
        }

        
    }
}
