//
// AsagaoFallController.cs
// 
// 2026/06/25 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class AsagaoFallController : FlowerFallController
{
    public AsagaoFallController(IBlock block, float speed) 
        : base(block, speed)
    {
        m_FallStrategys.Add(new DownFall(speed));
        m_FallStrategys.Add(new LeftFall(speed * 1.1f));
        m_FallStrategys.Add(new LeftFall(speed * 1.2f));
        m_FallStrategys.Add(new DownFall(speed * 1.3f));
        m_FallStrategys.Add(new RightFall(speed * 1.15f));

        //test
        block.blockTest.controllerName = "Asagao";
    }
}
