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

    private readonly List<GameObject> m_PreviewBlocks = new();

    private readonly List<Vector3> m_PathPoints = new();


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
 
    }


    //--------------------------------
    // Public：show preview
    //--------------------------------

    public void Show(int startRow, int startCol, BlockType type, List<FallDirection> path)
    {
        Debug.Log($"[Preview] Show() 被呼叫，type = {type}");
        Debug.Log($"Path Count = {path.Count}");

        ClearPreview();

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

        CreatePreviewBlocks(prefab);

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
    private void CreatePreviewBlocks(GameObject prefab)
    {
        foreach (Vector3 pos in m_PathPoints)
        {
            GameObject obj = GameObject.Instantiate(prefab);

            obj.transform.position = pos;
            obj.transform.localScale = Vector3.one;

            SpriteRenderer sr =
                obj.GetComponentInChildren<SpriteRenderer>();

            if (sr != null)
            {
                Color c = sr.color;
                c.a = 0.6f;
                sr.color = c;
            }

            m_PreviewBlocks.Add(obj);
        }
    }


    //--------------------------------
    // hide preview
    //--------------------------------
    public void Hide()
    {
        m_PathPoints.Clear();

        ClearPreview();
    }

    private void ClearPreview()
    {
        foreach (GameObject obj in m_PreviewBlocks)
        {
            if (obj != null)
                GameObject.Destroy(obj);
        }

        m_PreviewBlocks.Clear();
    }

}
