//
// MenuState.cs
// 
// 2026/05/19 Created By Man-Yi, Yeh
// 2026/05/26 Update By Man-Yi, Yeh 
// 2026/05/30 Updated By Man-Yi, Yeh
//

using UnityEngine;

public class MenuState : ISceneState
{
    public MenuState(SceneStateController controller) 
        : base(controller)
    {
        this.StateName = "MenuState";
    }

    public override void StateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            //change to SkillSelectState
            m_Controller.SetState(new SkillSelectState(m_Controller), "SkillSelectScene");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            //change to InGameState
            m_Controller.SetState(new InGameState(m_Controller), "InGameScene");
        }
    }
}
