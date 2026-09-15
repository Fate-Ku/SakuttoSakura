//
// JecLogo.cs
// 
// 2026/09/15 Created By Man-Yi, Yeh
//

using UnityEngine;
using UnityEngine.UI;

public class JecLogo : MonoBehaviour
{
    const float FadeInDuration = 0.2f;
    const float ShowDuration = 2.0f;
    const float FadeOutDuration = 0.3f;
    const float WaitDuration = 0.3f;

    [SerializeField] private Image jecLogo;
    private float timer = 0f;

    private enum JecLogoStep
    {
        FadeIn,
        Show,
        FadeOut,
        Wait
    }
    private JecLogoStep step = JecLogoStep.FadeIn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeLogoAlpha(0);
    }

    // Update is called once per frame
    void Update()
    {
        switch (step)
        {
            case JecLogoStep.FadeIn:
                {
                    timer += Time.deltaTime;
                    if (timer >= FadeInDuration)
                    {
                        timer = 0f;
                        step = JecLogoStep.Show;
                        ChangeLogoAlpha(1);
                    }
                    else
                    {
                        ChangeLogoAlpha(timer / FadeInDuration);
                    }
                }
                break;

            case JecLogoStep.Show:
                {
                    timer += Time.deltaTime;
                    if (timer >= ShowDuration)
                    {
                        timer = 0f;
                        step = JecLogoStep.FadeOut;
                    }
                }
                break;

            case JecLogoStep.FadeOut:
                {
                    timer += Time.deltaTime;
                    if (timer >= FadeOutDuration)
                    {
                        timer = 0f;
                        step = JecLogoStep.Wait;
                        ChangeLogoAlpha(0);
                    }
                    else
                    {
                        ChangeLogoAlpha(1 - timer / FadeOutDuration);
                    }
                }
                break;

            case JecLogoStep.Wait:
                {
                    timer += Time.deltaTime;
                    if (timer >= WaitDuration)
                    {
                        Destroy(gameObject);
                    }
                }
                break; 
            
            default:
                break;
        }
    }

    private void ChangeLogoAlpha(float alpha)
    {
        Color color = jecLogo.color;
        color.a = alpha;
        jecLogo.color = color;
    }
}
