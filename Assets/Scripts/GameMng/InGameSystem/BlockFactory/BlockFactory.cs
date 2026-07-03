//
// BlockFactory.cs
// 
// 2026/07/03 Created By Man-Yi, Yeh
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
    private Dictionary<BlockType, FallData> m_BlockFallDatas = new();

    public BlockFactory()
    {
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
        
        for (int i = 0; i < 7; ++i)
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
}
