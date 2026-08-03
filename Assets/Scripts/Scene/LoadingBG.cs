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
    private Animator[] m_Animators;

    void Start()
    {
        m_Canvas = GetComponent<Canvas>();
        m_Animators = GetComponentsInChildren<Animator>();
        SetActive(false);
    }

    public void SetActive(bool active)
    {
        if (m_Canvas != null)
        {
            m_Canvas.enabled = active;
        }

        if (m_Animators != null)
        {
            foreach (Animator animator in m_Animators)
            {
                animator.enabled = active;
            }
        }
    }

}
