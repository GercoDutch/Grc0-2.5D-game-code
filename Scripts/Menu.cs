using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartLevel()
    {
        Debug.Log("Starting Level 1!");
        
        SceneManager.LoadScene("Level1");
    }

    public void CloseGame()
    {
        Debug.Log("Closing Game!");

        Application.Quit();
    }
}