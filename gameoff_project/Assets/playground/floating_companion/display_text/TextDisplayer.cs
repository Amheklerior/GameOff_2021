using System;
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
    //[SerializeField]
    //[Tooltip("Key used to skip the display text animation")]
    //private KeyCode keyToSkip = KeyCode.Space;


    [SerializeField]
    [Tooltip("Set whether text will be displayed right after it is setted or not")]
    private bool autoplay = false;

    private bool animationInProgress;
    private bool animationFinished;

    private Coroutine coroutineInstance;
    
    void Awake()
    {
        animationInProgress = false;
        animationFinished = false;
        //Display();
    }

    /*
    void Update()
    {
        if (Input.GetKeyDown(keyToSkip))
        {
            SkipAnimation();
        }
    }
    */

    public void Display()
    {
        coroutineInstance = StartCoroutine(DisplayAnimation());
    }


    private IEnumerator DisplayAnimation()
    {
        animationInProgress = true;
        animationFinished = false;
        uiTextComponent.text = "";
        foreach (char c in textToDisplay.ToCharArray())
        {
            uiTextComponent.text += c;
            yield return new WaitForSeconds(charToCharDelay);
        }
        animationInProgress = false;
        animationFinished = true;
    }


    public void SkipAnimation()
    {
        StopCoroutine(coroutineInstance);
        uiTextComponent.text = textToDisplay;
        animationInProgress = false;
        animationFinished = true;
    }
    public void SetText(string text)
    {
        this.textToDisplay = text;
        if (autoplay)
        {
            Display();
        }
    }
    internal void setAutoplay(bool value)
    {
        autoplay = value;
    }

    public bool isAnimationInProgress()
    {
        return animationInProgress;
    }

    public bool isAnimationFinished()
    {
        return animationFinished;
    }
}
