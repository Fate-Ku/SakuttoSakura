//
// BlockCreateState.cs
// 
// 2026/06/10 Created By Man-Yi, Yeh
// 2026/06/25 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class BlockCreateState : IBlockState
{
    private float timer = 0.5f;
    private float size;

    public BlockCreateState(IBlock block, BlockStateController controller) 
        : base(block, controller)
    {
        StateType = BlockStateType.Create;
        StateName = "BlockCreateState";

        size = m_Block.BlockOb.transform.localScale.x;
    }

    public override void StateBegin()
    {
        m_Block.SetAnimation("Create", true);
    }

    public override void StateEnd()
    {
        //adjust size
        Vector3 scale = m_Block.BlockOb.transform.localScale;
        scale.x = size;
        scale.y = size;

        m_Block.BlockOb.transform.localScale = scale;
        m_Block.SetAnimation("Create", false);

        //test
        m_Block.blockTest.trigger = m_Trigger;
    }

    public override void StateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (m_Block.IsGoFall(FallDirection.Down))
            {
                //go fall
                m_Controller.SetState(new BlockFallState(m_Block, m_Controller));
                return;
            }
            else
            {
                //go idle
                m_Controller.SetState(new BlockIdleState(m_Block, m_Controller));
                return;
            }
        }

        //adjust size
        Vector3 scale = m_Block.BlockOb.transform.localScale;
        float rate = 0.5f + (0.5f * (1 - timer));
        scale.x = size * rate;
        scale.y = size * rate;

        m_Block.BlockOb.transform.localScale = scale;
    }
}
