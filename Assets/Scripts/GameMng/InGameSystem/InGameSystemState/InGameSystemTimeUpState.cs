//
// InGameSystemTimeUpState.cs
// 
// 2026/06/10 Created By Man-Yi, Yeh
// 

using System.Threading;
using UnityEngine;

public class InGameSystemTimeUpState : IInGameSystemState
{
    private float timer;

    public InGameSystemTimeUpState(InGameSystem inGameSystem, InGameStateController controller) 
        : base(inGameSystem, controller)
    {
        StateType = InGameSystemStateType.TimeUp;
        StateName = "InGameSystemTimeUpState";
    }

    public override void StateBegin()
    {
        timer = m_InGameSystem.GameInfo.GetTimeUpTime();
        m_InGameSystem.TestInGameStateText.text = "Time Up";
    }

    public override void StateUpdate()
    {
        if (!m_InGameSystem.IsAllBlocksIdle())
        {
            m_InGameSystem.GameRun();
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //go game over state
                m_Controller.SetState(new InGameSystemGameOverState(m_InGameSystem, m_Controller));
                return;
            }
        }
    }
}
