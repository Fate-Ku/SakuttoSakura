//
// InGameUIButton.cs
// 
// 2026/05/31 Created By Fate Ku
//
//
using UnityEngine;
using UnityEngine.UI;

public class InGameUIButton
{
    private UIManager m_UIManager;
    private GameMng m_GameMng;

    [SerializeField] private Button m_ButtonPrefab;
    [SerializeField] private RectTransform m_ParentUI;

    public InGameUIButton(UIManager uiManager, GameMng gameMng)
    {
        m_UIManager = uiManager;
        m_GameMng = gameMng;
    }

    public void Init()
    {
        // Load Prefab
        //m_ButtonPrefab = Resources.Load<Button>("InGame/UI/InGameButton");

        // Find UI Panel
        //m_ParentUI = GameObject.Find("InGameButtonPanel").GetComponent<RectTransform>();

        CreateButtons();
    }

    // -------------------------
    // delete button
    // -------------------------
    public void Term()
    {
        foreach (Transform child in m_ParentUI)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    // -------------------------
    // Create button
    // -------------------------
    private void CreateButtons()
    {
        for (int i = 0; i < 7; i++)
        {
            int index = i;

            Button btn = GameObject.Instantiate(m_ButtonPrefab, m_ParentUI);

            RectTransform rt = btn.GetComponent<RectTransform>();

            // set size
            rt.sizeDelta = m_UIManager.GetButtonSize();

            // set position
            rt.anchoredPosition = m_UIManager.GetButtonPos(index);

            // set Transparent
            SetButtonTransparent(btn);

            // on click event
            btn.onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    // -------------------------
    // set button Transparent
    // -------------------------
    private void SetButtonTransparent(Button btn)
    {
        Image img = btn.GetComponent<Image>();
        if (img != null)
        {
            Color c = img.color;
            //c.a = 0f; // Transparent

            //for test
            c.a = 0.5f; 

            img.color = c;
        }
    }

    // -------------------------
    // on click event
    // -------------------------
    private void OnButtonClicked(int index)
    {
        Debug.Log("InGameUIButton Clicked: " + index);

        // return to UIManager
        m_UIManager.SetSelectedIndex(index);

    }
}
