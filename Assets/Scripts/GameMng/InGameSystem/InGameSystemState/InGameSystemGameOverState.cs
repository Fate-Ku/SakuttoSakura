//
// InGameSystemGameOverState
// 
// 2026/06/10 Created By Man-Yi, Yeh
// 2026/07/02 Updated By Fate Ku
// 2026/07/03 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class InGameSystemGameOverState : IInGameSystemState
{
    private float timer;

    public InGameSystemGameOverState(InGameSystem inGameSystem, InGameStateController controller) 
        : base(inGameSystem, controller)
    {
        StateType = InGameSystemStateType.GameOver;
        StateName = "InGameSystemGameOverState";
    }

    public override void StateBegin()
    {
        timer = m_InGameSystem.GameInfo.GetGameOverTime();

        //start State UI
        GameMng.Instance.ShowStateUI(m_StateType);

    }

    public override void StateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            m_InGameSystem.IsGameEnd = true;
        }
    }
}
