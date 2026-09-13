//
// SceneChangeUI.cs
// 
// 2026/06/02 Created By Fate Ku
// 2026/07/14 Updated By Fate Ku
// 2026/09/13 Updated By Fate Ku
//
using UnityEngine;

public class SceneChangeUI : MonoBehaviour
{
    [Header("Popup UI")]
    public GameObject popupPanel;

    public void ShowVolumeOptions()
    {
        popupPanel.SetActive(true);
    }

    public void HideVolumeOptions()
    {
        popupPanel.SetActive(false);
    }

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
