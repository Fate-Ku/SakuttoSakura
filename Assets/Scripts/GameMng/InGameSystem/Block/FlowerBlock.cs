//
// FlowerBlock.cs
// 
// 2026/05/30 Created By Man-Yi, Yeh
// 2026/06/04 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class FlowerBlock : IBlock
{
    public FlowerBlock(GameObject block, float size) 
        : base(block, size)
    {
        m_FallController = new NormalFallController(this);

        m_CombineCheckStartegy = new NormalCombineCheck();
        m_DestroyStrategy = new NormalDestroy();
        m_NearDestroyStrategy = new DefaultStrategy();
    }

}
