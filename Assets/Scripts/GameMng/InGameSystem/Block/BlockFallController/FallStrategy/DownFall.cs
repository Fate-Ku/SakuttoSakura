//
// DownFall.cs
// 
// 2026/06/18 Created By Man-Yi, Yeh
// 2026/06/22 Updated By Man-Yi, Yeh
// 2026/06/23 Updated By Man-Yi, Yeh
// 

using UnityEngine;

public class DownFall : IFallStrategy
{
    public DownFall(float speed) 
        : base(FallDirection.Down, speed)
    {
    }

    public override bool CanFall(IBlock block)
    {
        bool res = false;

        if (block != null)
        {
            res = block.IsGoFallDown();
        }

        return res;
    }

    public override void UpdateFall(IBlock block, IBlockFallController controller)
    {
        float moveY = m_Speed * Time.deltaTime;
        float newY = block.Pos.y - moveY;
        float targetY = m_TargetPos.y;

        //check under
        BlockNode underBlockNode = block.GetNearNode(BlockNearPos.Below);
        if (underBlockNode != null)
        {
            if (!underBlockNode.IsEmpty())
            {
                IBlock underBlock = underBlockNode.Block;
                //check is under rising
                if (underBlock.IsStateType(BlockStateType.Rise))
                {
                    //if under rising
                    float riseBlockY = underBlock.Pos.y;
                    if (newY - riseBlockY < block.Size)
                    {
                        //if collision
                        //start rise
                        Vector2 pos = block.BlockNode.Pos;                  
                        block.StartRise(pos);
                        //end fall
                        controller.EndFall();

                        return;
                    }
                }
                //check is collision to falling down under
                if (underBlock.IsStateType(BlockStateType.Fall))
                {
                    if (underBlock.IsFalling(FallDirection.Down))
                    {
                        //if under fallind down
                        float fallBlockY = underBlock.Pos.y;
                        if (newY - fallBlockY < block.Size)
                        {
                            //set together down that speed as under
                            float speed = underBlock.GetFallSpeed();

                            //end fall
                            controller.EndFall(false);

                            return;
                        }
                    }
                }
            }
        }

        //check arrive
        if (newY <= targetY)
        {
            //if arrive
            //finish fall
            block.GoNearNode(BlockNearPos.Below);

            //move to targetY
            block.SetPos(new Vector2(block.Pos.x, targetY));
            //end fall
            controller.GoNextFall = true;
        }
        else
        {
            //move to newY
            block.SetPos(new Vector2(block.Pos.x, newY));
        }
    }
    

    protected override void SetTargetPos(IBlock block)
    {
        //set target pos as below node's pos
        Vector2 pos = block.GetNearNode(BlockNearPos.Below).Pos;
        m_TargetPos = pos;
    }
}
