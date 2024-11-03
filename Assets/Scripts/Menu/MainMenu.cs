using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void StartGame()
    {
       
        SceneManager.LoadScene("Main");
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
