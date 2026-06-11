//
// NormalCombineCheck.cs
// 
// 2026/06/07 Created By Man-Yi, Yeh
//


using UnityEngine;

public class NormalCombineCheck : ICombineCheckStrategy
{
    public override void DoCombine(IBlock onerBlock, CombineSetsController controller)
    {
        for (int i = 0; i < (int)BlockNearPos.Count; ++i)
        {
            BlockNode nearBlockNode = onerBlock.GetNearNode((BlockNearPos)i);
            IBlock nearBlock = nearBlockNode?.Block;
            //call near block's BeCombinedCheck
            nearBlock?.BeCombinedCheck(onerBlock, controller);
        }
    }

    public override void BeCombined(IBlock nearBlock, IBlock onerBlock, CombineSetsController controller)
    {
        if (nearBlock.Type == onerBlock.Type)
        {
            controller.CreateCombineSet(nearBlock.Type, nearBlock, onerBlock);
        }
    }

}
