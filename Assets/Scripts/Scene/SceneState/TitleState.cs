//
// TitleState.cs
// 
// 2026/05/19 Created By Man-Yi, Yeh
//

using System.Threading;
using UnityEngine;

public class TitleState : ISceneState
{
    private float timer = 0;

    public TitleState(SceneStateController controller) 
        : base(controller)
    {
        this.StateName = "StartState";
    }

    //update
    public override void StateUpdate() 
    {
        timer += Time.deltaTime;

        if (timer >= 1)
        {
            //change to MenuState
            m_Controller.SetState(new MenuState(m_Controller), "MenuScene");
        }
    }
}
