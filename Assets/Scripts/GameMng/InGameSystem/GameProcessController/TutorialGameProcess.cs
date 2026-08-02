//
// TutorialGameProcess.cs
// 
// 2026/06/29 Created By Man-Yi, Yeh
// 2026/06/30 Updated By Man-Yi, Yeh
// 2026/07/06 Updated By Man-Yi, Yeh
// 2026/07/07 Updated By Man-Yi, Yeh
// 2026/07/13 Updated By Man-Yi, Yeh
// 2026/07/14 Updated By Man-Yi, Yeh
// 2026/08/01 Updated By Fate Ku
// 


using System.Collections.Generic;
using UnityEngine;


public class TutorialGameProcess : IGameProcessController
{
    private TutorialTest m_TutorialTest;

    // 2026/08/01 Updated By Fate Ku
    private TutorialInfo m_TutorialInfo;

    //private List<BlockType> m_NextBlockType = new();
    //private List<int> m_NextBlockCol = new();
    private List<TutorialNextBlockData> m_NextSteps = new();
    // 2026/08/01 Updated By Fate Ku


    //m_Index = index for type
    //m_Index - 1 = index for col
    private int m_Index = -1;

    private bool m_FirstStepEnd = false;

    private bool m_AllIdlePreviousFrame = true;

    public TutorialGameProcess(InGameSystem inGameSystem)
        : base(inGameSystem,
            "Data/ProcessData/TutorialProcessData",
            "Data/EventData/TutorialEventData")
    {
        // 2026/08/01 Updated By Fate Ku
        GameObject tutorialInfoObj = GameObject.Find("TutorialInfo");

        if (tutorialInfoObj != null)
        {
            m_TutorialInfo = tutorialInfoObj.GetComponent<TutorialInfo>();

            m_TutorialInfo.GetInstructionsText().gameObject.SetActive(false);
            m_TutorialInfo.GetClickMark().SetActive(false);
        }
        // 2026/08/01 Updated By Fate Ku

        GameObject tutorialTestObj = GameObject.Find("TutorialTest");
        if (tutorialTestObj != null)
        {
            m_TutorialTest = tutorialTestObj.GetComponent<TutorialTest>();
            m_TutorialTest.SetActive(false);
        }

        //-------------------
        //Start Block Data
        //-------------------
        TextAsset jsonTextAsset = Resources.Load<TextAsset>("Data/Tutorial/TutorialStartBlockData");
        Debug.Log("Start Block Data: " + jsonTextAsset);
        TutorialStartBlockDataList startBlockDataSet =
            JsonUtility.FromJson<TutorialStartBlockDataList>(jsonTextAsset.text);

        foreach (TutorialStartBlockData blockData in startBlockDataSet.list)
        {
            BlockType type = blockData.type;
            Vector2Int id = new(blockData.col, blockData.row);

            m_InGameSystem.AddBlock(type, id);
        }

        //-------------------
        //Next Block Data
        //-------------------
        jsonTextAsset = Resources.Load<TextAsset>("Data/Tutorial/TutorialNextBlockData");
        Debug.Log("Start Block Data: " + jsonTextAsset);
        TutorialNextBlockDataList nextBlockDataSet =
            JsonUtility.FromJson<TutorialNextBlockDataList>(jsonTextAsset.text);

        foreach (TutorialNextBlockData blockData in nextBlockDataSet.list)
        {
            // 2026/08/01 Updated By Fate Ku
            //m_NextBlockType.Add(blockData.type);
            //m_NextBlockCol.Add(blockData.col);
            m_NextSteps.Add(blockData);
            // 2026/08/01 Updated By Fate Ku
        }
    }

    public override void OperateControl()
    {
        if (m_FirstStepEnd)
        {
            base.OperateControl();
        }
        else
        {
            if (!m_AllIdlePreviousFrame)
            {
                m_AllIdlePreviousFrame = m_InGameSystem.IsAllBlocksIdle();
            }
            else
            {
                if (m_InGameSystem.IsAllBlocksIdle())
                {
                    m_InGameSystem.CanOperate = true;

                    // 2026/08/01 Updated By Fate Ku
                    //if (m_Index - 1 < m_NextBlockCol.Count)
                    //{
                    //    m_TutorialTest.SetActive(true);
                    //    m_TutorialTest.SetCol(m_NextBlockCol[m_Index - 1]);
                    //}
                    if (m_Index - 1 < m_NextSteps.Count)
                    {
                        TutorialNextBlockData step = m_NextSteps[m_Index - 1];

                        // ClickMark
                        m_TutorialTest.SetActive(true);
                        m_TutorialTest.SetCol(step.col);

                        // type = sakura can click
                        if (m_Index - 1 == m_NextSteps.Count - 1)
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

                        // ClickMark(UI)
                        m_TutorialInfo.GetClickMark().SetActive(true);
                        Vector2 pos = GameMng.Instance.GetBgVirtualCubePosition(step.col, 4);
                        Vector3 spawnPos = new Vector3(pos.x, pos.y, -10f);
                        m_TutorialInfo.GetClickMark().transform.position = spawnPos;
                        m_TutorialInfo.GetClickMark().transform.localScale = Vector3.one * 0.1f;

                    }
                    else
                    {
                        m_FirstStepEnd = true;
                        GameMng.Instance.SetAllowColumn(-1);

                        m_TutorialTest.SetActive(false);

                        m_TutorialInfo.GetInstructionsText().gameObject.SetActive(false);
                        m_TutorialInfo.GetClickMark().SetActive(false);
                    }
                }
                else
                {
                    m_InGameSystem.CanOperate = false;
                    m_AllIdlePreviousFrame = false;
                    m_TutorialTest.SetActive(false);

                    m_TutorialInfo.GetInstructionsText().gameObject.SetActive(false);
                    m_TutorialInfo.GetClickMark().SetActive(false);
                }
                // 2026/08/01 Updated By Fate Ku
            }
        }
    }

    public override BlockType GetNowBlockType()
    {
        BlockType res = BlockType.Sakura;

        if (!m_FirstStepEnd)
        {
            m_Index += 1;

            // 2026/08/01 Updated By Fate Ku
            //if (m_Index < m_NextBlockType.Count)
            //{
            //    res = m_NextBlockType[m_Index];
            //}
            if (m_Index < m_NextSteps.Count)
            {
                res = m_NextSteps[m_Index].type;
            }
            // 2026/08/01 Updated By Fate Ku
        }

        return res;
    }

    public override void TimeControl()
    {
        if (GameMng.Instance.GetBlockDestroyNum(BlockType.Sakura) > 0)
        {
            m_GameTimer = 0;
        }
    }
    public override void EventControl() { }
    public override bool CheckLevelUp() { return false; }
}
