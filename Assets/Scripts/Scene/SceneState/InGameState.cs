//
// InGameState.cs
// 
// 2026/05/19 Created By Man-Yi, Yeh
//

using UnityEngine;

public class InGameState : ISceneState
{
    public InGameState(SceneStateController controller) 
        : base(controller)
    {
        this.StateName = "InGameState";
    }

    public override void StateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //change to MenuState
            m_Controller.SetState(new MenuState(m_Controller), "MenuScene");
        }
    }
}
