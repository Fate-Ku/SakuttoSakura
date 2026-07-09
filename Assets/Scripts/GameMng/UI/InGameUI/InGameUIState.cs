//
// InGameUIState.cs
// 
// 2026/06/24 Created By Fate Ku
// 2026/06/30 Updated By Fate Ku
// 2026/07/02 Updated By Fate Ku
// 2026/07/06 Updated By Fate Ku
// 2026/07/09 Updated By Fate Ku
//

using TMPro;
using UnityEngine;

public class InGameUIState
{
    //-------------------
    //Info
    //-------------------
    //blockPos info
    private BlockPosInfo m_BlockPosInfo;

    public BlockPosInfo BlockPosInfo
    {
        get { return m_BlockPosInfo; }
    }

    private TextMeshPro m_InGameStateText;

    public InGameSystemStateType m_StageType;
    public int m_GameLevel;

    // animation
    private bool m_IsAnimating = false;
    private float m_AnimTime = 0f;
    private float m_AnimDuration;

    private Vector3 m_StartPos;
    private Vector3 m_TargetPos;

    private float m_BasePosX;

    //private bool callTrigger = false;

    public InGameUIState(TextMeshPro inGameStateText)
    {
        m_InGameStateText = inGameStateText;
    }


    public void Init()
    {
        //-------------------
        //Info
        //-------------------
        //game info
        GameObject blockInfo = GameObject.Find("BlockPosInfo");
        if (blockInfo != null)
        {
            m_BlockPosInfo = blockInfo.GetComponent<BlockPosInfo>();
        }

        m_BasePosX = m_InGameStateText.transform.position.x;
    }

    public void Update()
    {
        UpdateAnimation();
    }

    public void Term()
    {
        m_InGameStateText = null;
    }

    public void ShowStateUI(InGameSystemStateType type)
    {
        m_StageType = type;
        m_GameLevel = GameMng.Instance.GetGameLevel();
        //UpdateState();
        UpdateText();
        StartAnimation();

    }

    public void EndStateUI(InGameSystemStateType type)
    {
        m_StageType = type;
        UpdateText();
        EndAnimation();
    }

    private void UpdateText()
    {
        if (m_StageType == InGameSystemStateType.Start ||
            m_StageType == InGameSystemStateType.TimeUp ||
            m_StageType == InGameSystemStateType.GameOver)
        {
            m_InGameStateText.text = m_StageType.ToString();
        }
        else if (m_StageType == InGameSystemStateType.LevelUp)
        {
            m_InGameStateText.text = "Level Up " + m_GameLevel;
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
        float scale = m_BlockPosInfo.GetSize();     // scaleX, scaleY
        Vector2 referPos = m_BlockPosInfo.GetReferPos();  // refer pos
        Vector2Int xy = m_BlockPosInfo.GetScale(); //column & row

        float col = xy.y; // 8
        float offsetY = scale * 0.5f;

        float startPosX = m_BasePosX;
        float startPosY = referPos.y + scale * col / 2 - offsetY; //middle

        float row = xy.x; // 7
        float offsetX = scale * 0.5f;

        float endPosX = referPos.x + scale * row / 2 - offsetX;

        // left→right
        if (m_StageType == InGameSystemStateType.Start ||
            m_StageType == InGameSystemStateType.GameOver)
        {
            m_StartPos = new Vector3(startPosX, startPosY, -1);
            m_TargetPos = new Vector3(startPosX + 10f, startPosY, -1);
            //callTrigger = true;
        }
        // left→middle
        else if (m_StageType == InGameSystemStateType.TimeUp ||
            m_StageType == InGameSystemStateType.LevelUp)
        {
            m_StartPos = new Vector3(startPosX, startPosY, -1);
            m_TargetPos = new Vector3(endPosX, startPosY, -1);
            //callTrigger = false;
        }
        else
        {
            m_IsAnimating = false;
        }

        // init position
        if (m_IsAnimating)
        {
            m_InGameStateText.transform.position = m_StartPos;
        }
    }

    private void EndAnimation()
    {
        m_IsAnimating = true;
        m_AnimTime = 0f;

        //setting
        float scale = m_BlockPosInfo.GetSize();     // scaleX, scaleY
        Vector2 referPos = m_BlockPosInfo.GetReferPos();  // refer pos
        Vector2Int xy = m_BlockPosInfo.GetScale(); //column & row

        float col = xy.y; // 8
        float row = xy.x; // 7
        float offsetX = scale * 0.5f;
        float offsetY = scale * 0.5f;

        float startPosX = referPos.x + scale * row / 2 - offsetX;
        float startPosY = referPos.y + scale * col / 2 - offsetY; //middle

        // middle→right
        if (m_StageType == InGameSystemStateType.TimeUp ||
            m_StageType == InGameSystemStateType.LevelUp)
        {
            m_StartPos = new Vector3(startPosX, startPosY, -1);
            m_TargetPos = new Vector3(startPosX + 7f, startPosY, -1);
            //callTrigger = true;
        }
        else
        {
            m_IsAnimating = false;
        }

        // init position
        if (m_IsAnimating)
        {
            m_InGameStateText.transform.position = m_StartPos;
        }
    }

    // ---------------------------------------------------------
    // update animation
    // ---------------------------------------------------------
    private void UpdateAnimation()
    {
        if (!m_IsAnimating)
            return;

        if (m_StageType == InGameSystemStateType.TimeUp ||
            m_StageType == InGameSystemStateType.LevelUp)
        {
            m_AnimDuration = 2f;
        }
        else
        {
            m_AnimDuration = 3f;
        }

        m_AnimTime += Time.deltaTime;
        float t = Mathf.Clamp01(m_AnimTime / m_AnimDuration);

        // duration
        m_InGameStateText.transform.position = Vector3.Lerp(m_StartPos, m_TargetPos, t);

        if (t >= 1f)
        {
            m_IsAnimating = false;

            //if (callTrigger)
            //{
                GameMng.Instance.CallInGameSystemStateTrigger();
            //}
        }


    }

}
