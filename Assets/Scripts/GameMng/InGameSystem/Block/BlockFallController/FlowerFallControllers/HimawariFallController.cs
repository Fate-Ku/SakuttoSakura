//
// HimawariFallController.cs
// 
// 2026/06/25 Created By Man-Yi, Yeh
// 


using UnityEngine;

public class HimawariFallController : FlowerFallController
{
    public HimawariFallController(IBlock block, float speed) 
        : base(block, speed)
    {
        m_FallStrategys.Add(new DownFall(speed));
        m_FallStrategys.Add(new RightFall(speed * 1.5f));
        m_FallStrategys.Add(new RightFall(speed * 1.5f));
        m_FallStrategys.Add(new DownFall(speed));
        m_FallStrategys.Add(new DownFall(speed));
        m_FallStrategys.Add(new LeftFall(speed * 0.6f));
        m_FallStrategys.Add(new LeftFall(speed * 0.6f));

        //test
        block.blockTest.controllerName = "Himawari";
    }
}
