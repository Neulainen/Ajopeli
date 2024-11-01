using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public Button StartButton;
    public Button Garagebutton;
    public Button HelpButton;
    public Button QuitButton;
    
    // Start is called before the first frame update
    void Start()
    {
    
      
    }

    // Update is called once per frame
    void Update()
    {
        StartButton.clicked += StartGame;
        HelpButton.clicked += HelpPage;

    }
    void StartGame()
    {
       
        SceneManager.LoadScene("Main");
    }
    void HelpPage()
    {
        SceneManager.LoadScene("HELP");
    }
    void Quit()
    {
        Application.Quit();
    }
    
}
