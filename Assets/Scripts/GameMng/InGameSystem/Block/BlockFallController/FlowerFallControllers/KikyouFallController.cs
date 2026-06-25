//
// KikyouFallController.cs
// 
// 2026/06/25 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class KikyouFallController : FlowerFallController
{
    public KikyouFallController(IBlock block, float speed) 
        : base(block, speed)
    {
        m_FallStrategys.Add(new LeftFall(speed));
        m_FallStrategys.Add(new LeftFall(speed));
        m_FallStrategys.Add(new LeftFall(speed));
        m_FallStrategys.Add(new LeftFall(speed));
        m_FallStrategys.Add(new LeftFall(speed));
        m_FallStrategys.Add(new LeftFall(speed));

        //test
        block.blockTest.controllerName = "Kikyou";
    }
}
