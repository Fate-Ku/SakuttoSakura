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
// 2026/07/06 Updated By Man-Yi, Yeh
// 2026/07/09 Updated By Fate Ku 
//

using UnityEngine;
using TMPro;
using System;


public class GameInfo : MonoBehaviour
{
    //number or col and row
    [Header("Scale")]
    [SerializeField] private int colNum;
    [SerializeField] private int rowNum;

    [Header("Block")]
    [SerializeField] private GameObject[] blocks;

    [Header("Next Block")]
    [SerializeField] private GameObject nextBlockPos;
    [SerializeField] private GameObject nextNextBlockPos;

    [Header("Operate Time")]
    [SerializeField] private float nextOperateTime;

    [Header("Combine Info")]
    [SerializeField] private float combineTime;
    [SerializeField] private int combineSize;

    [Header("Event Info")]
    [SerializeField, Range(1, 5)] private int floorNum;

    [Header("Level Up Info")]
    [SerializeField] private float levelUpAddGameTime;
    public int nowLevel;

    [Header("Test")]
    [SerializeField] private float playTime;


    //x :col, y :row
    public Vector2Int GetScale()
    {
        return new Vector2Int(colNum, rowNum);
    }

    public Vector2 GetReferPos()
    {
        float x = gameObject.transform.position.x;
        float y = gameObject.transform.position.y;

        return new Vector2(x, y);
    }

    public float GetSize()
    {
        return gameObject.transform.localScale.x;
    }

    public Vector3 GetNextBlockPos()
    {
        return nextBlockPos.transform.position;
    }

    public float GetNextBlockSize()
    {
        return nextBlockPos.transform.localScale.x;
    }

    public Vector2 GetNextNextBlockPos()
    {
        return nextNextBlockPos.transform.position;
    }

    public float GetNextNextBlockSize()
    {
        return nextNextBlockPos.transform.localScale.x;
    }

    public GameObject GetBlock(BlockType type)
    {
        GameObject res = blocks[(int)type];
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

    public float GetLevelUpAddGameTime()
    {
        return levelUpAddGameTime;
    }

    //-------------------
    //test
    //-------------------
    public float GetPlayTime()
    {
        return playTime;
    }
}
