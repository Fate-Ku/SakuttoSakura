//
// TitleState.cs
// 
// 2026/07/13 Created By Man-Yi, Yeh
//

using UnityEngine;

public class TitleState : ISceneState
{
    private float timer = 0;

    public TitleState(SceneStateController controller) 
        : base(controller)
    {
        StateName = "StartState";
    }

    //update
    public override void StateUpdate() 
    {
        timer += Time.deltaTime;

        if (timer >= 1)
        {
            GameObject gameTestOb = GameObject.Find("GameTest");
            if (gameTestOb != null)
            {
                GameTest gameTest = gameTestOb.GetComponent<GameTest>();
                if (gameTest != null)
                {
                    if (gameTest.isTutorial)
                    {
                        //change to TutorialState
                        m_Controller.SetState(new TutorialState(m_Controller), "TutorialScene");
                        return;

                    }
                   
                }
            }

            //change to MenuState
            m_Controller.SetState(new MenuState(m_Controller), "MenuScene");
        }
    }
}
