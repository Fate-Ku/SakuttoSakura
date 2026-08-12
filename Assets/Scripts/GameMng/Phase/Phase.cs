//
// Phase.cs
// 
// 2026/05/21 Update By Man-Yi, Yeh 
//

using UnityEngine;

public class Phase : IGameSystem
{
    protected bool m_IsTGS;

    public Phase(GameMng gameMng, bool isTGS) 
        : base(gameMng)
    {
        m_IsTGS = isTGS;
    }
}
