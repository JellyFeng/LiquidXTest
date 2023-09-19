using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    [SerializeField]
    private float typeSpeed = 50f;

    public Coroutine Run(string textToType, Text text)
    {
        return StartCoroutine(TypeText(textToType, text));
    }

    private IEnumerator TypeText(string textToType, Text text)
    {
        text.text = string.Empty;

        float t = 0f;
        int charIndex = 0;

        while (charIndex < textToType.Length) 
        {
            t += Time.deltaTime * typeSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            text.text = textToType.Substring(0, charIndex);

            yield return null;
        }

        text.text = textToType;
    }
}
