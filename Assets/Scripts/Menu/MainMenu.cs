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
       
        SceneManager.LoadScene(2);
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
