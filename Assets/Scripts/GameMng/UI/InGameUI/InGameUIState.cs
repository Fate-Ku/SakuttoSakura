//
// InGameUIState.cs
// 
// 2026/06/24 Created By Fate Ku
//

using TMPro;
using UnityEngine;

public class InGameUIState 
{
    private TextMeshPro m_InGameStateText;

    public InGameUIState(TextMeshPro inGameStateText)
    {
        m_InGameStateText = inGameStateText;
    }


    public void Init()
    {

    }

    public void Update()
    {
        
        if (m_InGameStateText != null)
        {




        }




    }

    public void Term()
    {
        m_InGameStateText = null;
    }



}
