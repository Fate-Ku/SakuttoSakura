//
// InGameSystemGameOverState
// 
// 2026/06/10 Created By Man-Yi, Yeh
// 2026/07/02 Updated By Fate Ku
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
        GameMng.Instance.ShowState();
        //m_InGameSystem.TestInGameStateText.text = "Game Over";
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
