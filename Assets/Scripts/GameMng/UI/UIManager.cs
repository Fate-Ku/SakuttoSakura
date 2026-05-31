//
// UIManager.cs
// 
// 2026/05/31 Created By Fate Ku
//
//
using UnityEngine;

public class UIManager : GameInfo
{
    //----------------In Game Button---------------------//
    public static UIManager Instance { get; private set; }

    // button width/height setting
    protected const float ButtonWidth = 22f;
    protected const float ButtonHeight = 176f;

    // button space（Width +12）
    protected const float ButtonSpacing = 12f;

    private int selectedIndex = -1; // default:-1 (no choose)

    protected virtual void Awake()
    {
        Instance = this;
    }

    public void SetSelectedIndex(int index)
    {
        selectedIndex = index;
        Debug.Log("UI MGR Set Selected button Index = " + index);
    }

    public int GetSelectedIndex()
    {
        return selectedIndex;
    }

    // button size
    public Vector2 GetButtonSize()
    {
        return new Vector2(ButtonWidth, ButtonHeight);
    }

    // button positions（index = 0~6）
    public Vector2 GetButtonPos(int index)
    {
        //Vector2 basePos = GetReferPos();

        //test
        Vector2 basePos = new Vector2(-36, -18);

        float x = basePos.x + index * ButtonSpacing;
        float y = basePos.y;

        return new Vector2(x, y);
    }


}
