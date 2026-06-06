using UnityEngine;

public class NormalCombineCheck : ICombineCheckStrategy
{
    public override void Do(IBlock block, CombineSetsController controller)
    {
        Debug.Log("normal combine check");
    }
}
