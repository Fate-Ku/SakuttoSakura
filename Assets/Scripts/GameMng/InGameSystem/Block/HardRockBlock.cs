//
// HardRockBlock.cs
// 
// 2026/06/12 Created By Man-Yi, Yeh
// 2026/06/16 Updated By Man-Yi, Yeh
// 2026/06/23 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class HardRockBlock : IBlock
{
    public HardRockBlock(InGameSystem inGameSystem, GameObject block, float size, float speed, 
        bool isCreate = false)
        : base(block, BlockType.HardRock, size, isCreate)
    {
        m_FallController = new NormalFallController(this, speed);

        m_CombineStartegy = new NormalCombine();
        m_DestroyStrategy = new NormalDestroy();
        m_NearCombineStrategy = new NonFlowerNearCombine();

        SetCreateBlock(inGameSystem.CreateBlock(BlockType.SoftRock, true));
    }
}
