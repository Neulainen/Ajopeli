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
       //HELP is scene index 1, first real levels begin from index 2
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
