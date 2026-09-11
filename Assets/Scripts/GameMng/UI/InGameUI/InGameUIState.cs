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
// 2026/09/04 Updated By Fate Ku
// 2026/09/09 Updated By Fate Ku
// 2026/09/11 Updated By Fate Ku
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

    private float m_AnimSpeed = 5.5f;

    private Vector3 m_StartPos;
    private Vector3 m_TargetPos;

    private float m_BasePosX;

    // 2026/08/04 Updated By Fate Ku
    //-------------------
    //Tutorial use
    //-------------------
    private InGameType m_InGameType;
    // 2026/08/04 Updated By Fate Ku

    // 2026/09/09 Updated By Fate Ku
    private GameObject m_Banner;

    private bool m_IsEndAnimation = false;
    // 2026/09/09 Updated By Fate Ku

    // 2026/09/11 Updated By Fate Ku

    // Level number
    private SpriteRenderer m_TensNumber;
    private SpriteRenderer m_OnesNumber;
    private SpriteRenderer m_OnlyOneNumber;

    private Sprite[] m_NumberSprites;

    // Level number follows LevelUp banner
    private Transform m_LevelNumberRoot;

    // 2026/09/11 Updated By Fate Ku


    public InGameUIState(TextMeshPro inGameStateText, Dictionary<InGameSystemStateType, GameObject> DStageType, InGameType inGameType,
        GameObject banner, SpriteRenderer tensNumber, SpriteRenderer onesNumber, SpriteRenderer onlyOneNumber,
        Sprite[] numberSprites, Transform levelNumberRoot)
    {
        m_InGameStateText = inGameStateText;
        m_DStageType = DStageType;
        m_InGameType = inGameType;

        m_Banner = banner;

        m_TensNumber = tensNumber;
        m_OnesNumber = onesNumber;
        m_OnlyOneNumber = onlyOneNumber;
        m_NumberSprites = numberSprites;
        m_LevelNumberRoot = levelNumberRoot;
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

        HideLevelNumbers();

        HideBanner();
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
            //m_InGameStateText.text = " " + m_GameLevel;
            SetLevelNumber(m_GameLevel);
        }
        else
        {
            m_InGameStateText.gameObject.SetActive(false);
            HideLevelNumbers();
        }
    }

    // ---------------------------------------------------------
    // animation setting
    // ---------------------------------------------------------
    private void StartAnimation()
    {
        m_IsAnimating = true;
        m_AnimTime = 0f;
        m_IsEndAnimation = false;

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

        // right→left
        if (m_StageType == InGameSystemStateType.Start ||
            m_StageType == InGameSystemStateType.GameOver)
        {
            m_StartPos = new Vector3(startPosX, startPosY, -10);
            m_TargetPos = new Vector3(startPosX - 10.5f, startPosY, -10);
            //callTrigger = true;
        }
        // 2026/08/04 Updated By Fate Ku
        // right→middle
        else if (m_StageType == InGameSystemStateType.TimeUp)
        {
            if (m_InGameType == InGameType.Tutorial)
            {
                m_CurrentStageObj.SetActive(false);
            }
            else
            {
                m_CurrentStageObj.SetActive(true);
                m_StartPos = new Vector3(startPosX, startPosY, -10);
                m_TargetPos = new Vector3(endPosX, startPosY, -10);
                //callTrigger = false;

            }
        }
        // 2026/08/04 Updated By Fate Ku
        else if (m_StageType == InGameSystemStateType.LevelUp && m_InGameType == InGameType.Classic)
        {
            m_StartPos = new Vector3(startPosX, startPosY, -10);
            m_TargetPos = new Vector3(endPosX - 2f, startPosY, -10);
        }
        else
        {
            m_IsAnimating = false;
        }

        // init position
        if (m_IsAnimating)
        {
            ShowBanner();

            if (m_CurrentStageObj != null)
            {
                m_CurrentStageObj.transform.position = m_StartPos;

                // Level font follow with picture
                //if (m_StageType == InGameSystemStateType.LevelUp)
                //{
                //    Vector3 pos = m_StartPos;
                //    pos.x += 1.5f;    // follow with UI
                //    pos.y -= 0.1f;
                //    m_InGameStateText.transform.position = pos;
                //}
            }
        }
    }

    private void EndAnimation()
    {
        m_IsAnimating = true;
        m_AnimTime = 0f;
        m_IsEndAnimation = true;

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
        // middle→left
        if (m_StageType == InGameSystemStateType.TimeUp)
        {
            if (m_InGameType == InGameType.Tutorial)
            {
                m_CurrentStageObj.SetActive(false);
            }
            else
            {
                m_CurrentStageObj.SetActive(true);

                m_StartPos = new Vector3(startPosX, startPosY, -10);
                m_TargetPos = new Vector3(startPosX - 7f, startPosY, -10);
                //callTrigger = true;
            }
        }
        // 2026/08/04 Updated By Fate Ku
        else if (m_StageType == InGameSystemStateType.LevelUp)
        {
            m_StartPos = new Vector3(startPosX - 2f, startPosY, -10);
            m_TargetPos = new Vector3(startPosX - 6f, startPosY, -10);
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

                //if (m_StageType == InGameSystemStateType.LevelUp)
                //{
                //    Vector3 pos = m_StartPos;
                //    pos.x += 1.5f;
                //    pos.y -= 0.1f;
                //    m_InGameStateText.transform.position = pos;
                //}
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

        // -----------------------------------
        // Calculate duration by distance
        // -----------------------------------
        float distance = Vector3.Distance(m_StartPos, m_TargetPos);

        if (m_StageType == InGameSystemStateType.LevelUp)
        {
            m_AnimSpeed = 4f;
        }
        else if (m_StageType == InGameSystemStateType.TimeUp)
        {
            m_AnimSpeed = 7f;
        }
        else
        {
            m_AnimSpeed = 6f;
        }

        if (m_AnimSpeed > 0f)
        {
            m_AnimDuration = distance / m_AnimSpeed;
        }
        else
        {
            m_AnimDuration = 1f;
        }

        m_AnimTime += Time.deltaTime;

        float t = Mathf.Clamp01(m_AnimTime / m_AnimDuration);

        // -----------------------------------
        // Move
        // -----------------------------------
        Vector3 pos = Vector3.Lerp(m_StartPos, m_TargetPos, t);

        if (m_CurrentStageObj != null)
        {
            m_CurrentStageObj.transform.position = pos;
        }

        // -----------------------------------
        // Level number follows UI
        // -----------------------------------
        //if (m_StageType == InGameSystemStateType.LevelUp)
        //{
        //    Vector3 levelPos = pos;

        //    levelPos.x += 1.5f;
        //    levelPos.y -= 0.1f;

        //    m_InGameStateText.transform.position = levelPos;
        //}

        // -----------------------------------
        // Animation End
        // -----------------------------------
        if (t >= 1f)
        {
            m_IsAnimating = false;

            if (m_IsEndAnimation)
            {
                HideBanner();
                m_IsEndAnimation = false;
            }
            else if (m_StageType != InGameSystemStateType.LevelUp &&
                m_StageType != InGameSystemStateType.TimeUp)
            {
                HideBanner();
            }

            GameMng.Instance.CallInGameSystemStateTrigger();
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


    private void ShowBanner()
    {
        if (m_Banner != null)
        {
            m_Banner.SetActive(true);
        }
    }

    private void HideBanner()
    {
        if (m_Banner != null)
        {
            m_Banner.SetActive(false);
        }
    }

    // ---------------------------------------------------------
    // Level Number
    // ---------------------------------------------------------

    private void SetLevelNumber(int level)
    {
        if (m_NumberSprites == null || m_NumberSprites.Length < 10)
        {
            Debug.LogWarning("NumberSprites is missing or less than 10 sprites.");
            return;
        }

        HideLevelNumbers();

        // Level 1~9
        if (level >= 1 && level <= 9)
        {
            if (m_OnlyOneNumber != null)
            {
                m_OnlyOneNumber.sprite = m_NumberSprites[level];
                m_OnlyOneNumber.gameObject.SetActive(true);
            }
        }
        // Level 10 +
        else if (level >= 10)
        {
            int tens = level / 10;
            int ones = level % 10;
            if (m_TensNumber != null)
            {
                m_TensNumber.sprite = m_NumberSprites[tens];
                m_TensNumber.gameObject.SetActive(true);
            }
            if (m_OnesNumber != null)
            {
                m_OnesNumber.sprite = m_NumberSprites[ones];
                m_OnesNumber.gameObject.SetActive(true);
            }
        }

    }


    private void HideLevelNumbers()
    {
        if (m_OnlyOneNumber != null)
        {
            m_OnlyOneNumber.gameObject.SetActive(false);
        }

        if (m_TensNumber != null)
        {
            m_TensNumber.gameObject.SetActive(false);
        }

        if (m_OnesNumber != null)
        {
            m_OnesNumber.gameObject.SetActive(false);
        }
    }


}
