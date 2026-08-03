//
// LoadingBG.cs
// 
// 2026/08/03 Created By Man-Yi, Yeh
//

using UnityEngine;
using UnityEngine.UI;

public class LoadingBG : MonoBehaviour
{
    private Canvas m_Canvas;

    void Start()
    {
        m_Canvas = GetComponent<Canvas>();
        SetActive(false);
    }

    public void SetActive(bool active)
    {
        if (m_Canvas != null)
        {
            m_Canvas.enabled = active;
        }
    }

}
