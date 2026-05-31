//
// UIManager.cs
// 
// 2026/05/31 Created By Fate Ku
//
//
using UnityEngine;
using UnityEngine.UI;

public class UIInGameButton : UIManager
{
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private Transform parentUI; 

    private void Start()
    {
        CreateButtons();
    }

    private void CreateButtons()
    {
        for (int i = 0; i < 7; i++)
        {
            int index = i; 

            Button btn = Instantiate(buttonPrefab, parentUI);

            RectTransform rt = btn.GetComponent<RectTransform>();

            // set size
            rt.sizeDelta = GetButtonSize();

            // set position
            rt.anchoredPosition = GetButtonPos(index);

            // Button Transparent
            SetButtonTransparent(btn);

            // on click
            btn.onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    // Button Transparent
    private void SetButtonTransparent(Button btn)
    {
        Image img = btn.GetComponent<Image>();
        if (img != null)
        {
            Color c = img.color;
            //c.a = 0f; // Transparent
            //test
            c.a = 0.7f; 
            img.color = c;
        }
    }

    private void OnButtonClicked(int index)
    {
        UIManager.Instance.SetSelectedIndex(index);
        Debug.Log("Button OnClicked index = " + index);
    }

}
