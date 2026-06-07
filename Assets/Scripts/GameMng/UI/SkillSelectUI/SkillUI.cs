//
// SkillUI.cs
// 
// 2026/06/07 Created By Fate Ku
//
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    public SkillDetailUI skillDetail;

    private void Awake()
    {
        skillDetail = GetComponentInChildren<SkillDetailUI>();
    }

}
