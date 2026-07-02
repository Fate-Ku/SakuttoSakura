//
// InGameSystemStartState.cs
// 
// 2026/06/10 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class InGameSystemStartState : IInGameSystemState
{
    private float timer;

    public InGameSystemStartState(InGameSystem inGameSystem, InGameStateController controller) 
        : base(inGameSystem, controller)
    {
        StateType = InGameSystemStateType.Start;
        StateName = "InGameSystemStartState";
    }

    public override void StateBegin()
    {
        timer = m_InGameSystem.GameInfo.GetStartTime();
        GameMng.Instance.ShowState(); //start
    }

    public override void StateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            //go play state
            m_Controller.SetState(new InGameSystemPlayState(m_InGameSystem, m_Controller));
            return;
        }
    }
}
