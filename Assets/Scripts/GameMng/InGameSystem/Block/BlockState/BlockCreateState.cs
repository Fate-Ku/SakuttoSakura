//
// BlockCreateState.cs
// 
// 2026/06/10 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class BlockCreateState : IBlockState
{
    private float timer = 1;
    private float size;

    public BlockCreateState(IBlock block, BlockStateController controller) 
        : base(block, controller)
    {
        StateType = BlockStateType.Create;
        StateName = "BlockCreateState";

        size = m_Block.BlockOb.transform.localScale.x;
    }

    public override void StateEnd()
    {
        //adjust size
        Vector3 scale = m_Block.BlockOb.transform.localScale;
        scale.x = size;
        scale.y = size;

        m_Block.BlockOb.transform.localScale = scale;
    }

    public override void StateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (m_Block.IsGoFall())
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
