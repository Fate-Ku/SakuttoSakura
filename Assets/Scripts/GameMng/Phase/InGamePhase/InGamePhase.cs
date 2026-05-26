//
// InGamePhase.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
//

using UnityEngine;

public class InGamePhase : Phase
{
    public InGamePhase(GameMng gameMng) 
        : base(gameMng)
    {
    }

    public override void Init()
    {
        GameMng.Instance.InGameSystemInit();
    }

    public override void Term()
    {
        GameMng.Instance.InGameSystemTerm();
    }

    public override void Update()
    {
        GameMng.Instance.InGameSystemUpdate();
    }

    
}
