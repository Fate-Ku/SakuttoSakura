//
// InGameSystemCheckEndState.cs
// 
// 2026/06/10 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class InGameSystemCheckEndState : IInGameSystemState
{
    public InGameSystemCheckEndState(InGameSystem inGameSystem, InGameStateController controller) 
        : base(inGameSystem, controller)
    {
        StateType = InGameSystemStateType.CheckEnd;
        StateName = "InGameSystemCheckEndState";
    }

    public override void StateBegin()
    {
        //m_InGameSystem.TestInGameStateText.text = "CheckEnd";
        Debug.Log("check end");
    }

    public override void StateUpdate()
    {
        //game basic update
        m_InGameSystem.GameRun();
        //time update
        m_InGameSystem.TimeControl();
        //event update
        m_InGameSystem.EventControl();

        if (m_InGameSystem.IsFullBlocks())
        {
            //go game over state
            m_Controller.SetState(new InGameSystemGameOverState(m_InGameSystem, m_Controller));
            return;
        }
        if (m_InGameSystem.GetGameTime() == 0)
        {
            //go time up state
            m_Controller.SetState(new InGameSystemTimeUpState(m_InGameSystem, m_Controller));
            return;
        }

        //go back play state
        m_Controller.SetState(new InGameSystemPlayState(m_InGameSystem, m_Controller));
        return;
    }
}
