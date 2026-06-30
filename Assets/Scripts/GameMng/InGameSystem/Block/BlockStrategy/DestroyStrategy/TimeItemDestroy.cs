//
// TimeItemDestroy.cs
// 
// 2026/06/1 Created By Man-Yi, Yeh
//

using UnityEngine;

public class TimeItemDestroy : IDestroyStrategy
{
    private float m_AddTime;
    public TimeItemDestroy(float addTime = 5)
    {
        m_AddTime = addTime;
    }

    public override void DestroyStart(IBlock onerBlock)
    {
        Debug.Log("DestroyStart");
        GameMng.Instance.AddGameTime(m_AddTime);
    }

    public override void DestroyEnd(IBlock onerBlock)
    {
        Debug.Log("DestroyEnd");
        BlockDestory(onerBlock);
    }
}
