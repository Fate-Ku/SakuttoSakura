//
// IGameSceneState.cs
// 
// 2026/05/31 Created By Man-Yi, Yeh
// 2026/06/03 Updated By Man-Yi, Yeh
// 2026/07/13 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class IGameSceneState : ISceneState
{
    public IGameSceneState(SceneStateController controller, bool isTGS) 
        : base(controller, isTGS)
    {
        StateName = "IGameSceneState";
    }

    protected void ControllSceneByGameMng()
    {
        if (IsSceneEndByGameMng())
        {
            string sceneName = GetNextSceneNameByGameMng();
            ISceneState state = null;

            switch (sceneName) 
            {
                case "MenuScene":
                    state = new MenuState(m_Controller, m_IsTGS);
                    break;

                case "SkillSelectScene":
                    state = new SkillSelectState(m_Controller, m_IsTGS);
                    break;

                case "TutorialScene":
                    state = new TutorialState(m_Controller, m_IsTGS);
                    break;

                case "InGameScene":
                    state = new InGameState(m_Controller, m_IsTGS);
                    break;

                case "ScoreScene":
                    state = new ScoreState(m_Controller, m_IsTGS);
                    break;

                case "IdleScreenScene":
                    state = new IdleScreenState(m_Controller, m_IsTGS);
                    break;

                default:
                    Debug.Log("Don't have scene with this name");
                    break;
            }

            if (state != null)
            {
                m_Controller.SetState(state, sceneName);
            }

            GameMng.Instance.IsSceneEnd = false;
        }
    }

    private bool IsSceneEndByGameMng()
    {
        return GameMng.Instance.IsSceneEnd;
    }

    private string GetNextSceneNameByGameMng()
    {
        return GameMng.Instance.NextSceneName;
    }
}
