//
// SoftRockBlock.cs
// 
// 2026/06/12 Created By Man-Yi, Yeh
// 2026/06/16 Updated By Man-Yi, Yeh
// 2026/06/23 Updated By Man-Yi, Yeh
// 2026/06/25 Updated By Man-Yi, Yeh
// 2026/07/05 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class SoftRockBlock:IBlock
{
    public SoftRockBlock(GameObject blockOb, float size, float speed) 
        : base(blockOb, size, BlockType.SoftRock)
    {
        m_FallController = new NormalFallController(this, speed);

        m_CombineStartegy = new NormalCombine();
        m_DestroyStrategy = new NormalDestroy();
        m_NearCombineStrategy = new NonFlowerNearCombine();
    }
}
