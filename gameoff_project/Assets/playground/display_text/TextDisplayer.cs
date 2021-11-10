using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayer : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI uiTextComponent;
    [SerializeField]
    private string textToDisplay;
    [SerializeField]
    [Tooltip("Char-to-char delay for display animation")]
    private float charToCharDelay = 0.05f;
    [SerializeField]
    [Tooltip("Key used to skip the display text animation")]
    private KeyCode keyToSkip = KeyCode.Space;
    [SerializeField]
    [Tooltip("Set whether text will be displayed right after it is setted or not")]
    private bool autoplay = false;

    private Coroutine coroutineInstance;
    
    void Start()
    {
        Display();
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToSkip))
        {
            SkipAnimation();
        }
    }


    public void Display()
    {
        coroutineInstance = StartCoroutine(DisplayAnimation());
    }


    private IEnumerator DisplayAnimation()
    {
        uiTextComponent.text = "";
        foreach (char c in textToDisplay.ToCharArray())
        {
            uiTextComponent.text += c;
            yield return new WaitForSeconds(charToCharDelay);
        }
    }


    public void SkipAnimation()
    {
        StopCoroutine(coroutineInstance);
        uiTextComponent.text = textToDisplay;
    }
    public void SetText(string text)
    {
        this.textToDisplay = text;
        if (autoplay)
        {
            Display();
        }
    }
}
