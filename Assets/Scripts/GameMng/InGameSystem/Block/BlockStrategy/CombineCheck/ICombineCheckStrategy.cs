//
// ICombineCheckStrategy.cs
// 
// 2026/06/06 Created By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
//


using UnityEngine;

public abstract class ICombineCheckStrategy
{
    public abstract void DoCombine(IBlock onerBlock, CombineSetsController controller);

    //nearblock: block who call the check
    //block: oner
    public abstract void BeCombined(IBlock nearBlock, IBlock onerBlock, CombineSetsController controller);
}
