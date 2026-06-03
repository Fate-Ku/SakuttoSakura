//
// SceneStateController.cs
// 
// 2026/06/03 Created By Man-Yi, Yeh
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
    }

    //block update
    public void BlockUpdate()
    {
        if (m_State == null)
        {
            return;
        }

        //state update
        m_State.StateUpdate();
    }

    //check is go fall
    public void GoFallCheck()
    {

    }

    //is falling
    public bool IsFalling()
    {
        return false;
    }

    //fall info
    //public FallInfo GetFallInfo()

    //check combine
    public void CombineCheck()
    {

    }

    //check is go destroy
    public void GoDestroyCheck()
    {

    }

    //near destroy
    public void NearDestroy()
    {

    }

    
}
