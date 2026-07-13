//
// InGameUIBackground.cs
// 
// 2026/06/06 Created By Fate Ku
// 2026/06/24 Updated By Fate Ku
// 2026/07/10 Updated By Fate Ku
// 2026/07/11 Updated By Fate Ku
//

using System.Collections.Generic;
using UnityEngine;

public class InGameUIBackground
{
    private Texture2D texWhite; // white board texture
    private Texture2D texBlack; // black board texture

    // save bg data
    private Dictionary<Vector2Int, BgVirtualCubeData> m_BgVirtualCubeDatas = new Dictionary<Vector2Int, BgVirtualCubeData>();

    public Dictionary<Vector2Int, BgVirtualCubeData> BgVirtualCubeDatas => m_BgVirtualCubeDatas;

    private Dictionary<Vector2Int, BgCubeData> m_BgCubeDatas = new Dictionary<Vector2Int, BgCubeData>();

    public Dictionary<Vector2Int, BgCubeData> BgBgCubeDatas => m_BgCubeDatas;

    //-------------------
    //Info
    //-------------------
    //blockPos info
    private BlockPosInfo m_BlockPosInfo;

    public BlockPosInfo BlockPosInfo
    {
        get { return m_BlockPosInfo; }
    }

    public void Init()
    {
        //-------------------
        //Info
        //-------------------
        //game info
        GameObject blockInfo = GameObject.Find("BlockPosInfo");
        if (blockInfo != null)
        {
            m_BlockPosInfo = blockInfo.GetComponent<BlockPosInfo>();
        }

        // Loading Picture（Resources/UI/InGame）
        texWhite = Resources.Load<Texture2D>("UI/InGame/GbW");
        texBlack = Resources.Load<Texture2D>("UI/InGame/GbB");

        if (texWhite == null || texBlack == null)
        {
            Debug.LogError("Load error！plz check route：Assets/Resources/UI/InGame/");
        }

        CreateBgCube();
        CreateVirtualBgCube();
    }

    // -------------------------
    // delete BgCubes
    // -------------------------

    public void Term()
    {
        GameObject[] bgCubes = GameObject.FindGameObjectsWithTag("BgCube");

        foreach (GameObject cube in bgCubes)
        {
            GameObject.Destroy(cube);
        }

        GameObject[] bgVirtualCubes = GameObject.FindGameObjectsWithTag("BgVirtualCube");

        foreach (GameObject cube in bgVirtualCubes)
        {
            GameObject.Destroy(cube);
        }


        m_BgVirtualCubeDatas.Clear();
        m_BgCubeDatas.Clear();
    }

    // -------------------------
    // Create BgCubes (by col * row) 
    // odd number : white
    // even number : black
    // -------------------------
    private void CreateBgCube()
    {

        // setting
        float scale = GameMng.Instance.GetSize();     // scaleX, scaleY
        Vector2 referPos = GameMng.Instance.GetGameReferPos();  // refer pos
        Vector2Int xy = GameMng.Instance.GetGameScale(); //column & row

        float col = xy.y + 1; // 8 
        float row = xy.x; // 7 

        // first Cube 
        Vector3 startPos = new Vector3(referPos.x, referPos.y, 5);

        for (int r = 0; r < row; r++)
        {
            for (int c = 0; c < col; c++)
            {
                // caculate pos（go right +scale，go up +scale）
                Vector3 pos = new Vector3(
                    startPos.x + r * scale,
                    startPos.y + c * scale,
                    5
                );

                // Create Cube
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.name = $"Bg_{r}_{c}";
                cube.tag = "BgCube";
                cube.transform.position = pos;
                cube.transform.localScale = new Vector3(scale, scale, 1);

                // save data
                BgCubeData data = new BgCubeData()
                {
                    Row = r,
                    Col = c,
                    Position = pos,
                    Cube = cube
                };

                m_BgCubeDatas[new Vector2Int(r, c)] = data;

                // judge white or black
                // (row + col) % 2 == 0 → black
                // than white
                bool isBlack = ((r + c) % 2 == 0);

                // paste texture
                Renderer rd = cube.GetComponent<Renderer>();
                rd.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                rd.material.mainTexture = isBlack ? texBlack : texWhite;
                rd.material.mainTextureScale = new Vector2(1, 1);
            }
        }
    }

    private void CreateVirtualBgCube()
    {

        // setting
        float scale = m_BlockPosInfo.GetSize();     // scaleX, scaleY
        Vector2 referPos = m_BlockPosInfo.GetReferPos();  // refer pos
        Vector2Int xy = m_BlockPosInfo.GetScale(); //column & row


        float col = xy.y; // 8 
        float row = xy.x; // 7 

        // first Cube 
        Vector3 startPos = new Vector3(referPos.x, referPos.y, 5);

        for (int r = 0; r < row; r++)
        {
            for (int c = 0; c < col; c++)
            {
                // caculate pos（go right +scale，go up +scale）
                Vector3 pos = new Vector3(
                    startPos.x + r * scale,
                    startPos.y + c * scale,
                    5
                );

                // Create Cube
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.name = $"BgVirtual_{r}_{c}";
                cube.tag = "BgVirtualCube";
                cube.transform.position = pos;
                cube.transform.localScale = new Vector3(scale, scale, 1);

                // 2026/07/10 added by Fate
                // save data
                BgVirtualCubeData data = new BgVirtualCubeData()
                {
                    Row = r,
                    Col = c,
                    Position = pos,
                    Cube = cube
                };

                m_BgVirtualCubeDatas[new Vector2Int(r, c)] = data;
                // 2026/07/10 added by Fate
            }
        }
    }

    // -------------------------
    // Get bg position for sakura fly use
    // -------------------------
    public Vector3 GetBgVirtualCubePosition(int row, int col)
    {
        if (m_BgVirtualCubeDatas.TryGetValue(new Vector2Int(row, col), out BgVirtualCubeData data))
            return data.Position;

        return Vector3.zero;
    }

    // -------------------------
    // Get real bg position 
    // -------------------------
    public Vector3 GetBgCubePosition(int row, int col)
    {
        if (m_BgCubeDatas.TryGetValue(new Vector2Int(row, col), out BgCubeData data))
            return data.Position;

        return Vector3.zero;
    }


}
