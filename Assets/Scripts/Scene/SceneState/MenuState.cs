//
// MenuState.cs
// 
// 2026/05/19 Created By Man-Yi, Yeh
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
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            //change to InGameState
            m_Controller.SetState(new SkillSelectState(m_Controller), "SkillSelectScene");
        }
    }
}
