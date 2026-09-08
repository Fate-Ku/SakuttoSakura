//
// TutorialInfo.cs
// 
// 2026/08/01 Created By Fate Ku
// 2026/09/09 Updated By Fate Ku
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
