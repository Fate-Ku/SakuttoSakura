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

    public override void StateBegin()
    {
        GameMng.Instance.SetPhase(GameMng.PhaseType.InGame);
    }

    public override void StateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            //change to ScoreState
            m_Controller.SetState(new ScoreState(m_Controller), "ScoreScene");
            return;
        }

        GameMng.Instance.Update();

    }
}
