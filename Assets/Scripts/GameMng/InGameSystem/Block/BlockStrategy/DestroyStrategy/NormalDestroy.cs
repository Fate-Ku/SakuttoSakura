//
// NormalDestroy.cs
// 
// 2026/06/07 Created By Man-Yi, Yeh
//

using UnityEngine;


public class NormalDestroy : IDestroyStrategy
{
    private float m_DestroyTime = 1;
    private float m_Timer;

    public override void DestroyStart(IBlock block)
    {
        Debug.Log("DestroyStart");
        m_Timer = m_DestroyTime;
    }

    public override void DestroyEnd(IBlock block)
    {
        Debug.Log("DestroyEnd");
        block.BlockDestroy();
    }

    public override void DestroyUpdate(IBlock block)
    {
        m_Timer -= Time.deltaTime;

        if (m_Timer <= 0)
        {
            //end destroy
            block.DestroyStrategy.DestroyEnd(block);
        }
        else
        {
            //adjust size
            float rate = m_Timer / m_DestroyTime;
            if (rate < 0.3f)
            {
                rate = 0.3f;
            }

            Vector3 scale = block.BlockOb.transform.localScale;
            scale.x *= rate;
            scale.y *= rate;

            block.BlockOb.transform.localScale = scale;
        }
    }
}
