//
// SceneStateController.cs
// 
// 2026/06/03 Created By Man-Yi, Yeh
// 2026/06/06 Updated By Man-Yi, Yeh
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

    //-------------------
    //update
    //-------------------
    //block update
    public void BlockUpdate()
    {
        //state update
        m_State?.StateUpdate();
    }

    //check combine
    public void CombineCheck(CombineSetsController controller)
    {
        //state combine check
        m_State?.StateCombineCheck(controller);
    }

    //check is go destroy
    public void DestroyCheck()
    {

    }

    //near destroy
    public void NearDestroy()
    {

    }

    //-------------------
    //change state
    //-------------------
    //go combine state
    public void GoCombine()
    {
        if (m_State.StateName != "CombineState")
        {

        }
    }

    //go destroy state
    public void GoDestroy()
    {
        if (m_State.StateName != "DestroyState")
        {

        }
    }

}
