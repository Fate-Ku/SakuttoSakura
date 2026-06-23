//
// TogetherDownController.cs
// 
// 2026/06/23 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class TogetherDownFallController : IBlockFallController
{
    public TogetherDownFallController(IBlock block, float speed, float basicSpeed, Vector2 targetPos) 
        : base(block, speed)
    {
        Debug.Log("down together speed" + speed.ToString() + " " + block.Type.ToString());
        m_BasicSpeed = basicSpeed;
        m_FallStrategys[0].TargetPos = targetPos;
    }

    public override void ResetlFallController()
    {
        m_Block.SetFallController(new NormalFallController(m_Block, m_BasicSpeed));
    }
}
