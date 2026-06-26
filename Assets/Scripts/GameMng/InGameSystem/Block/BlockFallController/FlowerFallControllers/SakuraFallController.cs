//
// SakuraFallController.cs
// 
// 2026/06/24 Created By Man-Yi, Yeh
// 2026/06/25 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class SakuraFallController : FlowerFallController
{
    public SakuraFallController(IBlock block, float speed) 
        : base(block, speed)
    {
        /*
        m_FallStrategys.Add(new DownFall(speed));
        m_FallStrategys.Add(new LeftFall(speed));
        m_FallStrategys.Add(new LeftFall(speed));
        m_FallStrategys.Add(new DownFall(speed * 0.85f));
        m_FallStrategys.Add(new LeftFall(speed * 0.85f));
        */
        m_FallStrategys.Add(new LeftFall(speed));
        m_FallStrategys.Add(new LeftFall(speed));
        m_FallStrategys.Add(new LeftFall(speed));
        m_FallStrategys.Add(new LeftFall(speed));
        m_FallStrategys.Add(new LeftFall(speed));
        m_FallStrategys.Add(new LeftFall(speed));

        //test
        block.blockTest.controllerName = "Sakura";
    }
}
