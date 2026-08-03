//
// GameLoop.cs
// 
// 2026/05/19 Created By Man-Yi, Yeh
// 2026/05/21 Updated By Man-Yi, Yeh 
// 2026/06/03 Updated By Man-Yi, Yeh 
// 2026/06/24 Updated By Man-Yi, Yeh 
// 2026/08/03 Updated By Man-Yi, Yeh 
//

using UnityEngine;

public class GameLoop : MonoBehaviour
{
    //scene state controller
    SceneStateController m_SceneStateController;

    [SerializeField] private LoadingBG m_LoadingBG;

    private void Awake()
    {
        //don't destroy
        DontDestroyOnLoad(gameObject);

        //fix frame rate
        Application.targetFrameRate = 30;
    }

    void Start()
    {
        m_SceneStateController = new(m_LoadingBG);

        //set start scene
        m_SceneStateController.SetState(new TitleState(m_SceneStateController), "");
    }

    void Update()
    {
        m_SceneStateController.StateUpdate();
    }
}
