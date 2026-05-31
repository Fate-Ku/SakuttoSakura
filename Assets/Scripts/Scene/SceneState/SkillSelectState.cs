//
// SkillSelectState.cs
// 
// 2026/05/21 Created By Man-Yi, Yeh
// 2026/05/26 Updated By Man-Yi, Yeh 
// 2026/05/31 Updated By Man-Yi, Yeh
//
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class SkillSelectState : IGameSceneState
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

    public override void StateEnd()
    {
        GameMng.Instance.EndPhase();
    }

    public override void StateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            //change to InGameState
            m_Controller.SetState(new InGameState(m_Controller), "InGameScene");
        }

        GameMng.Instance.Update();
        ControllSceneByGameMng();
    }
}
