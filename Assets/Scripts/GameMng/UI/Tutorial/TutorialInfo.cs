//
// TutorialInfo.cs
// 
// 2026/08/01 Created By Fate Ku
// 2026/09/09 Updated By Fate Ku
// 2026/09/12 Updated By Fate Ku
//

using TMPro;
using UnityEngine;

public class TutorialInfo : MonoBehaviour
{
    [Header("Instructions")]
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI InstructionsText;

    [Header("Click")]
    [SerializeField] public GameObject ClickMark;
    [SerializeField] public GameObject TapFrame;
    [SerializeField] public GameObject InfoFrame;
    [SerializeField] public GameObject TapWordAnim;

    [Header("Pause UI")]
    public GameObject popupPanel;

    public GameObject GetInfoFrame()
    {  return InfoFrame; }

    public GameObject GetTapWordAnim()
    { return TapWordAnim; }

    public GameObject GetPopupPanel()
    { return popupPanel; }

    public TextMeshProUGUI GetInstructionsText()
    {
        return InstructionsText;
    }

    public GameObject GetClickMark()
    {
        return ClickMark;
    }

    public TextMeshProUGUI GetInfoText() { return infoText; }

    public GameObject GetTapFrame() { return TapFrame; }

}
