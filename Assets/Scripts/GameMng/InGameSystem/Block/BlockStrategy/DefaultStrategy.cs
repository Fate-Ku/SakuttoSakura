using UnityEngine;

public class DefaultStrategy : IBlockStrategy
{
    public override void Do(IBlock block)
    {
        Debug.Log("DefaultStrategy Do: " + block.Type.ToString());
    }
}
