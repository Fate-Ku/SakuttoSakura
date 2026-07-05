//
// InGameSystemStartState.cs
// 
// 2026/06/10 Created By Man-Yi, Yeh
// 2026/07/02 Updated By Fate Ku
// 2026/07/03 Updated By Man-Yi, Yeh
// 2026/07/05 Updated By Man-Yi, Yeh
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
        Debug.Log("start trigger in begin: " + m_Trigger.ToString());

        timer = m_InGameSystem.GameInfo.GetStartTime();

        //start State UI
        GameMng.Instance.ShowStateUI(m_StateType); //start
    }

    public override void StateUpdate()
    {
        Debug.Log("start trigger in update: " + m_Trigger.ToString());
        if (m_Trigger)
        {
            //go play state
            m_Controller.SetState(new InGameSystemPlayState(m_InGameSystem, m_Controller));
            return;
        }

        /*
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            //go play state
            m_Controller.SetState(new InGameSystemPlayState(m_InGameSystem, m_Controller));
            return;
        }
        */
    }
}
