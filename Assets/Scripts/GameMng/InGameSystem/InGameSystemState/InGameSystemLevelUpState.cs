//
// InGameSystemLevelUpState.cs
// 
// 2026/06/29 Created By Man-Yi, Yeh
// 2026/07/02 Updated By Fate Ku
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
        timer = m_InGameSystem.GameInfo.GetLevelUpTime();
        startLevelUp = false;
        startWait = false;

        GameMng.Instance.ShowState();
        //m_InGameSystem.TestInGameStateText.text = "Level Up";
    }

    public override void StateUpdate()
    {
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
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    //go play state
                    m_Controller.SetState(new InGameSystemPlayState(m_InGameSystem, m_Controller));
                    return;
                }
            }
        } 
        
    }
}
