//
// SkilliconUI.cs
// 
// 2026/06/07 Created By Fate Ku
//

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkilliconUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private SkillUI skillUI;
    private RectTransform rect;

    [SerializeField] private SkillDataSO skillData;
    [SerializeField] private string skillName;

    [SerializeField] private Image skillIcon;
    [SerializeField] private string lockedColorHex = "#9F9797";

    public bool isUnlocked;
    public bool isLocked;

    public void Init()
    {
        skillUI = GetComponentInParent<SkillUI>();
        rect = GetComponent<RectTransform>();

        skillUI.skillDetail.hideSkillDetail();
        UpdateIconColor(GetColorByHex(lockedColorHex));

    }

    private void Unlock()
    {
        isUnlocked = true;
        UpdateIconColor(Color.white);
    }

    private bool CanBeUnlocked()
    {
        if (isUnlocked || isLocked)
        {
            return false;
        }
        return true;
    }

    private void UpdateIconColor(Color color)
    {
        if (skillIcon == null)
        {
            return;
        }
        skillIcon.color = color;

    }

    // -------------------------
    // Click Skill to show detail
    // -------------------------
    public void OnPointerDown(PointerEventData eventData)
    {
        skillUI.skillDetail.showSkillDetail(true, rect, skillData);

        if (CanBeUnlocked())
        {
            Unlock();
        }
        else
        {
            Debug.Log("Cannot be unlock");
        }

        Debug.Log("Click Skill to show detail");

    }

    private Color GetColorByHex(string hexNumber)
    {
        ColorUtility.TryParseHtmlString(hexNumber, out Color color);

        return color;
    }

    private void OnValidate()
    {
        if (skillData == null)
        {
            return;
        }
        skillName = skillData.skillName;
        skillIcon.sprite = skillData.icon;
        gameObject.name = "UI_Skillicon - " + skillData.skillName;


    }

}
