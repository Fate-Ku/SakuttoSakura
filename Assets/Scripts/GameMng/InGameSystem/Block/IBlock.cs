//
// IBlock.cs
// 
// 2026/05/26 Created By Man-Yi, Yeh
// 

using UnityEngine;

public abstract class IBlock
{
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
    protected void SetType(BlockType type)
    {
        m_Type = type;
    }

    //

    //is do combine check
    private bool m_IsDoCombineCheck = false;


    //startegys
    private IBlockStrategy m_UpdateStartegy;
    private IBlockStrategy m_CombineCheckStartegy;
    private IBlockStrategy m_NextDestroyStrategy;
    private IBlockStrategy m_DestroyStrategy;

    public IBlock() { }

    public virtual void Init() { }
    public virtual void Term() { }
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
            m_CombineCheckStartegy.Do(this);
        }
    }

    public void Destroy()
    {
    }


}
