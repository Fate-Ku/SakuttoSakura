//
// TutorialState.cs
// 
// 2026/07/13 Created By Man-Yi, Yeh
//

using UnityEngine;

public class TutorialState : IGameSceneState
{
    public TutorialState(SceneStateController controller) 
        : base(controller)
    {
        StateName = "TutorialState";
    }

    public override void StateBegin()
    {
        GameMng.Instance.SetPhase(GameMng.PhaseType.Tutorial);

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
