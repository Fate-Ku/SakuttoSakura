//
// ScoreState.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
//

using UnityEngine;

public class ScoreState : ISceneState
{
    public ScoreState(SceneStateController controller) 
        : base(controller)
    {
        this.StateName = "ScoreState";
    }

    public override void StateBegin()
    {
        GameMng.Instance.SetPhase(GameMng.PhaseType.Score);
    }

    public override void StateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            //change to MenuState
            m_Controller.SetState(new MenuState(m_Controller), "MenuScene");
            return;
        }

    }
}
