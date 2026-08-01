//
// TutorialInfo.cs
// 
// 2026/08/01 Created By Fate Ku
//

using TMPro;
using UnityEngine;

public class TutorialInfo : MonoBehaviour
{
    [Header("Instructions")]
    [SerializeField] private TextMeshProUGUI InstructionsText;

    [Header("Click")]
    [SerializeField] public GameObject ClickMark;

    public TextMeshProUGUI GetInstructionsText()
    {
        return InstructionsText;
    }

    public GameObject GetClickMark()
    {
        return ClickMark;
    }

}
