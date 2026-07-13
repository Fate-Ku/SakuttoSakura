//
// GameInfo.cs
// 
// 2026/07/13 Created By Man-Yi, Yeh
//

using UnityEngine;


public class TutorialTest : MonoBehaviour
{
    private float size;
    private float x;

    private void Start()
    {
        size = gameObject.transform.localScale.x;
        x = gameObject.transform.localPosition.x;
    }

    public void SetCol(int col)
    {
        Vector3 pos = new(
            col * size + x,
            gameObject.transform.localPosition.y,
            gameObject.transform.localPosition.z);
        gameObject.transform.localPosition = pos;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
