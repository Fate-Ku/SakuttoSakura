//
// InGameSystemLevelUpState.cs
// 
// 2026/06/29 Created By Man-Yi, Yeh
// 2026/07/02 Updated By Fate Ku
// 2026/07/03 Updated By Man-Yi, Yeh
// 2026/07/05 Updated By Man-Yi, Yeh
// 


using UnityEngine;

public class InGameSystemLevelUpState : IInGameSystemState
{
    private float timer;
    private bool startLevelUp;
    private bool startWait;

    public InGameSystemLevelUpState(InGameSystem inGameSystem, InGameStateController controller) 
        : base(inGameSystem, controller)
    {
        StateType = InGameSystemStateType.LevelUp;
        StateName = "InGameSystemLevelUpState";
    }

    public override void StateBegin()
    {
        Debug.Log("level up trigger in begin: " + m_Trigger.ToString());

        timer = m_InGameSystem.GameInfo.GetLevelUpTime();
        startLevelUp = false;
        startWait = false;

        //start State UI
        GameMng.Instance.ShowStateUI(m_StateType);

    }

    public override void StateUpdate()
    {
        Debug.Log("level up trigger in update: " + m_Trigger.ToString());

        //game basic update
        m_InGameSystem.GameRun();

        if (!startLevelUp)
        {
            Debug.Log("level: wait idle");
            if (m_InGameSystem.IsAllBlocksIdle())
            {
                m_InGameSystem.LevelUpStart();
                startLevelUp = true;
            }
        }
        else
        {
            if (!startWait)
            {
                //event update
                m_InGameSystem.EventControl();
                Debug.Log("level: wait event end");
                if (m_InGameSystem.IsLevelUpEnd())
                {
                    startWait = true;
                }
            }
            else
            {
                Debug.Log("level: wait end");
                if (m_Trigger)
                {
                    //end State UI
                    GameMng.Instance.EndStateUI(m_StateType);
                    //go play state
                    m_Controller.SetState(new InGameSystemPlayState(m_InGameSystem, m_Controller));
                    return;

                }

                /*
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    //end State UI
                    GameMng.Instance.EndStateUI(m_StateType);
                    //go play state
                    m_Controller.SetState(new InGameSystemPlayState(m_InGameSystem, m_Controller));
                    return;
                }
                */
            }
        }
        /*
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Debug.Log("level up trigger in update go true test");
            m_Trigger = true;
        }
        */
    }
}
