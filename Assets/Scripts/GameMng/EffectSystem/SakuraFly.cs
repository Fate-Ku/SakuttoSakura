//
// SakuraFly.cs
// 
// 2026/07/06 Created By Fate Ku
// 2026/08/24 Updated By Fate Ku
// 

using System;
using UnityEngine;

public class SakuraFly : MonoBehaviour
{
    private Action m_OnArrived;

    private Vector3 m_Start;
    private Vector3 m_Target;
    private Vector3 m_Control;

    private Transform m_SakuraImage;

    private float m_Timer;

    [SerializeField]
    private float m_Duration = 3f;

    private const float START_SCALE = 0.6f;
    private const float PEAK_SCALE = 0.75f;


    public void Init(Vector3 target, Action onArrived = null)
    {
        m_Start = transform.position;
        m_Target = target;

        m_OnArrived = onArrived;

        m_Timer = 0f;

        transform.localScale = new Vector3(
            START_SCALE,
            START_SCALE,
            1f);

        m_SakuraImage = transform.Find("Sakura_5");

        float height = 2.5f;

        // every flowers have different radians
        float randomOffset = UnityEngine.Random.Range(-1.2f, 1.2f);

        m_Control = (m_Start + m_Target) * 0.5f;
        m_Control.y += height;
        m_Control.x += randomOffset;
    }

    private void Update()
    {
        m_Timer += Time.deltaTime;

        float t = Mathf.Clamp01(m_Timer / m_Duration);

        // Ease In Out
        t = Mathf.SmoothStep(0f, 1f, t);

        // ===== Bezier =====
        Vector3 pos =
            Mathf.Pow(1f - t, 2f) * m_Start +
            2f * (1f - t) * t * m_Control +
            Mathf.Pow(t, 2f) * m_Target;

        transform.position = pos;

        // ===== Scale =====
        float scale;

        if (t < 0.15f)
        {
            scale = Mathf.Lerp(
                START_SCALE,
                PEAK_SCALE,
                t / 0.15f);
        }
        else
        {
            float p = (t - 0.15f) / 0.85f;

            scale = Mathf.Lerp(
                PEAK_SCALE,
                0f,
                p);
        }

        transform.localScale = new Vector3(
            scale,
            scale,
            1f);

        // ===== Rotate Sakura Only =====
        if (m_SakuraImage != null)
        {
            m_SakuraImage.Rotate(0f, 0f, 360f * Time.deltaTime, Space.Self);
        }

        if (m_Timer >= m_Duration)
        {
            m_OnArrived?.Invoke();

            Destroy(gameObject);
        }
    }
}