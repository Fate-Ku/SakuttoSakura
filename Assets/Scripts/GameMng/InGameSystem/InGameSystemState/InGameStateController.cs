//
// InGameStateController.cs
// 
// 2026/06/10 Created By Man-Yi, Yeh
// 


using UnityEngine;

public class InGameStateController
{
    private IInGameSystemState m_State;

    public InGameStateController() { }

    //set state
    public void SetState(IInGameSystemState state)
    {
        Debug.Log("Set InGameSystem State:" + state.ToString());

        //end previous state
        m_State?.StateEnd();

        //setting
        m_State = state;

        //begin new state
        m_State?.StateBegin();
    }

    //update
    public void StateUpdate()
    {
        //state update
        m_State?.StateUpdate();
    }
}
