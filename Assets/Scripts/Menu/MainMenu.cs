using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //control buttons, use buttons to call these functions
    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void HelpPage()
    {
        SceneManager.LoadScene("HELP");
    }
    public void Quit()
    {
        Application.Quit();
    }
    
}
