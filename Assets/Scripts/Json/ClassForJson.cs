//
// ClassForJson.cs
// 
// 2026/07/17 Created By Man-Yi, Yeh
// 2026/08/01 Updated By Fate Ku
//


using System;
using UnityEngine;


//-------------------
//Fall Data
//-------------------
[Serializable]
public class FallPathData
{
    public FallDirection direction;
    public float speed;
}

[Serializable]
public class FallData
{
    public float basicSpeed;
    public FallPathData[] pathDatas;
}

[Serializable]
public class BlockFallData
{
    public BlockType type;
    public FallData data;
}

[Serializable]
public class BlockFallDataList
{
    public BlockFallData[] list;
}

//-------------------
//Process Data
//-------------------
[Serializable]
public class ProcessData
{
    public int levelUpSakuraNum;
    public int typeQty;
    public float eventInterval;
}

[Serializable]
public class LevelProcessData
{
    public int level;
    public ProcessData data;
}

[Serializable]
public class LevelProcessDataList
{
    public LevelProcessData[] list;
}

//-------------------
//Event Data
//-------------------
[Serializable]
public class BlockTypeRateData
{
    public BlockType type;
    public float rate;
}

[Serializable]
public class FloorData
{
    public float createRate;
    public BlockTypeRateData[] typeRateDatas;
}

[Serializable]
public class LevelFloorData
{
    public int level;
    public FloorData[] datas;
}

[Serializable]
public class LevelFloorDataList
{
    public LevelFloorData[] list;
}


//-------------------
//Start Block Data
//-------------------
[Serializable]
public class TutorialStartBlockData
{
    public BlockType type;
    public int col;
    public int row;
}

[Serializable]
public class TutorialStartBlockDataList
{
    public TutorialStartBlockData[] list;
}

//-------------------
//Next Block Data
//-------------------
[Serializable]
public class TutorialNextBlockData
{
    public BlockType type;
    public int col;
    public string text; // 2026/08/01 Updated By Fate Ku
}

[Serializable]
public class TutorialNextBlockDataList
{
    public TutorialNextBlockData[] list;
}
