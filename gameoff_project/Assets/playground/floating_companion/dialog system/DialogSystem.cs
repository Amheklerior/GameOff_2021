using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    //[SerializeField]
    //[Tooltip("Time (in seconds) between voicelines")]
    //private float pauseTime;
    [SerializeField]
    [Tooltip("Key used to skip to the next line")]
    private KeyCode keyToSkip = KeyCode.Space;
    [SerializeField]
    private Voiceline[] voicelines;
    [SerializeField]
    private TextDisplayer textDisplayer;

    //private Coroutine voicelinesCoroutine;
    private Voiceline currentLine;


    private bool start;
    private bool wait;
    public int currentIndex;
    private Coroutine waitCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        currentLine = null;
        //Without coroutine
        start = true;
        wait = false;
        currentIndex = 0;
        textDisplayer.setAutoplay(true);

    }

    void Update()
    {
        if (start && !wait)
        {
            ShowNextLine();
            waitCoroutine = StartCoroutine(TimerWait());
        }

        if (Input.GetKeyDown(keyToSkip))
        {
            if (currentLine != null)
            {
                if (textDisplayer.isAnimationInProgress() && !textDisplayer.isAnimationFinished())
                {
                    Debug.Log("SKIP anim");
                    textDisplayer.SkipAnimation();
                }
                else if (textDisplayer.isAnimationFinished())
                {
                    Debug.Log("SKIP to next line");
                    SkipToNextLine();
                    //ShowVoicelines().MoveNext();
                }
            }
        }
    }

    /*
    private IEnumerator ShowVoicelines()
    {
        textDisplayer.setAutoplay(true);
        foreach (Voiceline line in voicelines)
        {
            textDisplayer.SetText(line.text);
            currentLine = line;
            yield return new WaitForSeconds(line.pause);
        }
    }
    */

    private void SkipToNextLine()
    {
        if (currentIndex >= voicelines.Length)
        {
            return;
        }
        wait = false;
        StopCoroutine(waitCoroutine);
    }
    private void ShowNextLine()
    {
        if (currentIndex >= voicelines.Length)
        {
            start = false;
            return;
        }
        Voiceline line = voicelines[currentIndex];
        textDisplayer.SetText(line.text);
        currentLine = line;
        currentIndex++;
    }

    private IEnumerator TimerWait()
    {
        wait = true;
        yield return new WaitForSeconds(currentLine.pause);
        wait = false;
    }

}

