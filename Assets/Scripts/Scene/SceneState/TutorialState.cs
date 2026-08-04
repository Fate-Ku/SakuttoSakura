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

        GameMng.Instance.SetBGM(BGMType.BLoop);
        //GameMng.Instance.SetNextBGM(BGMType.BLoop);
        GameMng.Instance.SetNextBGM(BGMType.None);
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
