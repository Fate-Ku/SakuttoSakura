//
// PauseUI.cs
// 
// 2026/06/14 Created By Fate Ku 
// 2026/08/03 Created By Fate Ku 
//

using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [Header("Popup UI")]
    public GameObject popupPanel;

    [Header("button frame")]
    public GameObject buttonPanel;


    public void ShowPauseOptions()
    {
        popupPanel.SetActive(true);
    }

    public void HidePauseOptions()
    {
        popupPanel.SetActive(false);
    }


    // 2026/08/03 Created By Fate Ku 
    public void ShowBtnFrame()
    {
        buttonPanel.SetActive(true);
    }

    public void HideBtnFrame()
    {
        buttonPanel.SetActive(false);
    }
    // 2026/08/03 Created By Fate Ku 


    public void Pause()
    {
        GameMng.Instance.InGameReversePause();
    }

}
