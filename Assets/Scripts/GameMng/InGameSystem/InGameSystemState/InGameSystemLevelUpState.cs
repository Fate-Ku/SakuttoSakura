//
// InGameSystemLevelUpState.cs
// 
// 2026/06/29 Created By Man-Yi, Yeh
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
        
        m_InGameSystem.TestInGameStateText.text = "Level Up";
    }

    public override void StateUpdate()
    {
        //game basic update
        m_InGameSystem.GameRun();

        if (!startLevelUp)
        {
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

                if (m_InGameSystem.IsLevelUpEnd())
                {
                    startWait = true;
                }
            }
            else
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
        
    }
}
