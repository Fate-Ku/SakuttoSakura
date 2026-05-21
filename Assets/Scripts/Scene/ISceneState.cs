//
// ISceneState.cs
// 
// 2026/05/19 Created By Man-Yi, Yeh
// 


using UnityEngine;

public class ISceneState
{
    //StateName
    private string m_StateName = "ISceneState";
    public string StateName
    {
        get { return m_StateName; }
        set { m_StateName = value; }
    }

    //Controller
    protected SceneStateController m_Controller = null;

    public ISceneState(SceneStateController controller)
    {
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
            "I_SceneState: StateName={0}", 
            StateName);
    }

}
