//
// NormalFallController.cs
// 
// 2026/06/04 Created By Man-Yi, Yeh
// 2026/06/16 Updated By Man-Yi, Yeh
// 2026/06/17 Updated By Man-Yi, Yeh
// 2026/06/22 Updated By Man-Yi, Yeh
// 2026/06/23 Updated By Man-Yi, Yeh
// 


using UnityEngine;

public class NormalFallController : IBlockFallController
{
    public NormalFallController(IBlock block, float speed)
        : base(block, speed)
    {
    }
}
