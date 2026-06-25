//
// FlowerBlock.cs
// 
// 2026/05/30 Created By Man-Yi, Yeh
// 2026/06/04 Updated By Man-Yi, Yeh
// 2026/06/10 Updated By Man-Yi, Yeh
// 2026/06/16 Updated By Man-Yi, Yeh
// 2026/06/23 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class FlowerBlock : IBlock
{
    public FlowerBlock(
        GameObject block, BlockType type, float size, float speed) 
        : base(block, type, size)
    {
        m_FallController = type switch
        {
            BlockType.Tsubaki => new TsubakiFallController(this, speed),
            BlockType.Kaede => new KaedeFallController(this, speed),
            BlockType.Himawari => new HimawariFallController(this, speed),
            BlockType.Clover => new CloverFallController(this, speed),
            BlockType.Asagao => new AsagaoFallController(this, speed),
            BlockType.Kikyou => new KikyouFallController(this, speed),
            BlockType.Sakura => new SakuraFallController(this, speed),
            _ => new NormalFallController(this, speed),
        };

        m_CombineStartegy = new FlowerCombine();
        m_DestroyStrategy = new NormalDestroy();
        m_NearCombineStrategy = new NormalNearCombine();
    }


}
