//
// TogetherDownController.cs
// 
// 2026/06/23 Created By Man-Yi, Yeh
// 2026/06/24 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class TogetherDownFallController : IBlockFallController
{
    public TogetherDownFallController(IBlock block, float speed, float basicSpeed, Vector2 targetPos) 
        : base(block)
    {
        Debug.Log("down together speed" + speed.ToString() + " " + block.Type.ToString());
        m_FallStrategys.Add(new DownFall(speed));
        m_BasicSpeed = basicSpeed;
        m_FallStrategys[0].TargetPos = targetPos;

        //test
        block.blockTest.controllerName = "TogetherDown";
    }

    public override void ResetlFallController()
    {
        m_Block.SetFallController(new NormalFallController(m_Block, m_BasicSpeed));
    }
}
