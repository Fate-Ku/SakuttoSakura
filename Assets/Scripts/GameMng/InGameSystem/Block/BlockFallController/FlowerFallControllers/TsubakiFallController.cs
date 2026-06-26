//
// TsubakiFallController.cs
// 
// 2026/06/25 Created By Man-Yi, Yeh
// 


using UnityEngine;

public class TsubakiFallController : FlowerFallController
{
    public TsubakiFallController(IBlock block, float speed) 
        : base(block, speed)
    {
        m_FallStrategys.Add(new DownFall(speed));
        m_FallStrategys.Add(new DownFall(speed));
        m_FallStrategys.Add(new RightFall(speed));
        m_FallStrategys.Add(new DownFall(speed));
        m_FallStrategys.Add(new DownFall(speed));
        m_FallStrategys.Add(new DownFall(speed));
        m_FallStrategys.Add(new LeftFall(speed));

        //test
        block.blockTest.controllerName = "Tsubaki";
    }
}
