//
// SkillSelectState.cs
// 
// 2026/05/21 Created By Man-Yi, Yeh
// 2026/05/26 Updated By Man-Yi, Yeh 
// 2026/05/31 Updated By Man-Yi, Yeh
//
using UnityEngine;


public class SkillSelectState : IGameSceneState
{
    public SkillSelectState(SceneStateController controller, bool isTGS)
        : base(controller, isTGS)
    {
        StateName = "SkillSelectState";
    }

    public override void StateBegin()
    {
        GameMng.Instance.SetPhase(GameMng.PhaseType.SkillSelect, m_IsTGS);
    }

    public override void StateEnd()
    {
        GameMng.Instance.EndPhase();
    }

    public override void StateUpdate()
    {
        GameMng.Instance.Update();
        ControllSceneByGameMng();
    }
}
