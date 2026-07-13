//
// InGameUIButton.cs
// 
// 2026/05/31 Created By Fate Ku
// 2026/06/02 Updated By Fate Ku
// 2026/06/24 Updated By Fate Ku
// 2026/07/01 Updated By Fate Ku
// 2026/07/09 Updated By Fate Ku
// 2026/07/12 Updated By Fate Ku
// 2026/07/13 Updated By Fate Ku
//

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InGameUIButton
{
    private bool m_CanOperate = true;

    private Camera m_MainCam;
    private Transform[] m_Cubes = new Transform[7]; //create new 7 cubes

    private int m_CurrentCol = -1;
    private int m_CurrentRow = -1;

    //------------------------------------
    // Path Preview
    //------------------------------------

    private PathPreviewSystem m_PathPreview;

    // avoids rebuilding every frame
    private int m_LastRow = -1;
    private int m_LastCol = -1;

    //------------------------------------
    // Next Block Info
    //------------------------------------

    public BlockType m_NextBlockType;

    private List<FallDirection> m_NextPath =
        new List<FallDirection>();

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

        m_PathPreview = new PathPreviewSystem(m_BlockPosInfo);
    }

    // -------------------------
    // Update：Raycast for click/touch
    // -------------------------
    public void Update()
    {
        /*
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
        */

        //========================
        // Mouse
        //========================

        if (Mouse.current != null)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();

            // Press, Hold
            if (
                //Mouse.current.leftButton.wasPressedThisFrame ||
                Mouse.current.leftButton.isPressed &&
                m_CanOperate)
            {
                CheckPress(mousePos);
            }

            // Release , click
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                CheckRaycast(mousePos);
                EndPress();
            }
        }

        //========================
        // Touch
        //========================

        if (Touchscreen.current != null)
        {
            var touch = Touchscreen.current.primaryTouch;

            Vector2 touchPos = touch.position.ReadValue();

            // Press , Hold
            if (
                //touch.press.wasPressedThisFrame ||
                touch.press.isPressed &&
                m_CanOperate)
            {
                CheckPress(touchPos);
            }

            // Release , click
            if (touch.press.wasReleasedThisFrame)
            {
                CheckRaycast(touchPos);
                EndPress();
            }
        }

        if (m_PathPreview != null)
        {
            m_PathPreview.Update();
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

    private void CheckPress(Vector2 screenPos)
    {
        Vector2Int xy = m_BlockPosInfo.GetScale(); //column & row
        float col = xy.y; // 8 
        float row = xy.x; // 7

        m_CurrentCol = (int)col;

        Ray ray = m_MainCam.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (!hit.collider.name.StartsWith("ClickButton"))
                return;

            m_CurrentRow = int.Parse(
                hit.collider.name.Replace("ClickButton", ""));

            Debug.Log($"Press Row = {m_CurrentRow}");


            // preview path
            //------------------------------------
            // avoids rebuilding
            //------------------------------------

            if (m_CurrentRow == m_LastRow &&
                m_CurrentCol == m_LastCol)
            {
                return;
            }

            m_LastRow = m_CurrentRow;
            m_LastCol = m_CurrentCol;

            //------------------------------------
            // start position
            //------------------------------------

            Vector3 pos =
                GameMng.Instance.GetBgCubePosition(
                    m_CurrentRow,
                    m_CurrentCol);

            Debug.Log($"({m_CurrentRow},{m_CurrentCol}) -> {pos}");


            if (m_NextBlockType == BlockType.None)
            {
                return;
            }

            //------------------------------------
            // show Preview path
            //------------------------------------
           
            m_PathPreview.Show(
                m_CurrentRow,
                m_CurrentCol,
                m_NextBlockType,
                m_NextPath);

            Debug.Log(
                $"Preview ({m_CurrentRow},{m_CurrentCol}) {m_NextBlockType}");
        }
    }


    private void EndPress()
    {
        m_CurrentCol = -1;
        m_CurrentRow = -1;

        m_LastRow = -1;
        m_LastCol = -1;

        if (m_PathPreview != null)
        {
            m_PathPreview.Hide();
        }

        //m_NextPath.Clear();
    }

    public void SetNextBlockPath(BlockType type, List<FallDirection> path)
    {
        Debug.Log($"SetNextBlockPath Count = {path.Count}");

        m_NextBlockType = type;

        m_NextPath.Clear();

        if (path != null)
        {
            m_NextPath.AddRange(path);
        }

        string pathText = "";

        for (int i = 0; i < m_NextPath.Count; i++)
        {
            pathText += $"[{i}] {m_NextPath[i]}";

            if (i < m_NextPath.Count - 1)
                pathText += " -> ";
        }

        Debug.Log($"Type={type}, Path={pathText}");
    }

    public void SetCanOperate(bool canOperate)
    {
        m_CanOperate = canOperate;
    }

}
