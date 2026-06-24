//
// BlockTest.cs
// 
// 2026/06/23 Created By Man-Yi, Yeh
// 2026/06/24 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class BlockTest : MonoBehaviour
{
    [Header("Info")]
    public Vector2Int id;
    public string state;

    [Header("Fall")]
    public string controllerName;
    public FallDirection direction;
    public float fallTargetX;
    public float fallTargetY;

    [Header("Rise")]
    public float riseTargetY;
    
}
