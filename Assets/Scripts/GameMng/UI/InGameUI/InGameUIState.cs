//
// InGameUIState.cs
// 
// 2026/06/24 Created By Fate Ku
// 2026/06/30 Updated By Fate Ku
// 2026/07/02 Updated By Fate Ku
// 2026/07/06 Updated By Fate Ku
// 2026/07/09 Updated By Fate Ku
// 2026/07/17 Updated By Fate Ku
// 2026/08/04 Updated By Fate Ku
//

using System.Collections.Generic;
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

    private Dictionary<InGameSystemStateType, GameObject> m_DStageType;

    private GameObject m_CurrentStageObj;

    // level check
    public int m_GameLevel;
    public int MaxLevel;

    // animation
    private bool m_IsAnimating = false;
    private float m_AnimTime = 0f;
    private float m_AnimDuration;

    private Vector3 m_StartPos;
    private Vector3 m_TargetPos;

    private float m_BasePosX;

    // 2026/08/04 Updated By Fate Ku
    //-------------------
    //Tutorial use
    //-------------------
    private InGameType m_InGameType;
    // 2026/08/04 Updated By Fate Ku


    public InGameUIState(TextMeshPro inGameStateText, Dictionary<InGameSystemStateType, GameObject> DStageType,InGameType inGameType)
    {
        m_InGameStateText = inGameStateText;
        m_DStageType = DStageType;
        m_InGameType = inGameType;
    }


    public void Init()
    {
        MaxLevel = 0;

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
        CheckMaxLevel();
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
        foreach (var stage in m_DStageType.Values)
        {
            if (stage != null)
                stage.SetActive(false);
        }

        m_CurrentStageObj = null;

        if (m_DStageType.TryGetValue(m_StageType, out GameObject stageObj))
        {
            Debug.Log($"Show Stage : {stageObj.name}");
            stageObj.SetActive(true);
            m_CurrentStageObj = stageObj;
        }
        else
        {
            Debug.LogWarning($"Can't find Stage : {m_StageType}");
        }

        if (m_StageType == InGameSystemStateType.LevelUp)
        {
            m_InGameStateText.gameObject.SetActive(true);
            m_InGameStateText.text = " " + m_GameLevel;
        }
        else
        {
            m_InGameStateText.gameObject.SetActive(false);
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
            m_TargetPos = new Vector3(startPosX + 13f, startPosY, -1);
            //callTrigger = true;
        }
        // 2026/08/04 Updated By Fate Ku
        // left→middle
        else if (m_StageType == InGameSystemStateType.TimeUp)
        {
            if (m_InGameType == InGameType.Tutorial)
            {
                m_CurrentStageObj.SetActive(false);
            }
            else
            {
                m_CurrentStageObj.SetActive(true);
                m_StartPos = new Vector3(startPosX, startPosY, -1);
                m_TargetPos = new Vector3(endPosX, startPosY, -1);
                //callTrigger = false;

            }
        }
        // 2026/08/04 Updated By Fate Ku
        else if (m_StageType == InGameSystemStateType.LevelUp)
        {
            m_StartPos = new Vector3(startPosX, startPosY, -1);
            m_TargetPos = new Vector3(endPosX - 1.5f, startPosY, -1);
        }
        else
        {
            m_IsAnimating = false;
        }

        // init position
        if (m_IsAnimating)
        {
            if (m_CurrentStageObj != null)
            {
                m_CurrentStageObj.transform.position = m_StartPos;

                // Level font follow with picture
                if (m_StageType == InGameSystemStateType.LevelUp)
                {
                    Vector3 pos = m_StartPos;
                    pos.x += 1.5f;    // follow with UI
                    pos.y -= 0.1f;
                    m_InGameStateText.transform.position = pos;
                }
            }
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

        // 2026/08/04 Updated By Fate Ku
        // middle→right
        if (m_StageType == InGameSystemStateType.TimeUp)
        {
            if (m_InGameType == InGameType.Tutorial)
            {
                m_CurrentStageObj.SetActive(false);
            }
            else
            {
                m_CurrentStageObj.SetActive(true);

                m_StartPos = new Vector3(startPosX, startPosY, -1);
                m_TargetPos = new Vector3(startPosX + 7f, startPosY, -1);
                //callTrigger = true;
            }
        }
        // 2026/08/04 Updated By Fate Ku
        else if (m_StageType == InGameSystemStateType.LevelUp)
        {
            m_StartPos = new Vector3(startPosX - 1.5f, startPosY, -1);
            m_TargetPos = new Vector3(startPosX + 7f, startPosY, -1);
        }
        else
        {
            m_IsAnimating = false;
        }

        // init position
        if (m_IsAnimating)
        {
            if (m_CurrentStageObj != null)
            {
                m_CurrentStageObj.transform.position = m_StartPos;

                if (m_StageType == InGameSystemStateType.LevelUp)
                {
                    Vector3 pos = m_StartPos;
                    pos.x += 1.5f;
                    pos.y -= 0.1f;
                    m_InGameStateText.transform.position = pos;
                }
            }
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
            m_AnimDuration = 2.2f;
        }

        m_AnimTime += Time.deltaTime;
        float t = Mathf.Clamp01(m_AnimTime / m_AnimDuration);

        // duration
        //m_InGameStateText.transform.position = Vector3.Lerp(m_StartPos, m_TargetPos, t);
        Vector3 pos = Vector3.Lerp(m_StartPos, m_TargetPos, t);

        if (m_CurrentStageObj != null)
        {
            m_CurrentStageObj.transform.position = pos;
        }

        if (m_StageType == InGameSystemStateType.LevelUp)
        {
            Vector3 levelPos = pos;
            levelPos.x += 1.5f;      // Maintain a fixed distance from the picture
            levelPos.y -= 0.1f;
            m_InGameStateText.transform.position = levelPos;
        }

        if (t >= 1f)
        {
            m_IsAnimating = false;

            //if (callTrigger)
            //{
            GameMng.Instance.CallInGameSystemStateTrigger();
            //}
        }


    }

    private void CheckMaxLevel()
    {
        if (m_GameLevel > MaxLevel)
        {
            MaxLevel = m_GameLevel;
        }

    }

    public int GetMaxLevel()
    {
        return MaxLevel;
    }


}
