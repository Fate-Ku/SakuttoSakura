//
// SkillSelectState.cs
// 
// 2026/05/21 Created By Man-Yi, Yeh
//
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class SkillSelectState : ISceneState
{
    public SkillSelectState(SceneStateController controller)
        : base(controller)
    {
        this.StateName = "SkillSelectState";
    }

    public override void StateBegin()
    {
        GameMng.Instance.SetPhase(GameMng.PhaseType.SkillSelect);
    }

    public override void StateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //change to InGameState
            m_Controller.SetState(new InGameState(m_Controller), "InGameScene");
        }

        GameMng.Instance.Update();
    }
}
