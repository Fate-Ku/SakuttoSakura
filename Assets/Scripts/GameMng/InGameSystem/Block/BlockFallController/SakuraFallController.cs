//
// SakuraFallController.cs
// 
// 2026/06/24 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class SakuraFallController : NormalFallController
{
    public SakuraFallController(IBlock block, float speed) 
        : base(block, speed)
    {
        m_FallStrategys.Add(new LeftFall(speed));

        //test
        block.blockTest.controllerName = "Sakura";
    }
}
