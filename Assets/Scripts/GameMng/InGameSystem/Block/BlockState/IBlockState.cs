//
// IBlockState.cs
// 
// 2026/06/03 Created By Man-Yi, Yeh
// 2026/06/10 Updated By Man-Yi, Yeh
// 2026/06/11 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public enum BlockStateType
{
    None = -1,

    Idle,
    Rise,
    Fall,
    Combine,
    Destroy,
    Create,
}

public class IBlockState
{
    //StateType
    protected BlockStateType m_StateType = BlockStateType.None;
    public BlockStateType StateType
    {
        get { return m_StateType; }
        set { m_StateType = value; }
    }

    //StateName
    private string m_StateName = "IBlockState";
    public string StateName
    {
        set { m_StateName = value; }
    }

    //block
    protected IBlock m_Block;

    //Controller
    protected BlockStateController m_Controller = null;

    //trigger
    protected bool m_Trigger = false;
    public bool Trigger
    {
        set { m_Trigger = value; }
    }

    public IBlockState(IBlock block, BlockStateController controller)
    {
        m_Block = block;
        m_Controller = controller;
    }

    //begin
    public virtual void StateBegin() { }

    //end
    public virtual void StateEnd() { }

    //-------------------
    //update
    //-------------------
    //update
    public virtual void StateUpdate() { }

    //do combine check
    public virtual void DoCombineCheck(CombineSetsController controller) { }

    //be combined check
    public virtual void BeCombinedCheck(IBlock nearBlock, CombineSetsController controller) { }

    //near destroy
    public virtual void NearDestroy(IBlock destroyBlock) { }

    //be destroyed
    public virtual void BeDestroyed() { }


    public override string ToString()
    {
        return string.Format(
            "I_BlockState: StateName={0}",
            m_StateName);
    }
}
