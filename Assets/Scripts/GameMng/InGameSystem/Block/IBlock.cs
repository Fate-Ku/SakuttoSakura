//
// IBlock.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
// 

using UnityEngine;

public abstract class IBlock
{
    //-------------------
    //game object
    //-------------------
    private GameObject m_Block;


    //-------------------
    //oner
    //-------------------
    //oner frame
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

    //startegys
    //private IBlockStrategy m_UpdateStartegy;
    //private IBlockStrategy m_CombineCheckStartegy;
    //private IBlockStrategy m_NextDestroyStrategy;
    //private IBlockStrategy m_DestroyStrategy;

    public IBlock(GameObject block) 
    {
        m_Block = Object.Instantiate(block);
    }

    public void Test(bool active)
    {
        m_Block.SetActive(active);
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
