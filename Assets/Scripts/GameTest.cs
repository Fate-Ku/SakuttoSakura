//
// GameTest.cs
// 
// 2026/06/24 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class GameTest : MonoBehaviour
{
    [Range(0, 3)] public int inGamePatternID;

    private void Awake()
    {
        //don't destroy
        DontDestroyOnLoad(gameObject);
    }


}
