//
// FlowerBlock.cs
// 
// 2026/05/30 Created By Man-Yi, Yeh
// 2026/06/04 Updated By Man-Yi, Yeh
// 2026/06/10 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class FlowerBlock : IBlock
{
    public FlowerBlock(
        BlockType type, GameObject block, float size, 
        bool isCreate = false) 
        : base(type, block, size, isCreate)
    {
        m_FallController = new NormalFallController(this);

        m_CombineCheckStartegy = new NormalCombineCheck();
        m_DestroyStrategy = new NormalDestroy();
        m_NearDestroyStrategy = new DefaultStrategy();
    }

}
