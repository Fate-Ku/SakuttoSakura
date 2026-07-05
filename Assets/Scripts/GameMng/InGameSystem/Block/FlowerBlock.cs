//
// FlowerBlock.cs
// 
// 2026/05/30 Created By Man-Yi, Yeh
// 2026/06/04 Updated By Man-Yi, Yeh
// 2026/06/10 Updated By Man-Yi, Yeh
// 2026/06/16 Updated By Man-Yi, Yeh
// 2026/06/23 Updated By Man-Yi, Yeh
// 2026/07/02 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class FlowerBlock : IBlock
{
    public FlowerBlock(GameObject block, float size, BlockType type,  FallData fallData)
        :base(block, size, type)
    {
        m_FallController = new FlowerFallController(this, fallData);

        m_CombineStartegy = new FlowerCombine();
        m_DestroyStrategy = new NormalDestroy();
        m_NearCombineStrategy = new NormalNearCombine();
    }
}
