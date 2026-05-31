//
// ScoreUI.cs
// 
// 2026/05/31 Created By Fate Ku
//
//
using UnityEngine;

public class ScoreUI : IGameSystem
{
    public ScoreUI(GameMng gameMng)
        : base(gameMng)
    {
    }

    public override void Init()
    {
        Debug.Log("ScoreUI Init");
        
    }

    public override void Update()
    {
        
    }

    public override void Term()
    {
        Debug.Log("ScoreUI Term");
        
    }
}