//
// SkillSelectUI.cs
// 
// 2026/05/31 Created By Fate Ku
// 2026/06/02 Updated By Fate Ku
//
using UnityEngine;

public class SkillSelectUI : UISystem
{
    private SkilliconUI skilliconUI;

    public SkillSelectUI(GameMng gameMng)
        : base(gameMng)
    {
        skilliconUI = new SkilliconUI();

    }

    public override void Init()
    {
        skilliconUI.Init();

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