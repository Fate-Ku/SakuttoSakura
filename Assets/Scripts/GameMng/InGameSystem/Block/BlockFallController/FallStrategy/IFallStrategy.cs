//
// IFallStrategy.cs
// 
// 2026/06/18 Created By Man-Yi, Yeh
// 2026/06/22 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class IFallStrategy
{
    protected FallDirection m_Direction;
    public FallDirection Direction
    {
        get { return m_Direction; }
    }
    protected float m_Speed;
    public float Speed
    {
        get { return m_Speed; }
    }

    protected Vector2 m_TargetPos;

    protected bool m_GoNextFall = false;
    public bool GoNextFall
    {
        get { return m_GoNextFall; }
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
        m_GoNextFall = false;
        SetTargetPos(block);
    }

    public virtual void UpdateFall(IBlock block) { }
    protected virtual void SetTargetPos(IBlock block) { }
}
