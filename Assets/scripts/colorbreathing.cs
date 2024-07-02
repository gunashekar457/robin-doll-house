using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class colorbreathing : MonoBehaviour
{
    public TMP_Text titleText;
    public Color color1 = Color.blue; // Change these colors as per your preference
    public Color color2 = Color.red;

    public float duration = 2f; // Time in seconds for each color change

    private bool isBreathing = false;

    private void Start()
    {
        StartCoroutine(BreathTitleColor());
    }

    private IEnumerator BreathTitleColor()
    {
        while (true) // Infinite loop to keep the effect going
        {
            if (!isBreathing)
            {
                isBreathing = true;
                yield return LerpTitleColor(color1, color2, duration);
                isBreathing = false;
            }
            else
            {
                isBreathing = true;
                yield return LerpTitleColor(color2, color1, duration);
                isBreathing = false;
            }
        }
    }

    private IEnumerator LerpTitleColor(Color startColor, Color targetColor, float time)
    {
        float elapsedTime = 0f;
        Color currentColor = startColor;

        while (elapsedTime < time)
        {
            currentColor = Color.Lerp(startColor, targetColor, elapsedTime / time);
            titleText.color = currentColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        titleText.color = targetColor; // Ensure the target color is set precisely at the end
    }
}
