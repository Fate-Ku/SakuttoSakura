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
        GameMng.Instance.ShowState();
        //m_InGameSystem.TestInGameStateText.text = "Time Up";
    }

    public override void StateUpdate()
    {
        if (!m_InGameSystem.IsAllBlocksIdle())
        {
            //game basic update
            m_InGameSystem.GameRun();
        }
        else
        {
            if (m_InGameSystem.CheckLevelUp())
            {
                //go level up state
                m_Controller.SetState(new InGameSystemLevelUpState(m_InGameSystem, m_Controller));
                return;
            }
            if (m_InGameSystem.GetGameTime() > 0)
            {
                //go play state
                m_Controller.SetState(new InGameSystemPlayState(m_InGameSystem, m_Controller));
                return;
            }

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
