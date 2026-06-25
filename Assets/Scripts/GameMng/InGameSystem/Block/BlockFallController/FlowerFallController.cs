//
// FlowerFallController.cs
// 
// 2026/06/25 Created By Man-Yi, Yeh
// 


using UnityEngine;

public class FlowerFallController : NormalFallController
{
    public FlowerFallController(IBlock block, float speed) 
        : base(block, speed)
    {
        m_IsResetFallController = true;
    }

    
}
