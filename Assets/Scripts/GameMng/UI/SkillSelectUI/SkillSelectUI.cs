//
// SkillSelectUI.cs
// 
// 2026/05/31 Created By Fate Ku
// 2026/06/02 Updated By Fate Ku
//
using UnityEngine;

public class SkillSelectUI : UISystem
{
    public SkillSelectUI(GameMng gameMng)
        : base(gameMng)
    {
    }

    public override void Init()
    {
        Debug.Log("SkillSelectUI Init");
        
    }

    public override void Update()
    {
      
    }

    public override void Term()
    {
        Debug.Log("SkillSelectUI Term");
    }
}