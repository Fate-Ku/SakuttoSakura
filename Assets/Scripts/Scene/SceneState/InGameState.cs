//
// InGameState.cs
// 
// 2026/05/19 Created By Man-Yi, Yeh
// 2026/05/31 Updated By Man-Yi, Yeh
//

using UnityEngine;

public class InGameState : IGameSceneState
{
    public InGameState(SceneStateController controller, bool isTGS) 
        : base(controller, isTGS)
    {
        StateName = "InGameState";
    }

    public override void StateBegin()
    {
        GameMng.Instance.SetPhase(GameMng.PhaseType.InGame, m_IsTGS);

        BGMMng.Instance.SetBGM(BGMType.BLoop, true);
    }

    public override void StateEnd()
    {
        GameMng.Instance.EndPhase();

        BGMMng.Instance.PauseBGM();
    }

    public override void StateUpdate()
    {
        GameMng.Instance.Update();
        ControllSceneByGameMng(); 
    }
}
