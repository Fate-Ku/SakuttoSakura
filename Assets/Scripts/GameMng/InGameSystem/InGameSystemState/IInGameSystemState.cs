//
// IInGameSystemState.cs
// 
// 2026/06/10 Created By Man-Yi, Yeh
// 

using UnityEngine;

public enum InGameSystemStateType
{
    None = -1,

    Start,
    Play,
    TimeUp,
    GameOver,
    LevelUp
}

public class IInGameSystemState
{
    //StateType
    private InGameSystemStateType m_StateType = InGameSystemStateType.None;
    public InGameSystemStateType StateType
    {
        get { return m_StateType; }
        set { m_StateType = value; }
    }

    //StateName
    private string m_StateName = "IInGameState";
    public string StateName
    {
        set { m_StateName = value; }
    }

    //block
    protected InGameSystem m_InGameSystem;

    //Controller
    protected InGameStateController m_Controller = null;

    public IInGameSystemState(InGameSystem inGameSystem, InGameStateController controller)
    {
        m_InGameSystem = inGameSystem;
        m_Controller = controller;
    }

    //begin
    public virtual void StateBegin() { }

    //end
    public virtual void StateEnd() { }

    //update
    public virtual void StateUpdate() { }

    public override string ToString()
    {
        return string.Format(
            "I_InGameSystemState: StateName={0}",
            m_StateName);
    }
}
