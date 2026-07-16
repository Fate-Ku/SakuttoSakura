//
// BlockPosInfo.cs
// 
// 2026/07/09 Created By Fate Ku
// 2026/07/16 Created By Fate Ku
//

using UnityEngine;

public class BlockPosInfo : MonoBehaviour
{
    //number or col and row
    [Header("Scale")]
    [SerializeField] private int colNum;
    [SerializeField] private int rowNum;

    [Header("Block")]
    [SerializeField] private GameObject[] blocks;

    [Header("Materials")]
    public Material matTsubaki;
    public Material matKaede;
    public Material matHimawari;
    public Material matClover;
    public Material matAsagao;
    public Material matKikyou;
    public Material matSakura;

    [Header("Direction")]
    public GameObject Down;
    public GameObject DownLeft;
    public GameObject DownRight;
    public GameObject Left;
    public GameObject LeftDown;
    public GameObject Right;
    public GameObject RightDown;


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

    public GameObject GetBlock(BlockType type)
    {
        GameObject res = blocks[(int)type];
        return res;
    }

    public Material GetMatTsubaki()
    {
        return matTsubaki;
    }

    public Material GetMatKaede()
    {
        return matKaede;
    }

    public Material GetMatHimawari()
    {
        return matHimawari;
    }

    public Material GetMatClover()
    {
        return matClover;
    }

    public Material GetMatAsagao()
    {
        return matAsagao;
    }

    public Material GetMatKikyou()
    {
        return matKikyou;
    }

    public Material GetMatSakura()
    {
        return matSakura;
    }
}
