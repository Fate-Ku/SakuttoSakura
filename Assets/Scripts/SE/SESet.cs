//
// SESet.cs
// 
// 2026/09/09 Created By Man-Yi, Yeh
// 

using UnityEngine;


public class SESet : MonoBehaviour
{
    [SerializeField] private AudioSource buttonSE;
    [SerializeField] private AudioSource alarmSE;
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

    public void PlayButtonSE(float volume)
    {
        if (buttonSE != null)
        {
            buttonSE.volume = volume;
            buttonSE.Play();
        }
    }

    public void PlayAlarmSE(float volume)
    {
        if (alarmSE != null)
        {
            alarmSE.volume = volume;
            alarmSE.Play();
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
