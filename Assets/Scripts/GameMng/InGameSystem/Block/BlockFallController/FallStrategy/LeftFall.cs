//
// DownFall.cs
// 
// 2026/06/24 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class LeftFall : IFallStrategy
{
    public LeftFall(float speed) 
        : base(FallDirection.Left, speed)
    {
    }
}
