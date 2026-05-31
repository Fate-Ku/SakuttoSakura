//
// ScorePhase.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
// 2026/05/31 Updated By Man-Yi, Yeh
//

using UnityEngine;

public class ScorePhase : Phase
{
    public ScorePhase(GameMng gameMng) 
        : base(gameMng)
    {
    }

    public override void Update()
    {
        Debug.Log("score update");
    }
}
