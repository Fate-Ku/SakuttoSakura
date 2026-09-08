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
// 2026/08/12 Updated By Man-Yi, Yeh
// 2026/09/09 Updated By Fate Ku
// 


using System.Collections.Generic;
using UnityEngine;


public class TutorialGameProcess : IGameProcessController
{
    private TutorialTest m_TutorialTest;


    private List<BlockType> m_NextBlockType = new();
    private List<int> m_NextBlockCol = new();



    //m_Index = index for type
    //m_Index - 1 = index for col
    private int m_Index = -1;

    private bool m_FirstStepEnd = false;

    private bool m_AllIdlePreviousFrame = true;

    public TutorialGameProcess(InGameSystem inGameSystem, bool isTGS)
        : base(inGameSystem,
            "Data/ProcessData/TutorialProcessData",
            "Data/EventData/TutorialEventData",
            "Data/NextBGMData/TutorialNextBGMData",
            1)
    {
        
        GameObject tutorialTestObj = GameObject.Find("TutorialTest");
        if (tutorialTestObj != null)
        {
            m_TutorialTest = tutorialTestObj.GetComponent<TutorialTest>();
            m_TutorialTest.SetActive(false);
        }
        

        //-------------------
        //Block Data
        //-------------------
        string startBlockDataPath = "Data/Tutorial/TutorialStartBlockData";
        string nextBlockDataPath = "Data/Tutorial/TutorialNextBlockData";
        if (isTGS)
        {
            startBlockDataPath = "Data/Tutorial/TGSTutorialStartBlockData"; 
            nextBlockDataPath = "Data/Tutorial/TGSTutorialNextBlockData";
        }

        //start block data
        TextAsset jsonTextAsset = Resources.Load<TextAsset>(startBlockDataPath);
        Debug.Log("Start Block Data: " + jsonTextAsset);
        TutorialStartBlockDataList startBlockDataSet =
            JsonUtility.FromJson<TutorialStartBlockDataList>(jsonTextAsset.text);

        foreach (TutorialStartBlockData blockData in startBlockDataSet.list)
        {
            BlockType type = blockData.type;
            Vector2Int id = new(blockData.col, blockData.row);

            m_InGameSystem.AddBlock(type, id);
        }

        //next block data
        jsonTextAsset = Resources.Load<TextAsset>(nextBlockDataPath);
        Debug.Log("Next Block Data: " + jsonTextAsset);
        TutorialNextBlockDataList nextBlockDataSet =
            JsonUtility.FromJson<TutorialNextBlockDataList>(jsonTextAsset.text);

        foreach (TutorialNextBlockData blockData in nextBlockDataSet.list)
        {
            m_NextBlockType.Add(blockData.type);
            m_NextBlockCol.Add(blockData.col);
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

                    // 2026/09/09 Updated By Fate Ku
                    //if (m_Index -1 < m_NextBlockCol.Count)
                    if (m_Index < m_NextBlockCol.Count)
                    // 2026/09/09 Updated By Fate Ku
                    {
                        m_TutorialTest.SetActive(true);
                        m_TutorialTest.SetCol(m_NextBlockCol[m_Index - 1]);
                    }
                    else
                    {
                        m_FirstStepEnd = true;
                    }
                }
                else
                {
                    m_InGameSystem.CanOperate = false;
                    m_AllIdlePreviousFrame = false;
                    m_TutorialTest.SetActive(false);
                }
            }
        }
    }

    public override BlockType GetNowBlockType()
    {
        BlockType res = BlockType.Sakura;

        if (!m_FirstStepEnd)
        {
            m_Index += 1;

            if (m_Index < m_NextBlockType.Count)
            {
                res = m_NextBlockType[m_Index];
            }
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
