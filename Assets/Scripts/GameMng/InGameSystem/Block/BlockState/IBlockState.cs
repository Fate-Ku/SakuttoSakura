//
// IBlockState.cs
// 
// 2026/06/03 Created By Man-Yi, Yeh
// 

using UnityEngine;

public class IBlockState
{
    //block
    protected IBlock m_Block;

    //StateName
    private string m_StateName = "IBlockState";
    public string StateName
    {
        get { return m_StateName; }
        set { m_StateName = value; }
    }

    //Controller
    protected BlockStateController m_Controller = null;

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
    public virtual void BeCombinedCheck(IBlock block, CombineSetsController controller) { }

    //near destroy
    public virtual void NearDestroy() { }

    //be destroyed
    public virtual void BeDestroyed() { }


    public override string ToString()
    {
        return string.Format(
            "I_BlockState: StateName={0}",
            StateName);
    }
}
