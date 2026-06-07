//
// SkillDetailUI.cs
// 
// 2026/06/07 Created By Fate Ku
//
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillDetailUI : MonoBehaviour
{
    private RectTransform rect; // skill frame
    private RectTransform canvasRect; // canvas frame

    public void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    public void showSkillDetail(bool show, RectTransform targetRect)
    {
        if (show ==false)
        {
            hideSkillDetail();
            return;
        }

        UpdatePosition(targetRect);
    }

    private void UpdatePosition(RectTransform targetRect)
    {

        rect.position = new Vector2(canvasRect.position.x, canvasRect.position.y);

    }

    public void hideSkillDetail()
    {
        rect.position = new Vector2(canvasRect.position.x + canvasRect.rect.width * 5f, canvasRect.position.y);
    }


}
