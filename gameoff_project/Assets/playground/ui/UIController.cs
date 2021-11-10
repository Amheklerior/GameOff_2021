using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;
    // Start is called before the first frame update
    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("start-btn");
        quitButton = root.Q<Button>("quit-btn");

        startButton.clicked += OnClick_StartButton;
        quitButton.clicked += OnClick_QuitButton;
    }

    // Update is called once per frame
    void OnClick_StartButton()
    {
        Debug.Log("START Button pressed");
        //SceneManager.LoadScene("name of the scene to load");
    }

    void OnClick_QuitButton()
    {
        Debug.Log("QUIT Button pressed");
        Application.Quit();
    }
}
