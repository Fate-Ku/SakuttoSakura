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
        Vector2Int scale = GameMng.Instance.GetGameScale();     // scaleX, scaleY
        Vector2 referPos = GameMng.Instance.GetGameReferPos();  // refer pos

        float scaleX = scale.x;
        float scaleY = scale.y * 8f;   // scaleY * 8

        // first Cube
        Vector3 pos = new Vector3(referPos.x, referPos.y, 0);

        for (int i = 0; i < 7; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.name = "ClickButton" + i;

            // set pos
            cube.transform.position = pos;

            // set size
            cube.transform.localScale = new Vector3(scaleX, scaleY, 1);

            // cannot see cube
            //cube.GetComponent<MeshRenderer>().enabled = false;

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
                    GameMng.Instance.InGameClickColumn(i);

                    return;
                }
            }
        }
    }

}
