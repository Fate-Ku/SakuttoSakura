//
// SceneChangeUI.cs
// 
// 2026/06/02 Created By Fate Ku
// 2026/07/14 Created By Fate Ku
//
//
using UnityEngine;

public class SceneChangeUI : MonoBehaviour
{
    public void GoToSkillSelectScene()
    {
        GameMng.Instance.SetNextScene("SkillSelectScene");
    }

    public void GoToInGameScene()
    {
        GameMng.Instance.SetNextScene("InGameScene");
    }

    public void GoToMenuScene()
    {
        GameMng.Instance.SetNextScene("MenuScene");
    }
    public void GoToTutorialScene()
    {
        GameMng.Instance.SetNextScene("TutorialScene");
    }
}
