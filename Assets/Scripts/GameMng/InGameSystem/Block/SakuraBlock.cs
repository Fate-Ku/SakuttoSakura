//
// SakuraBlock.cs
// 
// 2026/06/24 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class SakuraBlock : FlowerBlock
{
    public SakuraBlock(GameObject block, BlockType type, float size, float speed, bool isCreate = false) 
        : base(block, type, size, speed, isCreate)
    {
        m_FallController = new SakuraFallController(this, speed);
    }
}
