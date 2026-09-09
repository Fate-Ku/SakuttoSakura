//
// TutorialGameProcess.cs
// 
// 2026/09/08 Created By Fate Ku

using System.Collections.Generic;
using UnityEngine;

public class TutorialGameProcessUI
{
    private TutorialInfo m_TutorialInfo;

    private List<TutorialNextBlockData> m_NextSteps = new();

    // JSON index
    private int m_Index = -1;

    private InGameType m_InGameType;

    private bool m_PreviousCanOperate = true;

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

        if (canOperate)
        {
            ShowCurrentStep();
        }

        // ========================================
        // true → false
        // Step++
        // ========================================

        if (m_PreviousCanOperate && !canOperate)
        {
            m_TutorialInfo.GetClickMark().SetActive(false);
            m_TutorialInfo.GetTapFrame().SetActive(false);
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

        if (m_Index == m_NextSteps.Count - 1)
        {
            GameMng.Instance.SetAllowColumn(-1);
        }
        else
        {
            GameMng.Instance.SetAllowColumn(step.col);
        }

        // Instruction
        m_TutorialInfo.GetInstructionsText().gameObject.SetActive(true);
        m_TutorialInfo.GetInstructionsText().text = step.text;

        // info
        m_TutorialInfo.GetInfoText().gameObject.SetActive(true);
        m_TutorialInfo.GetInfoText().text = step.info;

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

    public void NextStep()
    {
        if (m_Index < m_NextSteps.Count)
        {
            m_Index++;

            Debug.Log("Next Tutorial Step : " + m_Index);
        }
    }
}