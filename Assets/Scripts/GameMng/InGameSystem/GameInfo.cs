//
// GameInfo.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
// 2026/05/30 Updated By Man-Yi, Yeh
// 2026/06/02 Updated By Man-Yi, Yeh
// 2026/06/06 Updated By Man-Yi, Yeh
// 2026/06/08 Updated By Man-Yi, Yeh
// 2026/06/09 Updated By Man-Yi, Yeh
// 2026/06/10 Updated By Man-Yi, Yeh
// 2026/06/23 Updated By Fate Ku 
// 2026/06/29 Updated By Man-Yi, Yeh
// 

using UnityEngine;
using TMPro;
using System;


public class GameInfo : MonoBehaviour
{
    [Header("Next Block")]
    [SerializeField] private Transform nextBlockPos;

    //number or col and row
    [Header("Scale")]
    [SerializeField] private int colNum;
    [SerializeField] private int rowNum;

    [Header("Block")]
    [SerializeField] private GameObject[] blocks;
    [SerializeField] private float[] blocksFallSpeed;

    [Header("Operate Time")]
    [SerializeField] private float nextOperateTime;

    [Header("Combine Info")]
    [SerializeField] private float combineTime;
    [SerializeField] private int combineSize;

    [Header("Event Info")]
    [SerializeField, Range(1, 5)] private int floorNum;
    [SerializeField] private float enevtInterval;

    [Header("Level Up Info")]
    [SerializeField] private int levelUpSakuraNum;
    [SerializeField] private float levelUpAddGameTime;
    public int nowLevel;

    [Header("Test")]
    [SerializeField, Range(1, 7)] private int blockTypeQty;
    [SerializeField] private TextMeshProUGUI testInGameStateText;
    [SerializeField] private float startTime;
    [SerializeField] private float playTime;
    [SerializeField] private float levelUpTime;
    [SerializeField] private float timeUpTime;
    [SerializeField] private float gameOverTime;
    

    public Vector2 GetReferPos()
    {
        float x = gameObject.transform.position.x;
        float y = gameObject.transform.position.y;

        return new Vector2(x, y);
    }

    public Vector2 GetNextBlockPos()
    {
        float x = nextBlockPos.position.x;
        float y = nextBlockPos.position.y;

        return new Vector2(x, y);
    }

    //x :col, y :row
    public Vector2Int GetScale()
    {
        return new Vector2Int(colNum, rowNum);
    }

    public float GetSize()
    {
        return gameObject.transform.localScale.x;
    }

    public GameObject GetBlock(BlockType type)
    {
        GameObject res = blocks[(int)type];
        return res;
    }

    public float GetBlockFallSpeed(BlockType type)
    {
        float res = blocksFallSpeed[(int)type];
        return res;
    }

    public float GetNextOperateTime()
    {
        return nextOperateTime;
    }

    public float GetCombineTime()
    {
        return combineTime;
    }

    public int GetCombineSize()
    {
        return combineSize;
    }

    public int GetFloorNum()
    {
        return floorNum;
    }

    public float GetEventInterval()
    {
        return enevtInterval;
    }

    public int GetLevelUpSakuraNum()
    {
        return levelUpSakuraNum;
    }

    public float GetLevelUpAddGameTime()
    {
        return levelUpAddGameTime;
    }

    //-------------------
    //test
    //-------------------
    public int GetBlockTypeQty()
    {
        return blockTypeQty;
    }

    public TextMeshProUGUI GetTestInGameStateText()
    {
        return testInGameStateText;
    }

    public float GetStartTime()
    {
        return startTime;
    }

    public float GetPlayTime()
    {
        return playTime;
    }

    public float GetLevelUpTime()
    {
        return levelUpTime;
    }

    public float GetTimeUpTime()
    {
        return timeUpTime;
    }

    public float GetGameOverTime()
    {
        return gameOverTime;
    }
}
