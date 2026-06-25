//
// CloverFallController.cs
// 
// 2026/06/24 Created By Man-Yi, Yeh
// 2026/06/25 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class CloverFallController : FlowerFallController
{
    public CloverFallController(IBlock block, float speed) 
        : base(block, speed)
    {
        m_FallStrategys.Add(new RightFall(speed));
        m_FallStrategys.Add(new RightFall(speed));
        m_FallStrategys.Add(new RightFall(speed));
        m_FallStrategys.Add(new RightFall(speed));
        m_FallStrategys.Add(new RightFall(speed));
        m_FallStrategys.Add(new RightFall(speed));

        //test
        block.blockTest.controllerName = "Clover";
    }
}
