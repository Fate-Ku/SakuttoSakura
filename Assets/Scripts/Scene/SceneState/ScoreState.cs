//
// ScoreState.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
// 2026/05/31 Updated By Man-Yi, Yeh
//

using UnityEngine;

public class ScoreState : IGameSceneState
{
    public ScoreState(SceneStateController controller, bool isTGS) 
        : base(controller, isTGS)
    {
        StateName = "ScoreState";
    }

    public override void StateBegin()
    {
        GameMng.Instance.SetPhase(GameMng.PhaseType.Score, m_IsTGS);

        BGMMng.Instance.SetBGM(BGMType.A2Loop);
        BGMMng.Instance.SetNextBGM(BGMType.Outro);
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
