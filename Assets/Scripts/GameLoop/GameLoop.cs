//
// GameLoop.cs
// 
// 2026/05/19 Created By Man-Yi, Yeh
// 2026/05/21 Update By Man-Yi, Yeh 
//

using UnityEngine;

public class GameLoop : MonoBehaviour
{
    //scene state controller
    SceneStateController m_SceneStateController = new SceneStateController();

    private void Awake()
    {
        //don't destroy
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        //set start scene
        m_SceneStateController.SetState(new TitleState(m_SceneStateController), "");
    }

    void Update()
    {
        m_SceneStateController.StateUpdate();
    }
}
