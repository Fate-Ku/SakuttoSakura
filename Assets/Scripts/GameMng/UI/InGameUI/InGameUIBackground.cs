//
// InGameUIBackground.cs
// 
// 2026/06/06 Created By Fate Ku
//

using UnityEngine;

public class InGameUIBackground
{
    private Texture2D texWhite; // white board texture
    private Texture2D texBlack; // black board texture

    public void Init()
    {
        // Loading Picture（Resources/UI/InGame）
        texWhite = Resources.Load<Texture2D>("UI/InGame/GbW");
        texBlack = Resources.Load<Texture2D>("UI/InGame/GbB");

        if (texWhite == null || texBlack == null)
        {
            Debug.LogError("Load error！plz check route：Assets/Resources/UI/InGame/");
        }

        CreateBgCube();
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

        float col = xy.y; // 8 
        float row = xy.x; // 7 

        // first Cube 
        Vector3 startPos = new Vector3(referPos.x, referPos.y, 5);

        for (int r = 0; r < row; r++)
        {
            for (int c = 0; c < col; c++)
            {
                // caculate pos（go right +scale，go down -scale）
                Vector3 pos = new Vector3(
                    startPos.x + r * scale,
                    startPos.y - c * scale,
                    5
                );

                // Create Cube
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.name = $"Bg_{r}_{c}";
                cube.tag = "BgCube";
                cube.transform.position = pos;
                cube.transform.localScale = new Vector3(scale, scale, 1);

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



}
