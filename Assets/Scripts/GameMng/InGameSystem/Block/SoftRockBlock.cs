//
// SoftRockBlock.cs
// 
// 2026/06/12 Created By Man-Yi, Yeh
// 2026/06/16 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class SoftRockBlock:IBlock
{
    public SoftRockBlock(GameObject block, float size, bool isCreate = false) 
        : base(block, BlockType.SoftRock,size, isCreate)
    {
        m_FallController = new NormalFallController(this, 2.5f);

        m_CombineStartegy = new NormalCombine();
        m_DestroyStrategy = new NormalDestroy();
        m_NearCombineStrategy = new NonFlowerNearCombine();
    }
}
