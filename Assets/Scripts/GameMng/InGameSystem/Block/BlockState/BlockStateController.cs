//
// SceneStateController.cs
// 
// 2026/06/03 Created By Man-Yi, Yeh
// 2026/06/06 Updated By Man-Yi, Yeh
// 2026/06/07 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class BlockStateController
{
    private IBlockState m_State;

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

    //get state name
    public string GetStateName()
    {
        return m_State.ToString();
    }

    //-------------------
    //update
    //-------------------
    //block update
    public void BlockUpdate()
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
    public void NearDestroy()
    {

    }


    //be destroyed
    public void BeDestroyed()
    {

    }

}
