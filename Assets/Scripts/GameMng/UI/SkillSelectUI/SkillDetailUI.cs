//
// SkillDetailUI.cs
// 
// 2026/06/07 Created By Fate Ku
// 2026/06/08 Updateed By Fate Ku
//

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillDetailUI : SkillDetailFrameUI
{
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private Image skillIcon;
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;


    public override void showSkillDetail(bool show, RectTransform targetRect)
    {
        base.showSkillDetail(show, targetRect);
    }

    public void showSkillDetail(bool show, RectTransform targetRect,SkillDataSO skillData)
    {
        base.showSkillDetail(show, targetRect);

        if(show == false)
        {
            return;
        }

        cost.text = "Cost " + skillData.cost.ToString();
        skillIcon.sprite = skillData.icon;
        skillName.text = skillData.skillName;
        skillDescription.text = skillData.description;


    }


}
