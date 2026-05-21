//
// SceneStateController.cs
// 
// 2026/05/19 Created By Man-Yi, Yeh
// 2026/05/21 Update By Man-Yi, Yeh 
// 

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateController
{
    private ISceneState m_State;
    private AsyncOperation asyncOp;
    private bool m_bRunBegin = false;

    public SceneStateController() { }

    //set state
    public void SetState(ISceneState state, string loadSceneName)
    {
        Debug.Log("SetState:" + state.ToString());
        m_bRunBegin = false;

        //load scene
        if (loadSceneName != "")
        {
            asyncOp = SceneManager.LoadSceneAsync(loadSceneName);
        }

        //end previous state
        if (m_State != null)
        {
            m_State.StateEnd();
        }

        //setting
        m_State = state;

    }

    //state update
    public void StateUpdate()
    {
        if (m_State == null) 
        {
            return;
        }

        //is loading
        if (asyncOp != null)
        {
            if (!asyncOp.isDone)
            {
                return;
            }
        }

        //start new state
        if (!m_bRunBegin)
        {
            m_State.StateBegin();
            m_bRunBegin= true;
        }

        //state update
        m_State.StateUpdate();
    }

    //load scene
    private void LoadScene(string loadSceneName)
    {
        if (loadSceneName == null || loadSceneName.Length == 0)
        {
            return;
        }
        SceneManager.LoadScene(loadSceneName);
    }

}
