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
// 

using UnityEngine;
using TMPro;


public class GameInfo : MonoBehaviour
{
    //number or col and row
    [Header("Scale")]
    [SerializeField] private int colNum;
    [SerializeField] private int rowNum;

    [Header("Block")]
    [SerializeField] private GameObject[] blocks;

    [Header("Operate Time")]
    [SerializeField] private float nextOperateTime;

    [Header("Combine Info")]
    [SerializeField] private float combineTime;
    [SerializeField] private int combineSize;

    [Header("Test")]
    [SerializeField, Range(1, 7)] private int blockTypeQty;
    [SerializeField] private TextMeshProUGUI testInGameStateText;
    [SerializeField] private TextMeshProUGUI testTimeText;
    [SerializeField] private float startTime;
    [SerializeField] private float playTime;
    [SerializeField] private float timeUpTime;
    [SerializeField] private float gameOverTime;


    //x :col, y :row
    public Vector2Int GetScale()
    {
        return new Vector2Int(colNum, rowNum);
    }

    public Vector2 GetReferPos()
    {
        float x = gameObject.transform.position.x;
        float y = gameObject.transform.position.y;

        return new Vector2 (x, y);
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

    public TextMeshProUGUI GetTestTimeText()
    {
        return testTimeText;
    }

    public float GetStartTime()
    {
        return startTime;
    }

    public float GetPlayTime()
    {
        return playTime;
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
