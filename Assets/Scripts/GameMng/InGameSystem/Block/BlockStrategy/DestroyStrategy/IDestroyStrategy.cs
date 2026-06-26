//
// IDestroyStrategy.cs
// 
// 2026/06/06 Created By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
// 2026/06/08 Updated By Man-Yi, Yeh
// 2026/06/11 Updated By Man-Yi, Yeh
//


using UnityEngine;

public abstract class IDestroyStrategy
{
    private float m_DestroyTime = 0.5f;
    private float m_Timer;

    //block that be created after destory
    protected IBlock m_CreateBlock;

    public abstract void DestroyStart(IBlock onerBlock);
    public abstract void DestroyEnd(IBlock onerBlock);
    public abstract void DestroyUpdate(IBlock onerBlock);

    public void SetCreateBlock(IBlock block)
    {
        m_CreateBlock = block;
        block?.SetActive(false);
    }

    protected void BlockDestory(IBlock onerBlock)
    {
        //record block destroy
        GameMng.Instance.RecordBlockDestroy(onerBlock.Type);

        CreateBlockAfterDestroy(onerBlock);
        onerBlock.BlockDestroy();
    }

    private void CreateBlockAfterDestroy(IBlock onerBlock)
    {
        if (m_CreateBlock != null)
        {
            BlockNode blockNode = onerBlock.BlockNode;
            blockNode.RemoveBlock();
            blockNode.SetBlock(m_CreateBlock);

            m_CreateBlock.SetActive(true);
            m_CreateBlock.SetPos(blockNode.Pos);
            m_CreateBlock.GoState(BlockStateType.Create);
        }
    }


    //-------------------
    //test
    //-------------------
    protected void TestTimeInit()
    {
        m_Timer = m_DestroyTime;
    }

    protected void TestUpdate(IBlock onerBlock)
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
            /*
            float rate = m_Timer / m_DestroyTime;
            if (rate > 0.3f)
            {
                rate = 0.3f;
            }
            if (rate < 0.1f)
            {
                rate = 0.1f;
            }

            Vector3 scale = onerBlock.BlockOb.transform.localScale;
            scale.x = rate;
            scale.y = rate;

            onerBlock.BlockOb.transform.localScale = scale;
            */
        }
    }
}


