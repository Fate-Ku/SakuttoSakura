//
// IGameSystem.cs
// 
// 2026/05/21 Update By Man-Yi, Yeh 
// 2026/05/26 Update By Man-Yi, Yeh 
// 

using UnityEngine;

public abstract class IGameSystem
{
    //GameMng
    protected GameMng m_GameMng = null;

    public IGameSystem(GameMng gameMng)
    {
        m_GameMng = gameMng;
    }

    public virtual void Init() { }
    public virtual void Term() { }
    public virtual void Update() { }
}
