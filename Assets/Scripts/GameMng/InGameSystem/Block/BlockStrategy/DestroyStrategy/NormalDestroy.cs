//
// NormalDestroy.cs
// 
// 2026/06/07 Created By Man-Yi, Yeh
// 2026/06/08 Updated By Man-Yi, Yeh
// 2026/06/11 Updated By Man-Yi, Yeh
// 2026/06/30 Updated By Man-Yi, Yeh
//

using UnityEngine;
using static Unity.Collections.AllocatorManager;


public class NormalDestroy : IDestroyStrategy
{
    public override void DestroyStart(IBlock onerBlock)
    {
        Debug.Log("DestroyStart");
    }

    public override void DestroyEnd(IBlock onerBlock)
    {
        Debug.Log("DestroyEnd");

        BlockDestory(onerBlock);

    }
}
