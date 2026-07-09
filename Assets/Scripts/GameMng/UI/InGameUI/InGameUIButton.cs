//
// InGameUIButton.cs
// 
// 2026/05/31 Created By Fate Ku
// 2026/06/02 Updated By Fate Ku
// 2026/06/24 Updated By Fate Ku
// 2026/07/01 Updated By Fate Ku
// 2026/07/09 Updated By Fate Ku
//

using UnityEngine;
using UnityEngine.InputSystem;


public class InGameUIButton
{

    private Camera m_MainCam;
    private Transform[] m_Cubes = new Transform[7]; //create new 7 cubes

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

        // main camera
        m_MainCam = Camera.main;

        CreateCubes();
    }

    // -------------------------
    // Update：Raycast for click/touch
    // -------------------------
    public void Update()
    {

        //// mouse click
        //if (Input.GetMouseButtonDown(0))
        //{
        //    CheckRaycast(Input.mousePosition);
        //}

        //// touch
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);

        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        CheckRaycast(touch.position);
        //    }
        //}

        // Mouse left button release
        if (Mouse.current != null && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            CheckRaycast(mousePos);
        }

        // Touch release
        if (Touchscreen.current != null)
        {
            var touch = Touchscreen.current.primaryTouch;

            if (touch.press.wasReleasedThisFrame)
            {
                Vector2 touchPos = touch.position.ReadValue();
                CheckRaycast(touchPos);
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
        float scale = m_BlockPosInfo.GetSize();     // scaleX, scaleY
        Vector2 referPos = m_BlockPosInfo.GetReferPos();  // refer pos
        Vector2Int xy = m_BlockPosInfo.GetScale(); //column & row

        float col = xy.y; // 8
        float row = xy.x; // 7

        float scaleX = scale;
        float scaleY = scale * col;   // scaleY * col

        float offsetY = scale * 0.5f; //top

        //float startPosY = referPos.y + offsetY - (offsetY * col); // middle
        float startPosY = referPos.y - offsetY + (offsetY * col); // middle

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
    public void CheckRaycast(Vector2 screenPos)
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
