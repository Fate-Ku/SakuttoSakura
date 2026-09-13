//
// MenuState.cs
// 
// 2026/05/19 Created By Man-Yi, Yeh
// 2026/05/26 Updated By Man-Yi, Yeh 
// 2026/05/30 Updated By Man-Yi, Yeh
// 2026/05/31 Updated By Man-Yi, Yeh
// 2026/09/14 Updated By Fate Ku
//

using UnityEngine;

public class MenuState : IGameSceneState
{
    private float m_Timer;

    public MenuState(SceneStateController controller, bool isTGS)
        : base(controller, isTGS)
    {
        StateName = "MenuState";
        m_Timer = 0f;
    }

    public override void StateBegin()
    {
        GameMng.Instance.SetPhase(GameMng.PhaseType.Menu, m_IsTGS);

        BGMMng.Instance.SetBGM(BGMType.Intro);
        BGMMng.Instance.SetNextBGM(BGMType.A1Loop, true);

    }

    public override void StateEnd()
    {
        GameMng.Instance.EndPhase();

        BGMMng.Instance.PauseBGM();
    }

    public override void StateUpdate()
    {
        GameMng.Instance.Update();
        ControllSceneByGameMng();

        if (m_IsTGS)
        {
            m_Timer += Time.deltaTime;

            // 2026/09/13 Updated By Fate Ku
            GameObject volumeMenuObj = GameObject.Find("VolumeMenu");
            if (volumeMenuObj != null && volumeMenuObj.activeSelf)
            {
                m_Timer = 0;
            }
            // 2026/09/13 Updated By Fate Ku

            if (m_Timer >= 20f)
            {
                //change to MenuState
                m_Controller.SetState(new IdleScreenState(m_Controller, m_IsTGS), "IdleScreenScene");
            }
        }
    }
}
