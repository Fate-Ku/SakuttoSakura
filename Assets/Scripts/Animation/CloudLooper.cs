//
// CloudLooper.cs
// 
// 2026/07/09 Created By Fate Ku
//

using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CloudLooper : MonoBehaviour
{
    [Header("Move")]
    [SerializeField]private bool m_MoveRight = true;
    [SerializeField] private float m_BaseSpeed = 0.5f;
    [SerializeField]
    [Range(0f, 1f)] private float m_SpeedRandom = 0.2f;
    [SerializeField]
    [Range(-20f, 20f)]private float m_MoveAngle = 5f;
    [SerializeField]private float m_AngleRandom = 3f;

    [Header("Float")]
    [SerializeField]private float m_BaseFloatAmplitude = 0.15f;
    [SerializeField]private float m_AmplitudeRandom = 0.05f;
    [SerializeField]private float m_FloatSpeed = 0.4f;

    [Header("Respawn")]
    [SerializeField]private float m_ExtraOffset = 1f;
    [SerializeField]private Vector2 m_RandomYRange = new Vector2(-0.5f, 0.5f);
    [SerializeField]private Vector2 m_RespawnDelay = new Vector2(0f, 2f);


    private Camera m_Camera;
    private Renderer m_Renderer;

    private float m_LeftBound;
    private float m_RightBound;

    private float m_OriginY;
    private float m_BaseY;

    private float m_CurrentSpeed;
    private float m_CurrentAmplitude;
    private float m_CurrentFloatSpeed;

    private float m_CurrentAngle;
    private float m_FloatOffset;

    private Vector2 m_MoveDirection;

    private void Awake()
    {
        m_Camera = Camera.main;
        m_Renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        m_OriginY = transform.position.y;
        m_BaseY = m_OriginY;

        CacheCameraBounds();

        RandomizeCloud(true);
    }

    private void Update()
    {
        Move();
        CheckLoop();
    }

    private void CacheCameraBounds()
    {
        float height = m_Camera.orthographicSize;
        float width = height * m_Camera.aspect;

        m_LeftBound = m_Camera.transform.position.x - width;
        m_RightBound = m_Camera.transform.position.x + width;
    }

    private void Move()
    {
        Vector3 pos = transform.position;

        pos += (Vector3)(m_MoveDirection *
                         m_CurrentSpeed *
                         Time.deltaTime);

        pos.y =
            m_BaseY +
            Mathf.Sin(Time.time * m_CurrentFloatSpeed + m_FloatOffset)
            * m_CurrentAmplitude;

        transform.position = pos;
    }

    private void CheckLoop()
    {
        float halfWidth = m_Renderer.bounds.extents.x;

        Vector3 pos = transform.position;

        bool respawn = false;

        if (m_MoveRight)
        {
            if (pos.x - halfWidth > m_RightBound + m_ExtraOffset)
            {
                pos.x =
                    m_LeftBound -
                    halfWidth -
                    m_ExtraOffset -
                    Random.Range(m_RespawnDelay.x, m_RespawnDelay.y);

                respawn = true;
            }
        }
        else
        {
            if (pos.x + halfWidth < m_LeftBound - m_ExtraOffset)
            {
                pos.x =
                    m_RightBound +
                    halfWidth +
                    m_ExtraOffset +
                    Random.Range(m_RespawnDelay.x, m_RespawnDelay.y);

                respawn = true;
            }
        }

        if (respawn)
        {
            m_BaseY =
                m_OriginY +
                Random.Range(m_RandomYRange.x, m_RandomYRange.y);

            pos.y = m_BaseY;

            RandomizeCloud(false);
        }

        transform.position = pos;
    }

    private void RandomizeCloud(bool firstSpawn)
    {
        m_CurrentSpeed =
            m_BaseSpeed *
            Random.Range(1f - m_SpeedRandom,
                         1f + m_SpeedRandom);

        m_CurrentAmplitude =
            m_BaseFloatAmplitude +
            Random.Range(-m_AmplitudeRandom,
                          m_AmplitudeRandom);

        m_CurrentFloatSpeed =
            m_FloatSpeed *
            Random.Range(0.8f, 1.2f);

        m_CurrentAngle =
            m_MoveAngle +
            Random.Range(-m_AngleRandom,
                          m_AngleRandom);

        float angle =
            m_MoveRight ?
            m_CurrentAngle :
            180f - m_CurrentAngle;

        float rad = angle * Mathf.Deg2Rad;

        m_MoveDirection = new Vector2(
            Mathf.Cos(rad),
            Mathf.Sin(rad));

        m_FloatOffset =
            Random.Range(0f, Mathf.PI * 2f);

        if (firstSpawn)
        {
            transform.position +=
                Vector3.right *
                Random.Range(-3f, 3f);
        }
    }
}