//
// GameInfo.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class GameInfo : MonoBehaviour
{
    //number or col and row
    [SerializeField] private int colNum;
    [SerializeField] private int rowNum;

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
}
