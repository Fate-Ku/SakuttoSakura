//
// SkillSelectPhase.cs
// 
// 2026/05/21 Update By Man-Yi, Yeh 
// 

using UnityEngine;

public class SkillSelectPhase : Phase
{
    public SkillSelectPhase(GameMng gameMng) 
        : base(gameMng)
    {
    }

    public override void Update() 
    {
        Debug.Log("skill select update");
    }
}
