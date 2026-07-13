//
// MenuState.cs
// 
// 2026/05/19 Created By Man-Yi, Yeh
// 2026/05/26 Updated By Man-Yi, Yeh 
// 2026/05/30 Updated By Man-Yi, Yeh
// 2026/05/31 Updated By Man-Yi, Yeh
//

using UnityEngine;

public class MenuState : IGameSceneState
{
    public MenuState(SceneStateController controller) 
        : base(controller)
    {
        StateName = "MenuState";
    }

    public override void StateUpdate()
    {
        GameMng.Instance.Update();
        ControllSceneByGameMng();
    }
}
