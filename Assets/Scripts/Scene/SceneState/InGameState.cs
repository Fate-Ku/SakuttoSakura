//
// InGameState.cs
// 
// 2026/05/19 Created By Man-Yi, Yeh
// 2026/05/31 Updated By Man-Yi, Yeh
//

using UnityEngine;

public class InGameState : IGameSceneState
{
    public InGameState(SceneStateController controller) 
        : base(controller)
    {
        this.StateName = "InGameState";
    }

    public override void StateBegin()
    {
        GameMng.Instance.SetPhase(GameMng.PhaseType.InGame);

        GameMng.Instance.SetBGM(BGMType.BLoop);
        GameMng.Instance.SetNextBGM(BGMType.BLoop);
    }

    public override void StateEnd()
    {
        GameMng.Instance.EndPhase();
        GameMng.Instance.PauseBGM();
    }

    public override void StateUpdate()
    {
        GameMng.Instance.Update();
        ControllSceneByGameMng(); 
    }
}
