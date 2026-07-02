//
// InGameSystemPlayState.cs
// 
// 2026/06/10 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class InGameSystemPlayState : IInGameSystemState
{
    public InGameSystemPlayState(InGameSystem inGameSystem, InGameStateController controller) 
        : base(inGameSystem, controller)
    {
        StateType = InGameSystemStateType.Play;
        StateName = "InGameSystemPlayState";
    }

    public override void StateBegin()
    {
        m_InGameSystem.StartPlay();
        //m_InGameSystem.TestInGameStateText.text = "Play";
    }

    public override void StateEnd()
    {
        m_InGameSystem.IsPlaying = false;
    }

    public override void StateUpdate()
    {
        //game basic update
        m_InGameSystem.GameRun();
        //operate update
        m_InGameSystem.OperateControl();
        //time update
        m_InGameSystem.TimeControl();
        //event update
        m_InGameSystem.EventControl();


        if (m_InGameSystem.IsFullBlocks())
        {
            //go check end state
            m_Controller.SetState(new InGameSystemCheckEndState(m_InGameSystem, m_Controller));
            return;
        }
        if (m_InGameSystem.CheckLevelUp())
        {
            //go level up state
            m_Controller.SetState(new InGameSystemLevelUpState(m_InGameSystem, m_Controller));
            return;
        }
        if (m_InGameSystem.GetGameTime() == 0)
        {
            //go time up state
            m_Controller.SetState(new InGameSystemTimeUpState(m_InGameSystem, m_Controller));
            return;
        }

    }

}
