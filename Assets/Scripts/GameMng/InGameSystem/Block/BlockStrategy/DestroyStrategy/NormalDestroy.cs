//
// NormalDestroy.cs
// 
// 2026/06/07 Created By Man-Yi, Yeh
//

using UnityEngine;


public class NormalDestroy : IDestroyStrategy
{
    private float m_DestroyTime = 0.5f;
    private float m_Timer;

    public override void DestroyStart(IBlock onerBlock)
    {
        Debug.Log("DestroyStart");
        m_Timer = m_DestroyTime;
    }

    public override void DestroyEnd(IBlock onerBlock)
    {
        Debug.Log("DestroyEnd");
        onerBlock.BlockDestroy();
    }

    public override void DestroyUpdate(IBlock onerBlock)
    {
        m_Timer -= Time.deltaTime;

        if (m_Timer <= 0)
        {
            //end destroy
            DestroyEnd(onerBlock);
        }
        else
        {
            //adjust size
            float rate = m_Timer / m_DestroyTime;
            if (rate > 0.4f)
            {
                rate = 0.4f;
            }
            if (rate < 0.2f)
            {
                rate = 0.2f;
            }

            Vector3 scale = onerBlock.BlockOb.transform.localScale;
            scale.x = rate;
            scale.y = rate;

            onerBlock.BlockOb.transform.localScale = scale;
        }
    }
}
