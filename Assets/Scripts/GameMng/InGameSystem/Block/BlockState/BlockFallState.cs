//
// BlockIdleState.cs
// 
// 2026/06/04 Created By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
// 2026/06/08 Updated By Man-Yi, Yeh
// 2026/06/10 Updated By Man-Yi, Yeh
// 2026/06/17 Updated By Man-Yi, Yeh
// 

using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class BlockFallState : IBlockState
{
    public BlockFallState(IBlock block, BlockStateController controller) 
        : base(block, controller)
    {
        StateType = BlockStateType.Fall;
        StateName = "BlockFallState";
    }

    public override void StateBegin()
    {
        Debug.Log("test block start fall: " + m_Block.Type.ToString());

        //remove combine set
        m_Block.RemoveCombineSet();

        //falling
        m_Block.FallController.SetFalling(true);
        m_Block.FallController.IsEndFall = false;
        //set target pos as below node's pos
        Vector2 pos = m_Block.GetNearNode(BlockNearPos.Below).Pos;
        m_Block.SetFallTargetPos(pos);
    }

    public override void StateUpdate()
    {
        m_Block.FallController.FallUpdate();
        if (m_Block.FallController.IsEndFall)
        {
            m_Controller.SetState(new BlockIdleState(m_Block, m_Controller));
        }
    }

    public override void BeDestroyed()
    {
        m_Block.GoDestroy();
    }
}
