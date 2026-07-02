//
// InGameUIState.cs
// 
// 2026/06/24 Created By Fate Ku
// 2026/06/30 Updated By Fate Ku
//

using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class InGameUIState
{
    private TextMeshPro m_InGameStateText;

    public InGameSystemStateType m_StageType;
    public int m_GameLevel;

    // animation
    private bool m_IsAnimating = false;
    private float m_AnimTime = 0f;
    private float m_AnimDuration = 3f;

    private Vector3 m_StartPos;
    private Vector3 m_TargetPos;

    public InGameUIState(TextMeshPro inGameStateText)
    {
        m_InGameStateText = inGameStateText;
    }


    public void Init()
    {

    }

    public void Update()
    {
        UpdateState();
        UpdateAnimation();
    }

    public void Term()
    {
        m_InGameStateText = null;
    }

    public void UpdateState()
    {
        m_StageType = GameMng.Instance.GetInGameSystemStateType();
        m_GameLevel = GameMng.Instance.GetGameLevel();

    }

    public void ShowState()
    {
        UpdateState();
        UpdateText();
        StartAnimation();

    }

    private void UpdateText()
    {
        m_InGameStateText.text = m_StageType.ToString();

        if (m_StageType == InGameSystemStateType.LevelUp)
        {
            m_InGameStateText.text += " " + m_GameLevel;
        }
    }

    // ---------------------------------------------------------
    // animation setting
    // ---------------------------------------------------------
    private void StartAnimation()
    {
        m_IsAnimating = true;
        m_AnimTime = 0f;

        //setting
        float scale = GameMng.Instance.GetSize();     // scaleX, scaleY
        Vector2 referPos = GameMng.Instance.GetGameReferPos();  // refer pos
        Vector2Int xy = GameMng.Instance.GetGameScale(); //column & row

        float col = xy.y; // 8
        float offsetY = scale * 0.5f;

        float startPosX = m_InGameStateText.transform.position.x;
        float startPosY = referPos.y + scale * col / 2 - offsetY; //middle

        // init position
        Vector3 pos = new Vector3(startPosX, startPosY, -1);

        // left→right
        if (m_StageType == InGameSystemStateType.None || //start
            m_StageType == InGameSystemStateType.TimeUp ||
            m_StageType == InGameSystemStateType.GameOver)
        {
            m_StartPos = new Vector3(startPosX, startPosY, -1);
            m_TargetPos = new Vector3(startPosX + 20f, startPosY, -1);
        }

        // down→up
        else if (m_StageType == InGameSystemStateType.LevelUp)
        {
            m_StartPos = new Vector3(startPosY, startPosY - 3f, -1);
            m_TargetPos = new Vector3(startPosY, startPosY + 3f, -1);
        }
        else
        {
            m_IsAnimating = false;
        }

        // init position
        if (m_IsAnimating)
        {
            m_InGameStateText.transform.position = m_StartPos;
            Debug.Log($"Start Animation");
            Debug.Log($"StartPos = {m_StartPos}");
            Debug.Log($"TargetPos = {m_TargetPos}");
        }
    }

    // ---------------------------------------------------------
    // update animation
    // ---------------------------------------------------------
    private void UpdateAnimation()
    {
        if (!m_IsAnimating)
            return;

        m_AnimTime += Time.deltaTime;
        float t = Mathf.Clamp01(m_AnimTime / m_AnimDuration);

        // duration
        m_InGameStateText.transform.position = Vector3.Lerp(m_StartPos, m_TargetPos, t);

        if (t >= 1f)
        {
            m_IsAnimating = false;
        }
    }

}
