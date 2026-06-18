//
// DownFall.cs
// 
// 2026/06/18 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class DownFall : IFallStrategy
{
    public DownFall(float speed) 
        : base(FallDirection.Down, speed)
    {
    }


}
