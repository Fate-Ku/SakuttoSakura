//
// SkillDataSO.cs
// 
// 2026/06/07 Created By Fate Ku
// 2026/06/07 Updated By Fate Ku
//
using UnityEngine;

[CreateAssetMenu(menuName = "Data Setup/Skill Data", fileName = "Skill data - ")]
public class SkillDataSO : ScriptableObject
{
    public int cost;
    public int id;
    public bool isSelected;

    [Header("skill description")]
    public string skillName;
    public string skillType;
    [TextArea]
    public string description;
    public Sprite icon;


}
