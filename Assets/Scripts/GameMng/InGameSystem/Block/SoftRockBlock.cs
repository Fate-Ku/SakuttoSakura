//
// SoftRockBlock.cs
// 
// 2026/06/12 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class SoftRockBlock:IBlock
{
    public SoftRockBlock(GameObject block, float size, bool isCreate = false) 
        : base(BlockType.SoftRock, block, size, isCreate)
    {
        m_FallController = new NormalFallController(this);

        m_CombineStartegy = new NormalCombine();
        m_DestroyStrategy = new NormalDestroy();
        m_NearCombineStrategy = new NonFlowerNearCombine();
    }
}
