//
// BlockRiseState.cs
// 
// 2026/06/12 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class BlockRiseState : IBlockState
{
    public BlockRiseState(IBlock block, BlockStateController controller) 
        : base(block, controller)
    {
        StateType = BlockStateType.Rise;
        StateName = "BlockRiseState";
    }

    public override void StateBegin()
    {
        Debug.Log("test block start rise: " + m_Block.Type.ToString());

        //remove combine set
        m_Block.RemoveCombineSet();

    }

    public override void StateUpdate()
    {
        m_Block.RiseController.RiseUpdate();
        if (!m_Block.RiseController.IsRising)
        {
            m_Controller.SetState(new BlockIdleState(m_Block, m_Controller));
        }

        //test
        m_Block.blockTest.trigger = m_Trigger;
    }

    public override void BeDestroyed()
    {
        m_Block.GoDestroy();
    }
}
