//
// InGameUIButton.cs
// 
// 2026/05/31 Created By Fate Ku
// 2026/06/02 Updated By Fate Ku
//
using UnityEngine;
public class InGameUIButton
{
    private Camera m_MainCam;
    private Transform[] m_Cubes = new Transform[7]; //create new 7 cubes

    public void Init()
    {
        // main camera
        m_MainCam = Camera.main;

        CreateCubes();
    }

    // -------------------------
    // Update：Raycast for click/touch
    // -------------------------
    public void Update()
    {
        // mouse click
        if (Input.GetMouseButtonDown(0))
        {
            CheckRaycast(Input.mousePosition);
        }

        // touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                CheckRaycast(touch.position);
            }
        }
    }

    // -------------------------
    // delete Cubes
    // -------------------------
    public void Term()
    {
        for (int i = 0; i < m_Cubes.Length; i++)
        {
            if (m_Cubes[i] != null)
                GameObject.Destroy(m_Cubes[i].gameObject);
        }
    }

    // -------------------------
    // Create 7 Cubes
    // -------------------------
    private void CreateCubes()
    {
        // setting
        float scale = GameMng.Instance.GetSize();     // scaleX, scaleY
        Vector2 referPos = GameMng.Instance.GetGameReferPos();  // refer pos
        Vector2Int xy = GameMng.Instance.GetGameScale(); //column & row

        float col = xy.y; // 8
        float row = xy.x; // 7

        float scaleX = scale;
        float scaleY = scale * col;   // scaleY * col

        float offsetY = scale * 0.5f; //top

        float startPosY = referPos.y + offsetY - (offsetY * col/2); // middle

        // first Cube
        Vector3 pos = new Vector3(referPos.x, startPosY, 0);

        for (int i = 0; i < row; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.name = "ClickButton" + i;

            // set pos
            cube.transform.position = pos;

            // set size
            cube.transform.localScale = new Vector3(scaleX, scaleY, 1);

            // cannot see cube
            cube.GetComponent<MeshRenderer>().enabled = false;

            // record
            m_Cubes[i] = cube.transform;

            // next Cube pos（+Cube width）
            pos.x += scaleX;
        }
    }

    // -------------------------
    // Check click/touch
    // -------------------------
    private void CheckRaycast(Vector2 screenPos)
    {
        Ray ray = m_MainCam.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            for (int i = 0; i < 7; i++)
            {
                if (hit.collider != null && hit.collider.name == "ClickButton" + i)
                {
                    Debug.Log("Click id：" + i);

                    // rid return to GameMng
                    GameMng.Instance.InGameColumnOnClick(i);

                    return;
                }
            }
        }
    }

}
