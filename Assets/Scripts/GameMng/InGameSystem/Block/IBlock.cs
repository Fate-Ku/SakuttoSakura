//
// IBlock.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
// 2026/06/02 Updated By Man-Yi, Yeh
// 

using Unity.VisualScripting;
using UnityEngine;

public abstract class IBlock
{
    //-------------------
    //game object
    //-------------------
    private GameObject m_BlockOb;


    //-------------------
    //oner
    //-------------------
    private BlockNode m_OnerNode;
    public BlockNode OnerNode
    {
        set { m_OnerNode = value; }
    }

    //oner combine set

    //-------------------
    //info
    //-------------------
    //type
    private BlockType m_Type;
    public BlockType Type
    {
        get { return m_Type; }
        set { m_Type = value; }
    }

    //is idle
    private bool m_IsIdle = false;
    public bool IsIdle
    {
        get { return m_IsIdle; }
        set {  m_IsIdle = value; }
    }

    //pos
    private Vector2 m_Pos;

    //startegys
    //private IBlockStrategy m_UpdateStartegy;
    //private IBlockStrategy m_CombineCheckStartegy;
    //private IBlockStrategy m_NextDestroyStrategy;
    //private IBlockStrategy m_DestroyStrategy;

    public IBlock(GameObject block, float size) 
    {
        m_BlockOb = Object.Instantiate(block);
        m_BlockOb.transform.localScale = new Vector3(size, size, 1);
    }
    ~IBlock()
    {
        TestDestroy();
    }


    public void SetPos(Vector2 pos)
    {
        m_Pos = pos;
        if (m_BlockOb != null) 
        {
            m_BlockOb.transform.localPosition =
                new Vector3(
                    pos.x,
                    pos.y,
                    m_BlockOb.transform.localPosition.z);
        }
    }

    public void Test(bool active)
    {
        m_BlockOb.SetActive(active);
    }

    public void TestDestroy()
    {
        Object.Destroy(m_BlockOb);
    }

    /*
    public void Update()
    {
        if (m_UpdateStartegy != null)
        {
            m_UpdateStartegy.Do(this);
        }
    }

    public void CombineCheck()
    {
        if (m_CombineCheckStartegy != null)
        {
            if (IsIdle)
            {
                m_CombineCheckStartegy.Do(this);
            }
        }
    }

    public void NextDestroy()
    {
        if (m_NextDestroyStrategy != null)
        {
            if (IsIdle)
            {
                m_NextDestroyStrategy.Do(this);
            }
        }
    }

    public void Destroy()
    {
        if (m_DestroyStrategy != null)
        {
            m_DestroyStrategy.Do(this);
        }
    }

    */
}
