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
        m_FallStrategys.Add(new RightFall(speed));
        m_FallStrategys.Add(new RightFall(speed));
        m_FallStrategys.Add(new RightFall(speed));
        m_FallStrategys.Add(new RightFall(speed));
        m_FallStrategys.Add(new RightFall(speed));
        m_FallStrategys.Add(new RightFall(speed));

        //test
        block.blockTest.controllerName = "Asagao";
    }
}
