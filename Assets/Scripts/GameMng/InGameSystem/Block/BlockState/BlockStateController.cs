//
// SceneStateController.cs
// 
// 2026/06/03 Created By Man-Yi, Yeh
// 2026/06/06 Updated By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
// 2026/06/10 Updated By Man-Yi, Yeh
// 2026/06/11 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class BlockStateController
{
    private IBlockState m_State;
    //test
    public IBlockState State
    {
        get { return m_State; }
    }

    public BlockStateController() { }

    //set state
    public void SetState(IBlockState state)
    {
        Debug.Log("Set Block State:" + state.ToString());

        //end previous state
        m_State?.StateEnd();

        //setting
        m_State = state;

        //begin new state
        m_State?.StateBegin();
    }

    //get state type
    public BlockStateType GetStateType()
    {
        return m_State.StateType;
    }

    //-------------------
    //update
    //-------------------
    //block update
    public void StateUpdate()
    {
        //state update
        m_State?.StateUpdate();
    }

    //do combine check
    public void DoCombineCheck(CombineSetsController controller)
    {
        //state do combine check
        m_State?.DoCombineCheck(controller);
    }

    //be combined check
    public void BeCombinedCheck(IBlock block,CombineSetsController controller)
    {
        //state be combined check
        m_State?.BeCombinedCheck(block, controller);
    }

    //near destroy
    public void NearDestroy(IBlock destroyBlock)
    {
        //state near destroy
        m_State.NearDestroy(destroyBlock);
    }


    //be destroyed
    public void BeDestroyed()
    {
        //state be destroyed
        m_State.BeDestroyed();
    }

}
