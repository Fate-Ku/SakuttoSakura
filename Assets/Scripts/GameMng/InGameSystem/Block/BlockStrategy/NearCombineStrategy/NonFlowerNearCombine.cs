//
// NonFlowerNearCombine.cs
// 
// 2026/06/11 Created By Man-Yi, Yeh
// 2026/06/12 Updated By Man-Yi, Yeh
// 


using UnityEngine;

public class NonFlowerNearCombine : INearCombineStrategy
{
    public override void NearDestroy(IBlock onerBlock, IBlock destoryBlock)
    {
        onerBlock.GoState(BlockStateType.Destroy);
    }
}
