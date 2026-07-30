//
// FlowerFallController.cs
// 
// 2026/06/25 Created By Man-Yi, Yeh
// 2026/07/02 Updated By Man-Yi, Yeh
// 

using System;
using UnityEngine;


public class FlowerFallController : NormalFallController
{
    public FlowerFallController(IBlock block, FallData fallData)
        : base(block,fallData.basicSpeed)
    {
        m_IsResetFallController = true;

        foreach (var pathData in fallData.pathDatas) 
        {
            switch (pathData.direction)
            {
                case FallDirection.Down:
                    m_FallStrategys.Add(new DownFall(pathData.speed));
                    break;

                case FallDirection.Left:
                    m_FallStrategys.Add(new LeftFall(pathData.speed));
                    break;

                case FallDirection.Right:
                    m_FallStrategys.Add(new RightFall(pathData.speed));
                    break;

                default:
                    break;
            }
        }

        block.blockTest.controllerName = "Flower";
    }
}
