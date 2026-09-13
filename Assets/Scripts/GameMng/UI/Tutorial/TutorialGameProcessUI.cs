//
// TutorialGameProcessUI.cs
// 
// 2026/09/08 Created By Fate Ku
// 2026/09/12 Updated By Fate Ku
// 2026/09/13 Updated By Fate Ku
//

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class TutorialGameProcessUI
{
    private TutorialInfo m_TutorialInfo;

    private List<TutorialNextBlockData> m_NextSteps = new();

    // JSON index
    private int m_Index = -1;

    private InGameType m_InGameType;

    private bool m_PreviousCanOperate = true;

    private bool m_NextInfo = true;

    private bool m_btnLock = false;

    private bool m_Freeze = true;

    public TutorialGameProcessUI(InGameType inGameType)
    {
        m_InGameType = inGameType;
    }

    public void Init()
    {

        GameObject tutorialInfoObj = GameObject.Find("TutorialInfo");

        if (tutorialInfoObj != null)
        {
            m_TutorialInfo = tutorialInfoObj.GetComponent<TutorialInfo>();

            m_TutorialInfo.GetInfoText().gameObject.SetActive(false);
            m_TutorialInfo.GetInstructionsText().gameObject.SetActive(false);
            m_TutorialInfo.GetClickMark().SetActive(false);
            m_TutorialInfo.GetTapFrame().SetActive(false);
            m_TutorialInfo.GetInfoFrame().SetActive(false);
            m_TutorialInfo.GetTapWordAnim().SetActive(false);
        }

        //-------------------
        // Info Data
        //-------------------

        string infoDataPath = "Data/Tutorial/TGSTutorialInfoData";

        TextAsset jsonTextAsset = Resources.Load<TextAsset>(infoDataPath);

        if (jsonTextAsset == null)
        {
            Debug.LogError("No Tutorial Info Data: " + infoDataPath);
            return;
        }

        TutorialNextBlockDataList infoDataDataSet =
            JsonUtility.FromJson<TutorialNextBlockDataList>(
                jsonTextAsset.text
            );

        m_NextSteps.Clear();

        foreach (TutorialNextBlockData infoData in infoDataDataSet.list)
        {
            m_NextSteps.Add(infoData);
        }

        Debug.Log("Tutorial Step Count : " + m_NextSteps.Count);
    }

    public void Update()
    {
        if (m_InGameType != InGameType.Tutorial)
            return;

        bool canOperate = GameMng.Instance.GetCanOperate();

        // ========================================
        // now Step
        // ========================================

        if (canOperate && m_NextInfo && !m_TutorialInfo.GetPopupPanel().activeSelf)
        {
            ShowCurrentStep();
        }

        // ========================================
        // btn lock -> unlock
        // for click/touch
        // ========================================
        bool mouseReleased =
            Mouse.current != null &&
            Mouse.current.leftButton.wasReleasedThisFrame;

        bool touchReleased =
            Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasReleasedThisFrame;

        if (m_btnLock &&
           (mouseReleased || touchReleased) &&
           (m_Index == 0 || m_Index == 2)
           && !m_TutorialInfo.GetPopupPanel().activeSelf)
        {
            if (IsClickTutorialButton())
            {
                m_TutorialInfo.GetInfoFrame().SetActive(false);
                m_TutorialInfo.GetTapWordAnim().SetActive(false);

                m_btnLock = false;

                NextStep();
            }
        }

        // ========================================
        // true → false
        // Step++
        // ========================================

        if (m_PreviousCanOperate && !canOperate && m_NextInfo && !m_TutorialInfo.GetPopupPanel().activeSelf)
        {
            m_TutorialInfo.GetClickMark().SetActive(false);
            m_TutorialInfo.GetTapFrame().SetActive(false);
            m_TutorialInfo.GetInfoFrame().SetActive(false);
            m_TutorialInfo.GetTapWordAnim().SetActive(false);
            NextStep();
        }

        m_PreviousCanOperate = canOperate;
    }

    private void ShowCurrentStep()
    {

        if (m_Index >= m_NextSteps.Count)
        {
            GameMng.Instance.SetAllowColumn(-1);

            //m_TutorialInfo.GetInfoText().gameObject.SetActive(false);
            //m_TutorialInfo.GetInstructionsText().gameObject.SetActive(false);
            m_TutorialInfo.GetClickMark().SetActive(false);
            m_TutorialInfo.GetTapFrame().SetActive(false);

            return;
        }

        TutorialNextBlockData step = m_NextSteps[m_Index];

        //bool mouseClick =
        //    Mouse.current != null &&
        //    Mouse.current.leftButton.wasPressedThisFrame;

        //bool touchClick =
        //    Touchscreen.current != null &&
        //    Touchscreen.current.primaryTouch.press.wasPressedThisFrame;

        //bool isClicked = mouseClick || touchClick;

        // click judge
        if (m_Index == m_NextSteps.Count - 1)
        {
            GameMng.Instance.SetAllowColumn(-1);
        }
        else if (m_Index == 0 || m_Index == 2)
        {
            GameMng.Instance.UnlockButtonOperation();
            m_btnLock = true;
        }
        //else if (m_Index == 3)
        //{
        //    if (isClicked)
        //    //if (isClicked && !m_Freeze)
        //    {
        //        GameMng.Instance.UnlockButtonOperation();
        //        m_Freeze = true;
        //        Debug.Log("!isPressed && !m_Freeze : " + m_Freeze);
        //    }
        //    else
        //    {
        //        GameMng.Instance.SetAllowColumn(step.col);
        //        m_Freeze = false;
        //        Debug.Log("m_Freeze : " + m_Freeze);
        //    }
        //}
        else
        {
            GameMng.Instance.SetAllowColumn(step.col);
        }

        // context
        if (m_Index == 0 || m_Index == 2)
        {
            if (m_Index == 0)
            {
                m_TutorialInfo.GetInfoFrame().SetActive(true);
            }
            m_TutorialInfo.GetTapWordAnim().SetActive(true);

            // Instruction
            m_TutorialInfo.GetInstructionsText().gameObject.SetActive(true);
            m_TutorialInfo.GetInstructionsText().text = step.text;

        }
        else
        {
            // Instruction
            m_TutorialInfo.GetInstructionsText().gameObject.SetActive(true);
            m_TutorialInfo.GetInstructionsText().text = step.text;

            // info
            //m_TutorialInfo.GetInfoText().gameObject.SetActive(true);
            //m_TutorialInfo.GetInfoText().text = step.info;

            int index = m_Index + 1;
            if (index != m_NextSteps.Count)
            {
                // ClickMark
                m_TutorialInfo.GetClickMark().SetActive(true);

                Vector2 pos =
                    GameMng.Instance.GetBgVirtualCubePosition(step.col, 4);

                Vector3 spawnPos =
                    new Vector3(pos.x + 0.2f, pos.y, -10f);

                m_TutorialInfo.GetClickMark().transform.position = spawnPos;
                m_TutorialInfo.GetClickMark().transform.localScale = Vector3.one * 0.4f;

                // Tap Frame
                m_TutorialInfo.GetTapFrame().gameObject.SetActive(true);

                Vector2 pos2 =
                    GameMng.Instance.GetBgVirtualCubePosition(step.col, 0);

                Vector3 tapPos =
                    new Vector3(pos2.x + 0.2f, pos2.y - 0.5f, -5f);

                m_TutorialInfo.GetTapFrame().transform.position = tapPos;
            }
        }

    }

    public void NextStep()
    {
        if (m_Index < m_NextSteps.Count)
        {
            m_Index++;

            Debug.Log("Next Tutorial Step : " + m_Index);
        }
    }

    private bool IsClickTutorialButton()
    {
        Vector2 pointerPosition;

        if (Mouse.current != null &&
            Mouse.current.leftButton.wasReleasedThisFrame)
        {
            pointerPosition = Mouse.current.position.ReadValue();
            Debug.Log("Mouse Click ");
        }
        else if (Touchscreen.current != null &&
                 Touchscreen.current.primaryTouch.press.wasReleasedThisFrame)
        {
            pointerPosition =
                Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else
        {
            return false;
        }

        PointerEventData eventData =
            new PointerEventData(EventSystem.current);

        eventData.position = pointerPosition;

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventData, results);

        GameObject clickBtn1 = m_TutorialInfo.GetClickBtn1();
        GameObject clickBtn2 = m_TutorialInfo.GetClickBtn2();

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == clickBtn1 ||
                result.gameObject.transform.IsChildOf(clickBtn1.transform) ||
                result.gameObject == clickBtn2 ||
                result.gameObject.transform.IsChildOf(clickBtn2.transform))
            {
                Debug.Log("Mouse Click1/2 ");
                return true;
            }
        }
        Debug.Log("NO Mouse Click");
        return false;
    }

}