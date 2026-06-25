//
// KaedeFallController.cs
// 
// 2026/06/25 Created By Man-Yi, Yeh
// 


using UnityEngine;

public class KaedeFallController : FlowerFallController
{
    public KaedeFallController(IBlock block, float speed) 
        : base(block, speed)
    {
        //test
        block.blockTest.controllerName = "Kaede";
    }
}
