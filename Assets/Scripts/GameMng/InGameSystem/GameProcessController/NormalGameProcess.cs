//
// NormalGameProcess.cs
// 
// 2026/06/29 Created By Man-Yi, Yeh
// 2026/06/30 Updated By Man-Yi, Yeh
// 2026/07/06 Updated By Man-Yi, Yeh
// 2026/07/07 Updated By Man-Yi, Yeh
// 2026/07/13 Updated By Man-Yi, Yeh
// 2026/07/28 Updated By Man-Yi, Yeh
// 

using UnityEngine;


public class NormalGameProcess : IGameProcessController
{
    public NormalGameProcess(InGameSystem inGameSystem, string processData, string eventData, string bgmData, int level)
        : base(inGameSystem, processData, eventData, bgmData, level)
    {
    }
}