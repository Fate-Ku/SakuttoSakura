//
// IFallStrategy.cs
// 
// 2026/06/18 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class IFallStrategy
{
    protected FallDirection m_Direction;
    protected float m_Speed;

    protected Vector2 m_TargetPos;

    protected bool m_IsEndFall;
    public bool IsEndFall
    {
        get { return m_IsEndFall; }
    }

    public IFallStrategy(FallDirection direction, float speed)
    {
        m_Direction = direction;
        m_Speed = speed;
    }

    public virtual bool CanFall(IBlock block)
    {
        return false;
    }

    public void StartFall(IBlock block) 
    {
        m_IsEndFall = false;
        SetTargetPos(block);
    }

    public virtual void UpdateFall(IBlock block) { }
    protected virtual void SetTargetPos(IBlock block) { }
}
