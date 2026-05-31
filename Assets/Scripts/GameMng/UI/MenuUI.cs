//
// MenuUI.cs
// 
// 2026/05/31 Created By Fate Ku
//
//
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MenuUI : MonoBehaviour
{

    public void GoToSkillSelectScene()
    {
        GameMng.Instance.SetNextScene("SkillSelectScene");
    }

    public void GoToInGameScene()
    {
        GameMng.Instance.SetNextScene("InGameScene");
    }

}
