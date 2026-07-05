//
// InGameSystemTimeUpState.cs
// 
// 2026/06/10 Created By Man-Yi, Yeh
// 2026/07/02 Updated By Fate Ku
// 2026/07/03 Updated By Man-Yi, Yeh
// 2026/07/05 Updated By Man-Yi, Yeh
// 

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
        Debug.Log("time up trigger in begin: " + m_Trigger.ToString());

        timer = m_InGameSystem.GameInfo.GetTimeUpTime();
        
        //start State UI
        GameMng.Instance.ShowStateUI(m_StateType);

    }

    public override void StateUpdate()
    {
        Debug.Log("time up trigger in update: " + m_Trigger.ToString());

        if (!m_InGameSystem.IsAllBlocksIdle())
        {
            //game basic update
            m_InGameSystem.GameRun();
        }
        else
        {
            if (m_Trigger)
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

                //end State UI
                GameMng.Instance.EndStateUI(m_StateType);
                //go game over state
                m_Controller.SetState(new InGameSystemGameOverState(m_InGameSystem, m_Controller));
                return;
            }
            
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Debug.Log("time up trigger in update go true test");
                m_Trigger = true;
            }
            
        }
    }
}
