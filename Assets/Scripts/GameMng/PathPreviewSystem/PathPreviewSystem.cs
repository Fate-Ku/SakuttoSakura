//
// PathPreviewSystem.cs
//
// 2026/07/12 Created By Fate Ku
// 2026/07/16 Updated By Fate Ku
// 2026/07/23 Updated By Fate Ku
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

    private float m_MoveSpeed = 6f;

    private readonly Dictionary<BlockType, Material> m_Materials;

    private readonly List<GameObject> m_DirectionObjects = new();

    private readonly List<FallDirection> m_PathDirections = new();

    private const float PREVIEW_FLOWER_SCALE = 0.5f;
    private const float DIRECTION_SCALE = 0.357f;

    //--------------------------------
    // Constructor
    //--------------------------------

    public PathPreviewSystem(
        BlockPosInfo info,
        Dictionary<BlockType, Material> materials)
    {
        m_BlockInfo = info;
        m_Materials = materials;
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
        Debug.Log($"[Preview] Show() is called，type = {type}");
        //Debug.Log($"Path Count = {path.Count}");

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

        //for (int i = 0; i < m_PathPoints.Count; i++)
        //{
        //    Debug.Log($"Point {i} = {m_PathPoints[i]}");
        //}


        if (m_PathPoints.Count == 0)
        {
            return;
        }

        CreatePreviewBlock(prefab, type);

        CreateDirectionObjects(type);


        //StartPreview();

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
        m_PathDirections.Clear();

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

                dir = FallDirection.Down;

                nextRow = row;
                nextCol = col - 1;
            }

            // bottom
            if (nextCol < 0)
                break;

            row = nextRow;
            col = nextCol;

            // record real path
            m_PathDirections.Add(dir);

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

        //Debug.Log($"Row={row} Col={col} Pos={pos}");
        m_PathPoints.Add(pos);
    }


    //--------------------------------
    // create preview object
    //--------------------------------

    private void CreatePreviewBlock(GameObject prefab, BlockType type)
    {
        if (m_PreviewBlock != null)
            GameObject.Destroy(m_PreviewBlock);

        m_PreviewBlock = GameObject.Instantiate(prefab);
        m_PreviewBlock.name = "Preview";

        m_PreviewBlock.transform.localScale =
            Vector3.one * PREVIEW_FLOWER_SCALE;

        // set start pos
        Vector3 p = m_PathPoints[0];
        p.z = -5f;
        m_PreviewBlock.transform.position = p;

        // translucent
        SpriteRenderer sr = m_PreviewBlock.GetComponentInChildren<SpriteRenderer>();

        if (sr != null)
        {
            if (m_Materials.TryGetValue(type, out Material mat))
            {
                sr.material = mat;
            }

            Color c = sr.color;
            c.a = 0.8f;
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

    private void CreateDirectionObjects(BlockType type)
    {
        if (m_PathDirections == null || m_PathDirections.Count == 0)
            return;

        // delete old
        foreach (GameObject obj in m_DirectionObjects)
        {
            if (obj != null)
                GameObject.Destroy(obj);
        }

        m_DirectionObjects.Clear();

        for (int i = 0; i < m_PathPoints.Count - 1; i++)
        {
            FallDirection current = m_PathDirections[i];

            FallDirection? next = null;

            if (i + 1 < m_PathDirections.Count)
                next = m_PathDirections[i + 1];

            GameObject prefab = GetDirectionPrefab(current, next);

            if (prefab == null) { continue; }

            GameObject obj = GameObject.Instantiate(prefab);

            // start from second path point
            Vector3 pos = m_PathPoints[i + 1];
            pos.z = -4.5f;

            obj.transform.position = pos;
            obj.transform.localScale = Vector3.one * DIRECTION_SCALE;

            // 2026/07/23 Updated By Fate Ku
            SpriteRenderer sr = obj.GetComponentInChildren<SpriteRenderer>();
           
            if (sr != null)
            {
                if (m_Materials.TryGetValue(type, out Material mat))
                {
                    sr.material = mat;
                }

                Color c = sr.color;
                c.a = 0.8f;
                sr.color = c;
            }
            // 2026/07/23 Updated By Fate Ku

            m_DirectionObjects.Add(obj);

        }
    }

    private GameObject GetDirectionPrefab(FallDirection current, FallDirection? next)
    {
        switch (current)
        {
            case FallDirection.Down:

                if (next == FallDirection.Left)
                    return m_BlockInfo.DownLeft;

                if (next == FallDirection.Right)
                    return m_BlockInfo.DownRight;

                return m_BlockInfo.Down;

            case FallDirection.Left:

                if (next == FallDirection.Down)
                    return m_BlockInfo.LeftDown;

                return m_BlockInfo.Left;

            case FallDirection.Right:

                if (next == FallDirection.Down)
                    return m_BlockInfo.RightDown;

                return m_BlockInfo.Right;
        }

        return null;
    }

    //--------------------------------
    // hide preview
    //--------------------------------

    public void Hide()
    {
        m_Playing = false;
        m_TargetIndex = 0;
        m_PathPoints.Clear();
        m_PathDirections.Clear();

        if (m_PreviewBlock != null)
            m_PreviewBlock.SetActive(false);

        foreach (GameObject obj in m_DirectionObjects)
        {
            if (obj != null)
                GameObject.Destroy(obj);
        }

        m_DirectionObjects.Clear();

    }
}
