//
// TimeItemBlock.cs
// 
// 2026/06/11 Created By Man-Yi, Yeh
// 2026/06/16 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class TimeItemBlock : IBlock
{
    public TimeItemBlock(GameObject block, float size, bool isCreate = false) 
        : base(block, BlockType.TimeItem, size, isCreate)
    {
        m_FallController = new NormalFallController(this, 2.5f);

        m_CombineStartegy = new NormalCombine();
        m_DestroyStrategy = new TimeItemDestroy();
        m_NearCombineStrategy = new NonFlowerNearCombine();
    }
}
