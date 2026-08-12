//
// IdleScreenState.cs
// 
// 2026/08/12 Created By Man-Yi, Yeh
//

using UnityEngine;

public class IdleScreenState : IGameSceneState
{
    public IdleScreenState(SceneStateController controller, bool isTGS) 
        : base(controller, isTGS)
    {
        StateName = "IdleScreenState";
    }

    public override void StateUpdate()
    {
        ControllSceneByGameMng();
    }
}
