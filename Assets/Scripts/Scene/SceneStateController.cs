//
// SceneStateController.cs
// 
// 2026/05/19 Created By Man-Yi, Yeh
// 2026/05/21 Updated By Man-Yi, Yeh 
// 2026/05/26 Updated By Man-Yi, Yeh 
// 2026/06/03 Updated By Man-Yi, Yeh 
// 2026/08/03 Updated By Man-Yi, Yeh 
// 

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateController
{
    private ISceneState m_State;
    private AsyncOperation m_AsyncOp;
    private bool m_bRunBegin = false;

    private LoadingBG m_LoadingBG;

    public SceneStateController(LoadingBG loadingBG)
    {
        m_LoadingBG = loadingBG;
    }

    //set state
    public void SetState(ISceneState state, string loadSceneName)
    {
        Debug.Log("Set Scene State:" + state.ToString());
        m_bRunBegin = false;

        //load scene
        LoadScene(loadSceneName);

        //end previous state
        m_State?.StateEnd();

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
        if (m_AsyncOp != null)
        {
            if (!m_AsyncOp.isDone)
            {
                return;
            }
        }

        //start new state
        if (!m_bRunBegin)
        {
            m_LoadingBG.SetActive(false);
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

        if (loadSceneName == "InGameScene")
        {
            GameObject gameTestOb = GameObject.Find("GameTest");
            if (gameTestOb != null) 
            {
                GameTest gameTest = gameTestOb.GetComponent<GameTest>();
                if (gameTest != null) 
                {
                    int id = gameTest.inGamePatternID;
                    if (id != 0)
                    {
                        loadSceneName += (" " + id.ToString());
                    }
                }
            }
        }

        m_AsyncOp = SceneManager.LoadSceneAsync(loadSceneName);
        m_LoadingBG.SetActive(true);
    }

}
