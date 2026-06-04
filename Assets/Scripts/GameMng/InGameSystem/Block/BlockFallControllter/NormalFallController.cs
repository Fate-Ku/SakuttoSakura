//
// NormalFallController.cs
// 
// 2026/06/04 Created By Man-Yi, Yeh
// 


using UnityEngine;

public class NormalFallController : IBlockFallController
{
    public NormalFallController(IBlock block) 
        : base(block)
    {
    }

    public override void FallUpdate()
    {

    }
}
