//
// TogetherLeftController.cs
// 
// 2026/06/24 Created By Man-Yi, Yeh
// 2026/06/25 Updated By Man-Yi, Yeh
// 


using UnityEngine;

public class TogetherLeftFallController : IBlockFallController
{
    public TogetherLeftFallController(IBlock block, float speed, float basicSpeed, Vector2 targetPos) 
        : base(block)
    {
        Debug.Log("left together speed" + speed.ToString() + " " + block.Type.ToString());
        m_IsResetFallController = true;
        m_FallStrategys.Add(new LeftFall(speed));
        m_BasicSpeed = basicSpeed;
        m_FallStrategys[0].TargetPos = targetPos;

        //test
        block.blockTest.controllerName = "TogetherLeft";
    }

    protected override bool CanNextFall()
    {
        return false;
    }
}
