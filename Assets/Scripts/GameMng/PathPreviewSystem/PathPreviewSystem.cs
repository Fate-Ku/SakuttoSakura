//
// PathPreviewSystem.cs
//
// 2026/07/12 Created By Fate Ku
//

using System.Collections.Generic;
using UnityEngine;

public class PathPreviewSystem
{
    //--------------------------------
    // Members
    //--------------------------------

    private readonly BlockPosInfo m_BlockInfo;

    private GameObject m_PreviewBlock;

    private readonly List<Vector3> m_PathPoints = new();

    private int m_TargetIndex = 0;

    private bool m_Playing = false;

    private float m_MoveSpeed = 4f;


    //--------------------------------
    // Constructor
    //--------------------------------

    public PathPreviewSystem(BlockPosInfo info)
    {
        m_BlockInfo = info;
    }


    //--------------------------------
    // Update
    //--------------------------------

    public void Update()
    {
        if (!m_Playing || m_PreviewBlock == null)
            return;

        if (m_PathPoints.Count <= 1)
            return;

        Transform tr = m_PreviewBlock.transform;
        Vector3 target = m_PathPoints[m_TargetIndex];

        tr.position = Vector3.MoveTowards(
            tr.position,
            target,
            m_MoveSpeed * Time.deltaTime);

        // reach node
        if (Vector3.Distance(tr.position, target) < 0.001f)
        {
            tr.position = target;
            m_TargetIndex++;

            // Finished playing → return to start position
            if (m_TargetIndex >= m_PathPoints.Count)
            {
                m_TargetIndex = 1;
                tr.position = m_PathPoints[0];
            }
        }
    }


    //--------------------------------
    // Public：show preview
    //--------------------------------

    public void Show(int startRow, int startCol, BlockType type, List<FallDirection> path)
    {
        Debug.Log($"[Preview] Show() 被呼叫，type = {type}");
        Debug.Log($"Path Count = {path.Count}");

        m_Playing = false;
        m_PathPoints.Clear();

        if (type == BlockType.None)
        {
            return;
        }

        if (path == null)
        {
            return;
        }

        GameObject prefab = m_BlockInfo.GetBlock(type);

        if (prefab == null)
        {
            return;
        }


        BuildPathPoints(startRow, startCol, path);

        for (int i = 0; i < m_PathPoints.Count; i++)
        {
            Debug.Log($"Point {i} = {m_PathPoints[i]}");
        }


        if (m_PathPoints.Count == 0)
        {
            return;
        }

        CreatePreviewBlock(prefab);


        StartPreview();

    }



    //--------------------------------
    // create path
    //--------------------------------

    private void BuildPathPoints(
        int startRow,
        int startCol,
        List<FallDirection> path)
    {
        m_PathPoints.Clear();

        Vector2Int boardSize = m_BlockInfo.GetScale();

        int row = startRow;
        int col = startCol;

        AddPoint(row, col);

        if (path == null || path.Count == 0)
            return;

        int patternIndex = 0;
        bool onlyDown = false;

        while (true)
        {
            FallDirection dir;

            if (onlyDown)
            {
                dir = FallDirection.Down;
            }
            else
            {
                dir = path[patternIndex];
                patternIndex++;

                if (patternIndex >= path.Count)
                    patternIndex = 0;
            }

            int nextRow = row;
            int nextCol = col;

            switch (dir)
            {
                case FallDirection.Down:
                    nextCol--;
                    break;

                case FallDirection.Left:
                    nextRow--;
                    break;

                case FallDirection.Right:
                    nextRow++;
                    break;
            }

            // wall
            if (nextRow < 0 || nextRow >= boardSize.x)
            {
                onlyDown = true;

                nextRow = row;
                nextCol = col - 1;
            }

            // bottom
            if (nextCol < 0)
                break;

            row = nextRow;
            col = nextCol;

            AddPoint(row, col);
        }
    }


    //--------------------------------
    // add path point
    //--------------------------------

    private void AddPoint(int row, int col)
    {
        Vector3 pos = GameMng.Instance.GetBgCubePosition(row, col);
        pos.z = -5f;

        Debug.Log($"Row={row} Col={col} Pos={pos}");
        m_PathPoints.Add(pos);
    }


    //--------------------------------
    // create preview object
    //--------------------------------

    private void CreatePreviewBlock(GameObject prefab)
    {
        if (m_PreviewBlock != null)
            GameObject.Destroy(m_PreviewBlock);

        m_PreviewBlock = GameObject.Instantiate(prefab);
        m_PreviewBlock.name = "Preview";

        m_PreviewBlock.transform.localScale =
            new Vector3(1f, 1f, 1f);

        // set start pos
        Vector3 p = m_PathPoints[0];
        p.z = -5f;
        m_PreviewBlock.transform.position = p;

        // translucent
        SpriteRenderer sr = m_PreviewBlock.GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            Color c = sr.color;
            c.a = 0.6f;
            sr.color = c;
        }
    }


    //--------------------------------
    // start to play animation
    //--------------------------------

    private void StartPreview()
    {
        if (m_PathPoints.Count <= 1)
        {
            m_Playing = false;
            return;
        }

        m_TargetIndex = 1;
        m_Playing = true;
    }


    //--------------------------------
    // hide preview
    //--------------------------------

    public void Hide()
    {
        m_Playing = false;
        m_TargetIndex = 0;
        m_PathPoints.Clear();

        if (m_PreviewBlock != null)
            m_PreviewBlock.SetActive(false);
    }
}
