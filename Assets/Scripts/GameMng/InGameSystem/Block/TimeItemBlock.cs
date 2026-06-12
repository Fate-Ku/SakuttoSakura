//
// TimeItemBlock.cs
// 
// 2026/06/11 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class TimeItemBlock : IBlock
{
    public TimeItemBlock(GameObject block, float size, bool isCreate = false) 
        : base(BlockType.TimeItem, block, size, isCreate)
    {
        m_FallController = new NormalFallController(this);

        m_CombineCheckStartegy = new NormalCombineCheck();
        m_DestroyStrategy = new TimeItemDestroy();
        m_NearCombineStrategy = new NonFlowerNearCombine();
    }
}
