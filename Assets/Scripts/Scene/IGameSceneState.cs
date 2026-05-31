//
// IGameSceneState.cs
// 
// 2026/05/31 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class IGameSceneState : ISceneState
{
    public IGameSceneState(SceneStateController controller) 
        : base(controller)
    {
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
                    state = new MenuState(m_Controller);
                    break;

                case "SkillSelectScene":
                    state = new SkillSelectState(m_Controller);
                    break;

                case "InGameScene":
                    state = new InGameState(m_Controller);
                    break;

                case "ScoreScene":
                    state = new ScoreState(m_Controller);
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
