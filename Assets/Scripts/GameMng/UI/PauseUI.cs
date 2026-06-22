//
// PauseUI.cs
// 
// 2026/06/14 Created By Fate Ku 

using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [Header("Popup UI")]
    public GameObject popupPanel;

    public void ShowPauseOptions()
    {
        popupPanel.SetActive(true);
    }

    public void HidePauseOptions()
    {
        popupPanel.SetActive(false);
    }

    public void Pause()
    {
        GameMng.Instance.InGameReversePause();
    }

}
