//
// MoveUpFadeOut.cs
// 
// 2026/06/25 Created By Fate Ku
//

using UnityEngine;
using TMPro;

public class MoveUpFadeOut : MonoBehaviour
{
    private TextMeshPro text;
    private float time = 0f;
    private float duration = 1.5f;
    private Vector3 startPos;

    void Start()
    {
        text = GetComponent<TextMeshPro>();
        startPos = transform.position;
    }

    void Update()
    {
        time += Time.deltaTime;
        float t = time / duration;

        // rise
        transform.position = startPos + new Vector3(0, t * 1.5f, 0);

        // fade out
        Color c = text.color;
        c.a = 1f - t;
        text.color = c;

        // finish and delete
        if (time >= duration)
        {
            Destroy(gameObject);
        }
    }
}
