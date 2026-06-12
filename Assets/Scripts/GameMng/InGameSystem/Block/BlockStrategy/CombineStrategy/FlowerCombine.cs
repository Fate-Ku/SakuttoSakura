//
// FlowerCombine.cs
// 
// 2026/06/07 Created By Man-Yi, Yeh
// 2026/06/11 Updated By Man-Yi, Yeh
// 2026/06/12 Updated By Man-Yi, Yeh
//


using UnityEngine;

public class FlowerCombine : ICombineStrategy
{
    public override void DoCombineCheck(IBlock onerBlock, CombineSetsController controller)
    {
        for (int i = 0; i < (int)BlockNearPos.Count; ++i)
        {
            BlockNode nearBlockNode = onerBlock.GetNearNode((BlockNearPos)i);
            IBlock nearBlock = nearBlockNode?.Block;
            //call near block's BeCombinedCheck
            nearBlock?.BeCombinedCheck(onerBlock, controller);
        }
    }

    public override void BeCombinedCheck(IBlock nearBlock, IBlock onerBlock, CombineSetsController controller)
    {
        if (nearBlock.Type == onerBlock.Type)
        {
            controller.CreateCombineSet(nearBlock.Type, nearBlock, onerBlock);
        }
    }

    public override void EndCombine(IBlock onerBlock)
    {
        onerBlock.GoDestroy();
    }
}
