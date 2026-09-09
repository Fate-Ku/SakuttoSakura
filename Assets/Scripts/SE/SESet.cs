//
// SESet.cs
// 
// 2026/09/09 Created By Man-Yi, Yeh
// 

using UnityEngine;

public enum UISEType
{
    Button1,
}


public enum GameSEType
{
    Alarm,
}


public class SESet : MonoBehaviour
{
    [SerializeField] private AudioSource[] UISE;
    [SerializeField] private AudioSource[] gameSE;
    [SerializeField] private AudioSource[] comboSE;

    void Awake()
    {
        //don't destroy
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SEMng.Instance.Init(this);
    }

    public void PlayUISE(UISEType type, float volume)
    {
        if ((int)type < 0 || (int)type >= UISE.Length)
        {
            return;
        }

        if (UISE[(int)type] != null)
        {
            UISE[(int)type].volume = volume;
            UISE[(int)type].Play();
        }
    }

    public void PlayGameSE(GameSEType type, float volume)
    {
        if ((int)type < 0 || (int)type >= gameSE.Length)
        {
            return;
        }

        if (gameSE[(int)type] != null)
        {
            gameSE[(int)type].volume = volume;
            gameSE[(int)type].Play();
        }
    }

    public void PlayComboSE(int combo, float volume)
    {
        int index = comboSE.Length - 1;
        if (combo > 0 && combo < comboSE.Length)
        {
            index = combo - 1;
        }

        AudioSource SE = comboSE[index];
        if (SE != null)
        {
            SE.volume = volume;
            SE.Play();
        }
    }
}
