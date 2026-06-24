//
// NormalFallController.cs
// 
// 2026/06/04 Created By Man-Yi, Yeh
// 2026/06/16 Updated By Man-Yi, Yeh
// 2026/06/17 Updated By Man-Yi, Yeh
// 2026/06/22 Updated By Man-Yi, Yeh
// 2026/06/23 Updated By Man-Yi, Yeh
// 2026/06/24 Updated By Man-Yi, Yeh
// 


using UnityEngine;

public class NormalFallController : IBlockFallController
{
    public NormalFallController(IBlock block, float speed)
        : base(block)
    {
        m_FallStrategys.Add(new DownFall(speed));
        m_BasicSpeed = speed;

        //test
        block.blockTest.controllerName = "Normal";
    }

    protected override bool CanNextFall()
    {
        bool res = false;

        //set next ID
        GoNextFallID();
        //check next can fall
        if (m_Block != null)
        {
            res = m_Block.IsGoFall(m_FallStrategys[m_NowFallStrategyID].Direction);
        }

        return res;
    }
}
