//
// BlockFactory.cs
// 
// 2026/07/03 Created By Man-Yi, Yeh
// 2026/07/05 Updated By Man-Yi, Yeh
// 

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FlowerFallData
{
    public BlockType type;
    public FallData data;
}

[Serializable]
public class FlowerFallDataList
{
    public FlowerFallData[] list;
}

public class BlockFactory
{
    private float m_Size;
    private Dictionary<BlockType, GameObject> m_BlockObs = new();
    private Dictionary<BlockType, FallData> m_BlockFallDatas = new();

    public BlockFactory(GameInfo gameInfo)
    {
        //-------------------
        //block info
        //-------------------
        //size
        m_Size = gameInfo.GetSize();
        //blockOb
        for (int i = 0; i < (int)BlockType.Count; i++)
        {
            bool isAdded = m_BlockObs.TryAdd((BlockType)i, gameInfo.GetBlock((BlockType)i));
            if (!isAdded)
            {
                Debug.Log("TryAdd failed for GameObject:" + ((BlockType)i).ToString());
            }
        }

        //-------------------
        //FallData
        //-------------------
        string jsonFilePath = "Data/FlowerFallPath";
        TextAsset jsonTextAsset = Resources.Load<TextAsset>(jsonFilePath);
        Debug.Log("Json file test: " + jsonTextAsset.text);
        FlowerFallDataList dataSet =
            JsonUtility.FromJson<FlowerFallDataList>(jsonTextAsset.text);

        foreach (FlowerFallData data in dataSet.list)
        {
            bool isAdded = m_BlockFallDatas.TryAdd(data.type, data.data);
            if (!isAdded)
            {
                Debug.Log("FallData TryAdd failed for Type:" + data.type.ToString());
            }
        }
        
        for (int i = 0; i < (int)BlockType.Count; ++i)
        {
            if (m_BlockFallDatas.TryGetValue((BlockType)i,out var fallData))
            {
                string text = 
                    "Json test:" +
                    ((BlockType)i).ToString() +
                    " " + 
                    fallData.pathDatas.Length.ToString();

                Debug.Log(text);
            }
        }
        
    }

    public IBlock GetBlock(BlockType type)
    {
        IBlock res = null;

        switch (type)
        {
            case BlockType.None:
                break;

            case BlockType.Count:
                break;

            case BlockType.SoftRock:
                res = GetSoftRockBlock();
                break;

            case BlockType.HardRock:
                res = GetHardRockBlock();
                break;

            case BlockType.TimeItem:
                res = GetTimeItemBlock();
                break;

            default:
                res = GetFlowerBlock(type);
                break;
        }

        return res;
    }

    //-------------------
    //create function
    //-------------------
    private IBlock GetFlowerBlock(BlockType type)
    {
        IBlock res = null;

        GameObject blockOb = GetBlockOb(type);
        FallData fallData = GetFallData(type);
        if (fallData != null)
        {
            res = new FlowerBlock(blockOb, m_Size, type, fallData);
        }

        return res;
    }

    private IBlock GetSoftRockBlock()
    {
        IBlock res = null;

        GameObject blockOb = GetBlockOb(BlockType.SoftRock);
        FallData fallData = GetFallData(BlockType.SoftRock);
        if (blockOb != null && fallData != null)
        {
            res = new SoftRockBlock(blockOb, m_Size, fallData.basicSpeed);
        }

        return res;
    }

    private IBlock GetHardRockBlock() 
    {
        IBlock res = null;

        IBlock softRockBlock = GetSoftRockBlock();
        GameObject blockOb = GetBlockOb(BlockType.HardRock);
        FallData fallData = GetFallData(BlockType.HardRock);
        if (softRockBlock != null && blockOb != null && fallData != null)
        {
            res = new HardRockBlock(softRockBlock, blockOb, m_Size, fallData.basicSpeed);
        }

        return res;
    }

    private IBlock GetTimeItemBlock()
    {
        IBlock res = null;

        GameObject blockOb = GetBlockOb(BlockType.TimeItem);
        FallData fallData = GetFallData(BlockType.TimeItem);
        if (blockOb != null && fallData != null)
        {
            res = new TimeItemBlock(blockOb, m_Size, fallData.basicSpeed);
        }

        return res;
    }

    //-------------------
    //basic function
    //-------------------

    private GameObject GetBlockOb(BlockType type)
    {
        GameObject res = null;
        
        if (m_BlockObs.TryGetValue(type, out var blockOb))
        {
            res = blockOb;
        }

        return res;
    }

    private FallData GetFallData(BlockType flowerType)
    {
        FallData res = null;

        if (m_BlockFallDatas.TryGetValue(flowerType, out var fallData))
        {
            res = fallData;
        }

        return res;
    }
}
